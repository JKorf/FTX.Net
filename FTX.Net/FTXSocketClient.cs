using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using FTX.Net.Objects;
using FTX.Net.Objects.SocketObjects;
using FTX.Net.Objects.Spot;
using FTX.Net.Objects.Spot.Socket;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FTX.Net.Interfaces;
using CryptoExchange.Net.Authentication;
using System.Threading;

namespace FTX.Net
{
    /// <summary>
    /// Client for interacting with the FTX websocket API
    /// </summary>
    public class FTXSocketClient : SocketClient, IFTXSocketClient
    {
        #region fields
        private static FTXSocketClientOptions _defaultOptions = new FTXSocketClientOptions();
        private static FTXSocketClientOptions DefaultOptions => _defaultOptions.Copy<FTXSocketClientOptions>();

        private readonly string? _subaccount;
        #endregion

        #region ctor
        /// <summary>
        /// Create a new instance of FTXSocketClient using the default options
        /// </summary>
        public FTXSocketClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of FTXSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public FTXSocketClient(FTXSocketClientOptions options) : base("FTX", options, options.ApiCredentials == null ? null : new FTXAuthenticationProvider(options.ApiCredentials))
        {
            if (options == null)
                throw new ArgumentException("Cant pass null options, use empty constructor for default");

            _subaccount = options.SubaccountName;

            SendPeriodic(TimeSpan.FromSeconds(15), (connection) => new SocketRequest("ping"));

            AddGenericHandler("PongHandler", (a) => { });
            AddGenericHandler("InfoHandler", InfoHandler);
        }
        #endregion

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        public void SetApiCredentials(string apiKey, string apiSecret)
        {
            SetAuthenticationProvider(new FTXAuthenticationProvider(new ApiCredentials(apiKey, apiSecret)));
        }

        /// <summary>
        /// set the default options used when creating a client without specifying options
        /// </summary>
        /// <param name="newDefaultOptions"></param>
        public static void SetDefaultOptions(FTXSocketClientOptions newDefaultOptions)
        {
            _defaultOptions = newDefaultOptions;
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<FTXStreamTicker>> handler, CancellationToken ct = default)
        {
            return await SubscribeAsync(new SubscribeRequest("ticker", symbol), false, handler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<FTXTrade>>> handler, CancellationToken ct = default)
        {
            return await SubscribeAsync(new SubscribeRequest("trades", symbol), false, handler, ct).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<FTXStreamOrderBook>> handler, CancellationToken ct = default)
        {
            return await SubscribeAsync(new SubscribeRequest("orderbook", symbol), false, handler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToGroupedOrderBookUpdatesAsync(string symbol, int grouping, Action<DataEvent<FTXStreamOrderBook>> handler, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>((data) =>
            {
                var actualData = data.Data["data"];
                if (actualData == null)
                    return;

                var deserializeResult = Deserialize<FTXStreamOrderBook>(actualData);
                if (!deserializeResult)
                {
                    log.Write(LogLevel.Warning, "Failed to deserialize grouped orderbook data: " + deserializeResult.Error);
                    return;
                }

                var resultObject = deserializeResult.Data;
                resultObject.Action = data.Data["type"]!.ToString();
                resultObject.Timestamp = DateTime.UtcNow;

                handler?.Invoke(data.As(resultObject));
            });
            return await SubscribeAsync(new GroupedOrderBookSubscribeRequest("orderbookGrouped", symbol, grouping), null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<FTXOrder>> handler, CancellationToken ct = default)
        {
            return await SubscribeAsync(new SubscribeRequest("orders", null), true, handler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<FTXUserTrade>> handler, CancellationToken ct = default)
        {
            return await SubscribeAsync(new SubscribeRequest("fills", null), true, handler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFTXPayUpdatesAsync(Action<DataEvent<FTXUserTrade>> handler, CancellationToken ct = default)
        {
            return await SubscribeAsync(new SubscribeRequest("ftxpay", null), true, handler, ct).ConfigureAwait(false);
        }

        private async Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(object request, bool authenticated, Action<DataEvent<T>> handler, CancellationToken ct)
        {
            var internalHandler = new Action<DataEvent<JToken>>(data =>
            {
                var actualData = data.Data["data"];
                if (actualData == null)
                    return;

                var deserializeResult = Deserialize<T>(actualData);
                if (!deserializeResult)
                {
                    log.Write(LogLevel.Warning, "Failed to deserialize stream data: " + deserializeResult.Error);
                    return;
                }

                var market = data.Data["market"]?.ToString();
                handler?.Invoke(data.As(deserializeResult.Data, market));
            });
            return await SubscribeAsync(request, null, authenticated, internalHandler, ct).ConfigureAwait(false);
        }

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
            if (authProvider == null)
                return new CallResult<bool>(false, new NoApiCredentialsError());

            var loginRequest = new LoginRequest(authProvider.Credentials.Key!.GetString(), authProvider.Sign(time.ToString() + "websocket_login"), time, _subaccount);
            // If we don't get a response it's okay
            var result = new CallResult<bool>(true, null);

            await socketConnection.SendAndWaitAsync(loginRequest, TimeSpan.FromSeconds(1), tokenData =>
            {
                if (tokenData.Type != JTokenType.Object)
                    return false;

                var code = tokenData["code"];

                if(code != null && (int)code == 400)
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
                if(type?.ToString() == "error" && ftxRequest.Market != null)
                {
                    var message = data["msg"];
                    if(message != null && message.ToString().ToLowerInvariant().Contains(ftxRequest.Market.ToLowerInvariant()))
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

            if(type == null)
            {
                log.Write(LogLevel.Warning, "Received subscribe response without type. Data received: " + data);
                return true;
            }

            if(type.ToString() == "subscribed")
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
    }
}
