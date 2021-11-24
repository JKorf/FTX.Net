using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;
using FTX.Net.Enums;
using FTX.Net.Interfaces.Clients.Rest;
using FTX.Net.Objects.Internal;
using FTX.Net.Objects.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FTX.Net.Clients.Rest
{
    /// <summary>
    /// Client for interacting with the FTX API
    /// </summary>
    public class FTXClient : RestClient, IExchangeClient, IFTXClient
    {
        private const string SubaccountHeaderName = "FTX-SUBACCOUNT";

        /// <inheritDoc />
        public event Action<ICommonOrderId>? OnOrderPlaced;
        /// <inheritDoc />
        public event Action<ICommonOrderId>? OnOrderCanceled;

        internal new Objects.FTXClientOptions ClientOptions;

        /// <inheritdoc />
        public IFTXClientAccount Account { get; }
        /// <inheritdoc />
        public IFTXClientExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IFTXClientTrading Trading { get; }

        /// <inheritdoc />
        public IFTXClientConvert Convert { get; }
        /// <inheritdoc />
        public IFTXClientOptions Options { get; }
        /// <inheritdoc />
        public IFTXClientLeveragedTokens LeveragedTokens { get; }
        /// <inheritdoc />
        public IFTXClientStaking Staking { get; }
        /// <inheritdoc />
        public IFTXClientMargin Margin { get; }
        /// <inheritdoc />
        public IFTXClientNft NFT { get; }
        /// <inheritdoc />
        public IFTXClientPay FTXPay { get; }
        /// <inheritdoc />
        public IFTXClientSubaccounts Subaccounts { get; }

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of FTXClient using the default options
        /// </summary>
        public FTXClient() : this(Objects.FTXClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of FTXClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public FTXClient(Objects.FTXClientOptions options) : base("FTX", options, options.ApiCredentials == null ? null
            : new FTXAuthenticationProvider(options.ApiCredentials))
        {
            if (options == null)
                throw new ArgumentException("Cant pass null options, use empty constructor for default");

            ClientOptions = options;
            if (!string.IsNullOrEmpty(options.SubaccountName))
            {
                StandardRequestHeaders = new Dictionary<string, string>
                {
                    { SubaccountHeaderName, WebUtility.UrlEncode(options.SubaccountName) }
                };
            }
            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InBody;

            Account = new FTXClientAccount(this);
            ExchangeData = new FTXClientExchangeData(this);
            Trading = new FTXClientTrading(this);

            Convert = new FTXClientConvert(this);
            Options = new FTXClientOptions(this);
            Margin = new FTXClientMargin(this);
            Staking = new FTXClientStaking(this);
            LeveragedTokens = new FTXClientLeveragedTokens(this);
            NFT = new FTXClientNFT(this);
            FTXPay = new FTXClientPay(this);
            Subaccounts = new FTXClientSubaccounts(this);
        }
        #endregion

        #region methods
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
        public static void SetDefaultOptions(Objects.FTXClientOptions newDefaultOptions)
        {
            Objects.FTXClientOptions.Default = newDefaultOptions;
        }

        internal static Dictionary<string, string>? GetSubaccountHeader(string? subaccountName) => subaccountName == null ? null : new Dictionary<string, string>
            {
                { SubaccountHeaderName, WebUtility.UrlEncode(subaccountName) }
            };

        #region private

        internal async Task<WebCallResult<T>> SendFTXRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null, ArrayParametersSerialization? arraySerialization = null, int credits = 1, JsonSerializer? deserializer = null, Dictionary<string, string>? additionalHeaders = null)
        {
            if (signed)
                await FTXTimestampProvider.UpdateTimeAsync(this, log, ClientOptions).ConfigureAwait(false);

            var result = await SendRequestAsync<FTXResult<T>>(uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, credits, deserializer, additionalHeaders).ConfigureAwait(false);
            if (result)
                return result.As(result.Data.Result);

            return WebCallResult<T>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);
        }

        internal async Task<WebCallResult> SendFTXRequest(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null, ArrayParametersSerialization? arraySerialization = null, int credits = 1, JsonSerializer? deserializer = null, Dictionary<string, string>? additionalHeaders = null)
        {
            var result = await SendRequestAsync<FTXResult>(uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, credits, deserializer, additionalHeaders).ConfigureAwait(false);
            if (result)
                return new WebCallResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return WebCallResult.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);
        }

        internal CallResult<T> DeserializeInternal<T>(string data)
        {
            return Deserialize<T>(data);
        }

        internal Uri GetUri(string path)
        {
            return new Uri(ClientOptions.BaseAddress.AppendPath(path));
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(JToken error)
        {
            if (error["error"] == null)
                return new ServerError(error.ToString());

            return new ServerError(error["error"]!.ToString());
        }

        internal static void AddFilter(Dictionary<string, object> parameters, DateTime? startTime, DateTime? endTime)
        {
            parameters.AddOptionalParameter("start_time", startTime == null ? null : JsonConvert.SerializeObject(startTime, new TimestampSecondsConverter()));
            parameters.AddOptionalParameter("end_time", endTime == null ? null : JsonConvert.SerializeObject(endTime, new TimestampSecondsConverter()));
        }

        
        #endregion

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
            return trades.As<IEnumerable<ICommonOrder>>(trades.Data.Where<FTXOrder>(o => o.Status == OrderStatus.Closed));
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
        #endregion

        internal void InvokeOrderPlaced(ICommonOrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(ICommonOrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }
        #endregion
    }
}
