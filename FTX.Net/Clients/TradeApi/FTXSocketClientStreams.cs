using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using FTX.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using System.Threading;
using FTX.Net.Objects.Internal;
using FTX.Net.Objects.Models;
using FTX.Net.Objects.Models.Socket;
using CryptoExchange.Net.Logging;
using FTX.Net.Interfaces.Clients.TradeApi;

namespace FTX.Net.Clients.TradeApi
{
    /// <inheritdoc cref="IFTXSocketClientStreams" />
    public class FTXSocketClientStreams : SocketApiClient, IFTXSocketClientStreams
    {
        #region fields
        private readonly Log _log;
        private readonly FTXSocketClient _baseClient;
        #endregion

        #region ctor
        
        internal FTXSocketClientStreams(Log log, FTXSocketClient baseClient, FTXSocketClientOptions options)
            : base(options, options.StreamOptions)
        {
            _log = log;
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new FTXAuthenticationProvider(credentials);

        #endregion

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<FTXStreamTicker>> handler, CancellationToken ct = default)
        {
            return await SubscribeAsync(new SubscribeRequest("ticker", symbol), false, handler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolsUpdatesAsync(Action<DataEvent<Dictionary<string, FTXStreamSymbol>>> handler, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(data =>
            {
                var actualData = data.Data["data"];
                if (actualData == null)
                    return;

                var deserializeResult = _baseClient.DeserializeInternal<Dictionary<string, FTXStreamSymbol>>(actualData);
                if (!deserializeResult)
                {
                    _log.Write(LogLevel.Warning, "Failed to deserialize stream data: " + deserializeResult.Error);
                    return;
                }

                handler?.Invoke(data.As(deserializeResult.Data));
            });
            return await SubscribeAsync(new SubscribeRequest("markets", null), false, innerHandler, ct).ConfigureAwait(false);
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

                var deserializeResult = _baseClient.DeserializeInternal<FTXStreamOrderBook>(actualData);
                if (!deserializeResult)
                {
                    _log.Write(LogLevel.Warning, "Failed to deserialize grouped orderbook data: " + deserializeResult.Error);
                    return;
                }

                var resultObject = deserializeResult.Data;
                resultObject.Action = data.Data["type"]!.ToString();
                resultObject.Timestamp = DateTime.UtcNow;

                handler?.Invoke(data.As(resultObject));
            });
            return await _baseClient.SubscribeInternalAsync(this, new GroupedOrderBookSubscribeRequest("orderbookGrouped", symbol, grouping), null, false, innerHandler, ct).ConfigureAwait(false);
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

                var deserializeResult = _baseClient.DeserializeInternal<T>(actualData);
                if (!deserializeResult)
                {
                    _log.Write(LogLevel.Warning, "Failed to deserialize stream data: " + deserializeResult.Error);
                    return;
                }

                var market = data.Data["market"]?.ToString();
                handler?.Invoke(data.As(deserializeResult.Data, market));
            });
            return await _baseClient.SubscribeInternalAsync(this, request, null, authenticated, internalHandler, ct).ConfigureAwait(false);
        }
    }
}
