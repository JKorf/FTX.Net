using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using FTX.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Threading;
using FTX.Net.Objects.Internal;
using Newtonsoft.Json;
using FTX.Net.Interfaces.Clients;
using FTX.Net.Interfaces.Clients.TradeApi;
using FTX.Net.Clients.TradeApi;

namespace FTX.Net.Clients
{
    /// <summary>
    /// Client for interacting with the FTX websocket API
    /// </summary>
    public class FTXSocketClient : BaseSocketClient, IFTXSocketClient
    {
        #region fields
        private readonly string? _subaccount;
        #endregion

        #region Api clients

        public IFTXSocketClientStreams Streams { get; }

        #endregion

        #region ctor
        /// <summary>
        /// Create a new instance of FTXSocketClient using the default options
        /// </summary>
        public FTXSocketClient() : this(FTXSocketClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of FTXSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public FTXSocketClient(FTXSocketClientOptions options) : base("FTX", options)
        {
            if (options == null)
                throw new ArgumentException("Cant pass null options, use empty constructor for default");

            _subaccount = options.SubaccountName;

            Streams = new FTXSocketClientStreams(log, this, options);

            SendPeriodic(TimeSpan.FromSeconds(15), (connection) => new SocketRequest("ping"));

            AddGenericHandler("PongHandler", (a) => { });
            AddGenericHandler("InfoHandler", InfoHandler);
        }
        #endregion

        internal Task<CallResult<UpdateSubscription>> SubscribeInternalAsync<T>(SocketApiClient apiClient, object? request, string? identifier, bool authenticated, Action<DataEvent<T>> dataHandler, CancellationToken ct)
        {
            return SubscribeAsync(apiClient, request, identifier, authenticated, dataHandler, ct);
        }

        internal Task<CallResult<T>> QueryInternalAsync<T>(SocketApiClient apiClient, object request, bool authenticated)
            => QueryAsync<T>(apiClient, request, authenticated);

        internal CallResult<T> DeserializeInternal<T>(JToken obj, JsonSerializer? serializer = null, int? requestId = null)
            => Deserialize<T>(obj, serializer, requestId);

        private void InfoHandler(MessageEvent messageEvent)
        {
            var code = messageEvent.JsonData["code"];
            if (code?.ToString() == "20001")
            {
                log.Write(LogLevel.Information, $"Code {code} received. Reconnecting/Resubscribing socket.");
                messageEvent.Connection.Socket.CloseAsync(); // Closing it via socket will automatically reconnect
            }
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection socketConnection)
        {
            var time = (long)Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds);
            if (socketConnection.ApiClient.AuthenticationProvider == null)
                return new CallResult<bool>(false, new NoApiCredentialsError());

            var loginRequest = new LoginRequest(socketConnection.ApiClient.AuthenticationProvider.Credentials.Key!.GetString(), socketConnection.ApiClient.AuthenticationProvider.Sign(time.ToString() + "websocket_login"), time, _subaccount);
            // If we don't get a response it's okay
            var result = new CallResult<bool>(true, null);

            await socketConnection.SendAndWaitAsync(loginRequest, TimeSpan.FromSeconds(1), tokenData =>
            {
                if (tokenData.Type != JTokenType.Object)
                    return false;

                var code = tokenData["code"];

                if (code != null && (int)code == 400)
                {
                    result = new CallResult<bool>(false, new ServerError((int)code, tokenData["msg"]?.ToString() ?? "Unknown error"));
                    return true;
                }

                return false;
            }).ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection socketConnection, object request, JToken data, out CallResult<T> callResult)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection socketConnection, SocketSubscription subscription, object request, JToken data, out CallResult<object>? callResult)
        {
            callResult = null;
            var ftxRequest = (SubscribeRequest)request;
            var channel = data["channel"];
            var market = data["market"];
            var type = data["type"];

            var matchesRequest = channel?.ToString() == ftxRequest.Channel && market?.ToString() == ftxRequest.Market;
            if (!matchesRequest)
            {
                // Error response don't contain channel/market so this message might still be for this subscription if the subscription resulted in an error
                // We can only check this if the market is not null
                if (type?.ToString() == "error" && ftxRequest.Market != null)
                {
                    var message = data["msg"];
                    if (message != null && message.ToString().ToLowerInvariant().Contains(ftxRequest.Market.ToLowerInvariant()))
                    {
                        // It is an error and the error message contains the market we sent, assuming its for this request
                        var code = data["code"];
                        var errorCode = code == null ? 0 : (int)code;
                        callResult = new CallResult<object>(null, new ServerError(errorCode, message.ToString()));
                        return true;
                    }
                }

                return false;
            }

            if (type == null)
            {
                log.Write(LogLevel.Warning, "Received subscribe response without type. Data received: " + data);
                return true;
            }

            if (type.ToString() == "subscribed")
            {
                callResult = new CallResult<object>(null, null);
                return true;
            }

            callResult = new CallResult<object>(null, new ServerError("Unexpected subscribe request answer: " + type));
            return true;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            var ftxRequest = (SubscribeRequest)request;
            var channel = message["channel"];
            var market = message["market"];
            var type = message["type"];
            return (type?.ToString() == "update" || type?.ToString() == "partial") && channel?.ToString() == ftxRequest.Channel && market?.ToString() == ftxRequest.Market;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            var type = message["type"];
            if (type == null)
                return false;

            if (identifier == "PongHandler" && type.ToString() == "pong")
                return true;
            if (identifier == "InfoHandler" && type.ToString() == "info")
                return true;

            return false;
        }

        /// <inheritdoc />
        protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription subscriptionToUnsub)
        {
            var ftxRequest = (SubscribeRequest)subscriptionToUnsub.Request!;
            if (ftxRequest == null)
                return false;

            var unsub = new UnsubscribeRequest(ftxRequest.Channel, ftxRequest.Market);
            var result = false;
            await connection.SendAndWaitAsync(unsub, ClientOptions.SocketResponseTimeout, data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

                var channel = data["channel"];
                var market = data["market"];
                var type = data["type"];

                result = type?.ToString() == "unsubscribed" && channel?.ToString() == ftxRequest.Channel && market?.ToString() == ftxRequest.Market;
                return result;
            }).ConfigureAwait(false);
            return result;
        }

        public override void Dispose()
        {
            Streams.Dispose();
            base.Dispose();
        }
    }
}
