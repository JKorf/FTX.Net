using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;
using FTX.Net.Enums;
using FTX.Net.Interfaces.Clients.TradeApi;
using FTX.Net.Objects;
using FTX.Net.Objects.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FTX.Net.Clients.TradeApi
{
    public class FTXClientTradeApi : RestApiClient, IFTXClientTradeApi, IExchangeClient
    {
        private readonly FTXClient _baseClient;

        internal FTXClientOptions ClientOptions;

        /// <inheritDoc />
        public event Action<ICommonOrderId>? OnOrderPlaced;
        /// <inheritDoc />
        public event Action<ICommonOrderId>? OnOrderCanceled;

        /// <inheritdoc />
        public IFTXClientTradeApiAccount Account { get; }
        /// <inheritdoc />
        public IFTXClientTradeApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IFTXClientTradeApiTrading Trading { get; }


        public FTXClientTradeApi(FTXClient baseClient, FTXClientOptions options)
            : base(options, options.ApiOptions)
        {
            _baseClient = baseClient;

            ClientOptions = options;

            Account = new FTXClientTradeApiAccount(this);
            ExchangeData = new FTXClientTradeApiExchangeData(this);
            Trading = new FTXClientTradeApiTrading(this);
        }

        public override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new FTXAuthenticationProvider(credentials);

        #region common interface
        /// <inheritdoc />
        public string GetSymbolName(string baseAsset, string quoteAsset)
        {
            return baseAsset + "/" + quoteAsset;
        }

#pragma warning disable 1066
        async Task<WebCallResult<IEnumerable<ICommonSymbol>>> IExchangeClient.GetSymbolsAsync()
        {
            var symbols = await ExchangeData.GetSymbolsAsync().ConfigureAwait(false);
            return symbols.As<IEnumerable<ICommonSymbol>>(symbols.Data);
        }

        Task<WebCallResult<IEnumerable<ICommonTicker>>> IExchangeClient.GetTickersAsync()
        {
            throw new InvalidOperationException("FTX API has no support for getting High/Low/Volume stats for all symbols in a call");
        }

        async Task<WebCallResult<ICommonTicker>> IExchangeClient.GetTickerAsync(string symbol)
        {
            var klines = await ExchangeData.GetKlinesAsync(symbol, KlineInterval.OneHour).ConfigureAwait(false);
            if (!klines)
                return klines.As((ICommonTicker)null!);

            return klines.As(GetTickerFromKlines(symbol, klines.Data));
        }

        private static ICommonTicker GetTickerFromKlines(string symbol, IEnumerable<FTXKline> klines)
        {
            var data = klines.OrderByDescending(d => d.OpenTime).Take(24).ToList();
            if (!data.Any())
                return new FTXTick
                {
                    Symbol = symbol
                };

            return new FTXTick
            {
                Symbol = symbol,
                HighPrice = data.Max(d => d.HighPrice),
                LowPrice = data.Min(d => d.LowPrice),
                Volume = data.Sum(d => d.Volume ?? 0)
            };
        }

        async Task<WebCallResult<IEnumerable<ICommonKline>>> IExchangeClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            var klines = await ExchangeData.GetKlinesAsync(symbol, GetKlineIntervalFromTimeSpan(timespan), startTime, endTime).ConfigureAwait(false);
            return klines.As<IEnumerable<ICommonKline>>(klines.Data);
        }

        async Task<WebCallResult<ICommonOrderBook>> IExchangeClient.GetOrderBookAsync(string symbol)
        {
            var book = await ExchangeData.GetOrderBookAsync(symbol, 100).ConfigureAwait(false);
            return book.As<ICommonOrderBook>(book.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonRecentTrade>>> IExchangeClient.GetRecentTradesAsync(string symbol)
        {
            var trades = await ExchangeData.GetTradeHistoryAsync(symbol).ConfigureAwait(false);
            return trades.As<IEnumerable<ICommonRecentTrade>>(trades.Data);
        }

        async Task<WebCallResult<ICommonOrderId>> IExchangeClient.PlaceOrderAsync(string symbol, IExchangeClient.OrderSide side, IExchangeClient.OrderType type, decimal quantity, decimal? price = null, string? accountId = null)
        {
            var trades = await Trading.PlaceOrderAsync(
                symbol,
                side == IExchangeClient.OrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                type == IExchangeClient.OrderType.Limit ? OrderType.Limit : OrderType.Market,
                quantity,
                price
                ).ConfigureAwait(false);
            return trades.As<ICommonOrderId>(trades.Data);
        }

        async Task<WebCallResult<ICommonOrder>> IExchangeClient.GetOrderAsync(string orderId, string? symbol = null)
        {
            var trades = await Trading.GetOrderAsync(long.Parse(orderId)).ConfigureAwait(false);
            return trades.As<ICommonOrder>(trades.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonTrade>>> IExchangeClient.GetTradesAsync(string orderId, string? symbol = null)
        {
            var trades = await Trading.GetUserTradesAsync(orderId: long.Parse(orderId)).ConfigureAwait(false);
            return trades.As<IEnumerable<ICommonTrade>>(trades.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetOpenOrdersAsync(string? symbol = null)
        {
            var trades = await Trading.GetOpenOrdersAsync(symbol).ConfigureAwait(false);
            return trades.As<IEnumerable<ICommonOrder>>(trades.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetClosedOrdersAsync(string? symbol = null)
        {
            var trades = await Trading.GetOrdersAsync(symbol).ConfigureAwait(false);
            return trades.As<IEnumerable<ICommonOrder>>(trades.Data.Where(o => o.Status == OrderStatus.Closed));
        }

        async Task<WebCallResult<ICommonOrderId>> IExchangeClient.CancelOrderAsync(string orderId, string? symbol = null)
        {
            var trades = await Trading.CancelOrderAsync(long.Parse(orderId)).ConfigureAwait(false);
            return trades.As<ICommonOrderId>(new FTXOrder { Id = long.Parse(orderId) });
        }

        async Task<WebCallResult<IEnumerable<ICommonBalance>>> IExchangeClient.GetBalancesAsync(string? accountId = null)
        {
            var balances = await Account.GetBalancesAsync().ConfigureAwait(false);
            return balances.As<IEnumerable<ICommonBalance>>(balances.Data);
        }
#pragma warning restore 1066

        private static KlineInterval GetKlineIntervalFromTimeSpan(TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.FromSeconds(15)) return KlineInterval.FifteenSeconds;
            if (timeSpan == TimeSpan.FromMinutes(1)) return KlineInterval.OneMinute;
            if (timeSpan == TimeSpan.FromMinutes(5)) return KlineInterval.FiveMinutes;
            if (timeSpan == TimeSpan.FromMinutes(15)) return KlineInterval.FifteenMinutes;
            if (timeSpan == TimeSpan.FromHours(1)) return KlineInterval.OneHour;
            if (timeSpan == TimeSpan.FromHours(4)) return KlineInterval.FourHours;
            if (timeSpan == TimeSpan.FromDays(1)) return KlineInterval.OneDay;
            if (timeSpan == TimeSpan.FromDays(7)) return KlineInterval.OneWeek;
            if (timeSpan == TimeSpan.FromDays(30) || timeSpan == TimeSpan.FromDays(31)) return KlineInterval.OneMonth;

            throw new ArgumentException("Unsupported timespan for FTX Klines, check supported intervals using FTX.Net.Enums.KlineInterval");
        }

        internal void InvokeOrderPlaced(ICommonOrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(ICommonOrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }
        #endregion


        internal Task<WebCallResult<T>> SendFTXRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null, ArrayParametersSerialization? arraySerialization = null, int credits = 1, JsonSerializer? deserializer = null, Dictionary<string, string>? additionalHeaders = null)
         => _baseClient.SendFTXRequest<T>(this, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, credits, deserializer, additionalHeaders);

        internal Task<WebCallResult> SendFTXRequest(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null, ArrayParametersSerialization? arraySerialization = null, int credits = 1, JsonSerializer? deserializer = null, Dictionary<string, string>? additionalHeaders = null)
         => _baseClient.SendFTXRequest(this, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, credits, deserializer, additionalHeaders);

        internal CallResult<T> DeserializeInternal<T>(string data)
        {
            return _baseClient.DeserializeInternal<T>(data);
        }

        internal Uri GetUri(string path)
        {
            return new Uri(BaseAddress.AppendPath(path));
        }
    }
}
