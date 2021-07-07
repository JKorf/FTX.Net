using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;
using FTX.Net.Converters;
using FTX.Net.Enums;
using FTX.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FTX.Net
{
    public class FTXClient : RestClient, IExchangeClient
    {
        private static FTXClientOptions defaultOptions = new FTXClientOptions();

        public event Action<ICommonOrderId> OnOrderPlaced;
        public event Action<ICommonOrderId> OnOrderCanceled;

        private static FTXClientOptions DefaultOptions => defaultOptions.Copy<FTXClientOptions>();

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of BitfinexClient using the default options
        /// </summary>
        public FTXClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of BitfinexClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public FTXClient(FTXClientOptions options) : base("FTX", options, options.ApiCredentials == null ? null : new FTXAuthenticationProvider(options.ApiCredentials))
        {
            if (options == null)
                throw new ArgumentException("Cant pass null options, use empty constructor for default");

        }
        #endregion

        #region methods
        /// <summary>
        /// set the default options used when creating a client without specifying options
        /// </summary>
        /// <param name="newDefaultOptions"></param>
        public static void SetDefaultOptions(FTXClientOptions newDefaultOptions)
        {
            defaultOptions = newDefaultOptions;
        }

        /// <summary>
        /// Get the list of supported symbols
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXSymbol>>> GetSymbolsAsync(CancellationToken ct = default)
        {
            return await SendFTXRequest<IEnumerable<FTXSymbol>>(GetUri("markets"), HttpMethod.Get, ct);
        }

        /// <summary>
        /// Get the list balances
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXBalance>>> GetBalancesAsync(CancellationToken ct = default)
        {
            return await SendFTXRequest<IEnumerable<FTXBalance>>(GetUri("wallet/balances"), HttpMethod.Get, ct, signed: true);
        }

        /// <summary>
        /// Place a new order
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXBalance>>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal quantity, decimal? price = null, bool? reduceOnly = null, bool? immediateOrCancel = null, bool? postOnly = null, string? clientId = null, bool? rejectOnPriceBand = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "market", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) },
                { "size", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("reduceOnly", reduceOnly);
            parameters.AddOptionalParameter("ioc", immediateOrCancel);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("clientId", clientId);
            parameters.AddOptionalParameter("rejectOnPriceBand", rejectOnPriceBand);

            return await SendFTXRequest<IEnumerable<FTXBalance>>(GetUri("orders"), HttpMethod.Post, ct, parameters, signed: true);
        }

        private async Task<WebCallResult<T>> SendFTXRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, bool checkResult = true, PostParameters? postPosition = null, ArrayParametersSerialization? arraySerialization = null, int credits = 1, JsonSerializer? deserializer = null) where T : class
        {
            var result = await SendRequest< FTXResult<T>>(uri, method, cancellationToken, parameters, signed, checkResult, postPosition, arraySerialization, credits, deserializer);
            if(result)
                return result.As(result.Data.Result);

            return WebCallResult<T>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
        }


        private Uri GetUri(string path)
        {
            return new Uri(BaseAddress + path);
        }

        protected override Error ParseErrorResponse(JToken error)
        {
            if(error["error"] == null)
                return new ServerError(error.ToString());

            return new ServerError(error["error"].ToString());
        }

        #region common interface
        public string GetSymbolName(string baseAsset, string quoteAsset)
        {
            return baseAsset + "/" + quoteAsset;
        }

        public Task<WebCallResult<IEnumerable<ICommonSymbol>>> GetSymbolsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonTicker>>> GetTickersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonTicker>> GetTickerAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonKline>>> GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonOrderBook>> GetOrderBookAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonRecentTrade>>> GetRecentTradesAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonOrderId>> PlaceOrderAsync(string symbol, IExchangeClient.OrderSide side, IExchangeClient.OrderType type, decimal quantity, decimal? price = null, string? accountId = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonOrder>> GetOrderAsync(string orderId, string? symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonTrade>>> GetTradesAsync(string orderId, string? symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonOrder>>> GetOpenOrdersAsync(string? symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonOrder>>> GetClosedOrdersAsync(string? symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonOrderId>> CancelOrderAsync(string orderId, string? symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonBalance>>> GetBalancesAsync(string? accountId = null)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}
