using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.CommonClients;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using FTX.Net.Enums;
using FTX.Net.Interfaces.Clients.TradeApi;
using FTX.Net.Objects;
using FTX.Net.Objects.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FTX.Net.Clients.TradeApi
{
    /// <inheritdoc cref="IFTXClientTradeApi" />
    public class FTXClientTradeApi : RestApiClient, IFTXClientTradeApi, ISpotClient, IFuturesClient
    {
        private readonly FTXClient _baseClient;
        private readonly Log _log;

        internal static TimeSyncState TimeSyncState = new TimeSyncState();

        internal FTXClientOptions ClientOptions;

        /// <inheritDoc />
        public event Action<OrderId>? OnOrderPlaced;
        /// <inheritDoc />
        public event Action<OrderId>? OnOrderCanceled;

        /// <inheritdoc />
        public string ExchangeName => "FTX";

        /// <inheritdoc />
        public IFTXClientTradeApiAccount Account { get; }
        /// <inheritdoc />
        public IFTXClientTradeApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IFTXClientTradeApiTrading Trading { get; }

        internal FTXClientTradeApi(Log log, FTXClient baseClient, FTXClientOptions options)
            : base(options, options.ApiOptions)
        {
            _baseClient = baseClient;
            _log = log;
            ClientOptions = options;

            Account = new FTXClientTradeApiAccount(this);
            ExchangeData = new FTXClientTradeApiExchangeData(this);
            Trading = new FTXClientTradeApiTrading(this);
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new FTXAuthenticationProvider(credentials);

        #region common interface
        /// <inheritdoc />
        public string GetSymbolName(string baseAsset, string quoteAsset)
        {
            return baseAsset + "/" + quoteAsset;
        }

        async Task<WebCallResult<IEnumerable<Symbol>>> IBaseRestClient.GetSymbolsAsync(CancellationToken ct)
        {
            var symbols = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!symbols)
                return symbols.As<IEnumerable<Symbol>>(null);

            return symbols.As(symbols.Data.Select(s => new Symbol
            {
                SourceObject = s,
                Name = s.Name,
                MinTradeQuantity = s.MinProvideSize,
                PriceStep = s.PriceStep,
                QuantityStep = s.QuantityStep
            }));
        }

        async Task<WebCallResult<IEnumerable<Ticker>>> IBaseRestClient.GetTickersAsync(CancellationToken ct)
        {
            var symbols = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!symbols)
                return symbols.As<IEnumerable<Ticker>>(null);

            return symbols.As(symbols.Data.Select(s => new Ticker
            {
                SourceObject = s,
                LastPrice = s.LastPrice,
                Price24H = s.LastPrice - s.Change24Hour,
                Symbol = s.Name
            }));
        }

        async Task<WebCallResult<Ticker>> IBaseRestClient.GetTickerAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException(nameof(symbol) + " required for FTX " + nameof(ISpotClient.GetTickerAsync), nameof(symbol));

            var klines = await ExchangeData.GetKlinesAsync(symbol, KlineInterval.OneHour, ct: ct).ConfigureAwait(false);
            if (!klines)
                return klines.As<Ticker>(null);

            return klines.As(GetTickerFromKlines(symbol, klines.Data));
        }

        private static Ticker GetTickerFromKlines(string symbol, IEnumerable<FTXKline> klines)
        {
            var data = klines.OrderByDescending(d => d.OpenTime).Take(24).ToList();
            if (!data.Any())
                return new Ticker
                {
                    Symbol = symbol
                };

            return new Ticker
            {
                SourceObject = data,
                Symbol = symbol,
                HighPrice = data.Max(d => d.HighPrice),
                LowPrice = data.Min(d => d.LowPrice),
                Volume = data.Sum(d => d.Volume ?? 0),
                LastPrice = data.Last().ClosePrice,
                Price24H = data.First().OpenPrice
            };
        }

        async Task<WebCallResult<IEnumerable<Kline>>> IBaseRestClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime, DateTime? endTime, int? limit, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException(nameof(symbol) + " required for FTX " + nameof(ISpotClient.GetKlinesAsync), nameof(symbol));

            var klines = await ExchangeData.GetKlinesAsync(symbol, GetKlineIntervalFromTimeSpan(timespan), startTime, endTime, ct: ct).ConfigureAwait(false);
            if (!klines)
                return klines.As<IEnumerable<Kline>>(null);

            return klines.As(klines.Data.Select(k =>
                new Kline
                {
                    SourceObject = k,
                    OpenPrice = k.OpenPrice,
                    ClosePrice = k.ClosePrice,
                    HighPrice = k.HighPrice,
                    LowPrice = k.LowPrice,
                    OpenTime = k.OpenTime,
                    Volume = k.Volume
                }));
        }

        async Task<WebCallResult<OrderBook>> IBaseRestClient.GetOrderBookAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException(nameof(symbol) + " required for FTX " + nameof(ISpotClient.GetOrderBookAsync), nameof(symbol));

            var book = await ExchangeData.GetOrderBookAsync(symbol, 100, ct: ct).ConfigureAwait(false);
            if (!book)
                return book.As<OrderBook>(null);

            return book.As(new OrderBook
            {
                SourceObject = book.Data,
                Asks = book.Data.Asks.Select(a => new OrderBookEntry { Price = a.Price, Quantity = a.Quantity }),
                Bids = book.Data.Bids.Select(b => new OrderBookEntry { Price = b.Price, Quantity = b.Quantity })
            });
        }

        async Task<WebCallResult<IEnumerable<Trade>>> IBaseRestClient.GetRecentTradesAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException(nameof(symbol) + " required for FTX " + nameof(ISpotClient.GetRecentTradesAsync), nameof(symbol));

            var trades = await ExchangeData.GetTradeHistoryAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!trades)
                return trades.As<IEnumerable<Trade>>(null);

            return trades.As(trades.Data.Select(k =>
                new Trade
                {
                    SourceObject = k,
                    Price = k.Price,
                    Quantity = k.Quantity,
                    Symbol = symbol,
                    Timestamp = k.Timestamp
                }));
        }

        async Task<WebCallResult<OrderId>> IFuturesClient.PlaceOrderAsync(string symbol, CommonOrderSide side, CommonOrderType type, decimal quantity, decimal? price, int? leverage, string? accountId, string? clientOrderId, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException(nameof(symbol) + " required for FTX " + nameof(ISpotClient.PlaceOrderAsync), nameof(symbol));

            var order = await Trading.PlaceOrderAsync(
                symbol,
                side == CommonOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                type == CommonOrderType.Limit ? Enums.OrderType.Limit : Enums.OrderType.Market,
                quantity,
                price, 
                clientOrderId: clientOrderId,
                ct: ct
                ).ConfigureAwait(false);
            if (!order)
                return order.As<OrderId>(null);

            return order.As(new OrderId
            {
                SourceObject = order.Data,
                Id = order.Data.Id.ToString(CultureInfo.InvariantCulture)
            });
        }

        async Task<WebCallResult<OrderId>> ISpotClient.PlaceOrderAsync(string symbol, CommonOrderSide side, CommonOrderType type, decimal quantity, decimal? price, string? accountId, string? clientOrderId, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException(nameof(symbol) + " required for FTX " + nameof(ISpotClient.PlaceOrderAsync), nameof(symbol));

            var order = await Trading.PlaceOrderAsync(
                symbol,
                side == CommonOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                type == CommonOrderType.Limit ? Enums.OrderType.Limit : Enums.OrderType.Market,
                quantity,
                price,
                clientOrderId: clientOrderId, 
                ct: ct
                ).ConfigureAwait(false);
            if (!order)
                return order.As<OrderId>(null);

            return order.As(new OrderId
            {
                SourceObject = order.Data,
                Id = order.Data.Id.ToString(CultureInfo.InvariantCulture)
            });
        }

        async Task<WebCallResult<IEnumerable<Position>>> IFuturesClient.GetPositionsAsync(CancellationToken ct)
        {
            var positions = await Account.GetPositionsAsync(ct: ct).ConfigureAwait(false);
            if (!positions)
                return positions.As<IEnumerable<Position>>(null);

            return positions.As(positions.Data.Select(p => new Position
            {
                SourceObject = p,
                Symbol = p.Future,
                EntryPrice = p.EntryPrice,
                Quantity = p.Quantity,
                UnrealizedPnl = p.UnrealizedPnl,
                LiquidationPrice = p.EstimatedLiquidationPrice,
                Side = p.Side == OrderSide.Sell ? CommonPositionSide.Short: CommonPositionSide.Long,
                MaintananceMargin = p.MaintenanceMarginRequirement,
                RealizedPnl = p.RealizedPnl
            }));
        }

        async Task<WebCallResult<Order>> IBaseRestClient.GetOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (!long.TryParse(orderId, out var id))
                throw new ArgumentException($"Invalid order id for CoinEx {nameof(ISpotClient.GetOrderAsync)}", nameof(orderId));

            var order = await Trading.GetOrderAsync(id, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.As<Order>(null);

            return order.As(new Order
            {
                SourceObject = order.Data,
                Id = order.Data.Id.ToString(CultureInfo.InvariantCulture),
                Price = order.Data.Price,
                Quantity = order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                Timestamp = order.Data.CreateTime,
                Symbol = order.Data.Symbol,
                Side = order.Data.Side == Enums.OrderSide.Buy ? CommonOrderSide.Buy: CommonOrderSide.Sell,
                Status = (order.Data.Status == Enums.OrderStatus.Open || order.Data.Status == Enums.OrderStatus.New) ? CommonOrderStatus.Active: order.Data.QuantityRemaining > 0 ? CommonOrderStatus.Canceled: CommonOrderStatus.Filled,
                Type = order.Data.Type == Enums.OrderType.Market ? CommonOrderType.Market: CommonOrderType.Limit
            });
        }

        async Task<WebCallResult<IEnumerable<UserTrade>>> IBaseRestClient.GetOrderTradesAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (!long.TryParse(orderId, out var id))
                throw new ArgumentException($"Invalid order id for CoinEx {nameof(ISpotClient.GetOrderTradesAsync)}", nameof(orderId));

            var trades = await Trading.GetUserTradesAsync(orderId: id, ct: ct).ConfigureAwait(false);
            if (!trades)
                return trades.As<IEnumerable<UserTrade>>(null);

            return trades.As(trades.Data.Select(t => new UserTrade
            {
                SourceObject = t,
                Id = t.Id.ToString(CultureInfo.InvariantCulture),
                OrderId = t.OrderId?.ToString(CultureInfo.InvariantCulture),
                Price = t.Price,
                Quantity = t.Quantity,
                Symbol = t.Symbol,
                Timestamp = t.Timestamp,
                Fee = t.Fee,
                FeeAsset = t.FeeAsset
            }));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetOpenOrdersAsync(string? symbol, CancellationToken ct)
        {
            var orders = await Trading.GetOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.As<IEnumerable<Order>>(null);

            return orders.As(orders.Data.Select(t => new Order
            {
                SourceObject = t,
                Id = t.Id.ToString(CultureInfo.InvariantCulture),
                Price = t.Price,
                Quantity = t.Quantity,
                QuantityFilled = t.QuantityFilled,
                Timestamp = t.CreateTime,
                Symbol = t.Symbol,
                Side = t.Side == Enums.OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
                Status = (t.Status == Enums.OrderStatus.Open || t.Status == Enums.OrderStatus.New) ? CommonOrderStatus.Active : t.QuantityRemaining > 0 ? CommonOrderStatus.Canceled : CommonOrderStatus.Filled,
                Type = t.Type == Enums.OrderType.Market ? CommonOrderType.Market : CommonOrderType.Limit
            }));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetClosedOrdersAsync(string? symbol, CancellationToken ct)
        {
            var orders = await Trading.GetOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.As<IEnumerable<Order>>(null);

            return orders.As(orders.Data.Select(t => new Order
            {
                SourceObject = t,
                Id = t.Id.ToString(CultureInfo.InvariantCulture),
                Price = t.Price,
                Quantity = t.Quantity,
                QuantityFilled = t.QuantityFilled,
                Timestamp = t.CreateTime,
                Symbol = t.Symbol,
                Side = t.Side == Enums.OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
                Status = (t.Status == Enums.OrderStatus.Open || t.Status == Enums.OrderStatus.New) ? CommonOrderStatus.Active : t.QuantityRemaining > 0 ? CommonOrderStatus.Canceled : CommonOrderStatus.Filled,
                Type = t.Type == Enums.OrderType.Market ? CommonOrderType.Market : CommonOrderType.Limit
            }));
        }

        async Task<WebCallResult<OrderId>> IBaseRestClient.CancelOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (!long.TryParse(orderId, out var id))
                throw new ArgumentException($"Invalid order id for CoinEx {nameof(ISpotClient.CancelOrderAsync)}", nameof(orderId));

            var order = await Trading.CancelOrderAsync(id, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.As<OrderId>(null);

            return order.As(new OrderId
            {
                SourceObject = order.Data,
                Id = orderId
            });
        }

        async Task<WebCallResult<IEnumerable<Balance>>> IBaseRestClient.GetBalancesAsync(string? accountId, CancellationToken ct)
        {
            var balances = await Account.GetBalancesAsync(ct: ct).ConfigureAwait(false);
            if (!balances)
                return balances.As<IEnumerable<Balance>>(null);

            return balances.As(balances.Data.Select(b => new Balance
            {
                SourceObject = b,
                Asset = b.Asset,
                Available = b.Available,
                Total = b.Total
            }));
        }

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

        internal void InvokeOrderPlaced(OrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(OrderId id)
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

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        protected override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, ClientOptions.ApiOptions.AutoTimestamp, TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan GetTimeOffset()
            => TimeSyncState.TimeOffset;

        /// <inheritdoc />
        public ISpotClient CommonSpotClient => this;

        /// <inheritdoc />
        public IFuturesClient CommonFuturesClient => this;
    }
}
