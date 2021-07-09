using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using FTX.Net.Objects;
using FTX.Net.Objects.SocketObjects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTX.Net
{
    public class FTXSocketClient : SocketClient
    {
        #region fields
        private static FTXSocketClientOptions defaultOptions = new FTXSocketClientOptions();
        private static FTXSocketClientOptions DefaultOptions => defaultOptions.Copy<FTXSocketClientOptions>();
        #endregion

        #region ctor
        /// <summary>
        /// Create a new instance of BitfinexSocketClient using the default options
        /// </summary>
        public FTXSocketClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of BitfinexSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public FTXSocketClient(FTXSocketClientOptions options) : base("FTX", options, options.ApiCredentials == null ? null : new FTXAuthenticationProvider(options.ApiCredentials))
        {
            if (options == null)
                throw new ArgumentException("Cant pass null options, use empty constructor for default");

            SendPeriodic(TimeSpan.FromSeconds(15), (connection) => new SocketRequest("ping"));

            AddGenericHandler("PongHandler", (a) => { });
            AddGenericHandler("InfoHandler", InfoHandler);
        }
        #endregion

        /// <summary>
        /// set the default options used when creating a client without specifying options
        /// </summary>
        /// <param name="newDefaultOptions"></param>
        public static void SetDefaultOptions(FTXSocketClientOptions newDefaultOptions)
        {
            defaultOptions = newDefaultOptions;
        }


        /// <summary>
        /// Subscribes to ticker updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="handler">The handler for the data</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<FTXStreamTicker>> handler)
        {
            return await SubscribeAsync(new SubscribeRequest("ticker", symbol), false, handler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribes to ticker updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="handler">The handler for the data</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<FTXOrder>> handler)
        {
            return await SubscribeAsync(new SubscribeRequest("orders", null), true, handler).ConfigureAwait(false);
        }

        public async Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(object request, bool authenticated, Action<DataEvent<T>> handler)
        {
            var internalHandler = new Action<DataEvent<JToken>>(data =>
            {
                var actualData = data.Data["data"];
                if (actualData == null)
                    return;

                var deserializeResult = Deserialize<T>(actualData);
                if (!deserializeResult)
                {
                    // log
                    return;
                }

                handler?.Invoke(data.As(deserializeResult.Data));
            });
            return await SubscribeAsync(request, null, authenticated, internalHandler).ConfigureAwait(false);
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

        protected override async Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection socketConnection)
        {
            var time = (long)Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds);
            var loginRequest = new LoginRequest(authProvider.Credentials.Key.GetString(), authProvider.Sign(time.ToString() + "websocket_login"), time);
            // If we don't get a response it's okay
            var result = new CallResult<bool>(true, null);

            await socketConnection.SendAndWaitAsync(loginRequest, TimeSpan.FromSeconds(1), tokenData =>
            {
                if (tokenData.Type != JTokenType.Object)
                    return false;

                var code = tokenData["code"];

                if(code != null && (int)code == 400)
                {
                    result = new CallResult<bool>(false, new ServerError((int)code, tokenData["msg"]?.ToString()));
                    return true;
                }

                return false;
            }).ConfigureAwait(false);

            return result;
        }

        protected override bool HandleQueryResponse<T>(SocketConnection socketConnection, object request, JToken data, out CallResult<T>? callResult)
        {
            throw new NotImplementedException();
        }

        protected override bool HandleSubscriptionResponse(SocketConnection socketConnection, SocketSubscription subscription, object request, JToken data, out CallResult<object>? callResult)
        {
            callResult = null;
            var ftxRequest = (SubscribeRequest)request;
            var channel = data["channel"];
            var market = data["market"];
            var type = data["type"];

            if (channel?.ToString() != ftxRequest.Channel || market?.ToString() != ftxRequest.Market)
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

        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            var ftxRequest = (SubscribeRequest)request;
            var channel = message["channel"];
            var market = message["market"];
            var type = message["type"];
            return (type?.ToString() == "update" || type?.ToString() == "partial") && channel?.ToString() == ftxRequest.Channel && market?.ToString() == ftxRequest.Market;
        }

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

        protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription subscriptionToUnsub)
        {
            var ftxRequest = (SubscribeRequest)subscriptionToUnsub.Request;
            if (ftxRequest == null)
                return false;

            var unsub = new UnsubscribeRequest(ftxRequest.Channel, ftxRequest.Market);
            var result = false;
            await connection.SendAndWaitAsync(unsub, ResponseTimeout, data =>
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
