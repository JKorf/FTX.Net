using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using FTX.Net.Converters;
using FTX.Net.Enums;
using FTX.Net.Objects;
using FTX.Net.Objects.Futures;
using FTX.Net.Objects.Spot;
using FTX.Net.SubClients;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Interfaces;
using FTX.Net.Interfaces.SubClients;
using System.Net;
using CryptoExchange.Net.Authentication;

namespace FTX.Net
{
    /// <summary>
    /// Client for interacting with the FTX API
    /// </summary>
    public class FTXClient : RestClient, IExchangeClient, IFTXClient
    {
        private static FTXClientOptions _defaultOptions = new FTXClientOptions();

        private const string SubaccountHeaderName = "FTX-SUBACCOUNT";

        /// <inheritDoc />
        public event Action<ICommonOrderId>? OnOrderPlaced;
        /// <inheritDoc />
        public event Action<ICommonOrderId>? OnOrderCanceled;

        private static FTXClientOptions DefaultOptions => _defaultOptions.Copy<FTXClientOptions>();

        private readonly string _affiliateCode;
        private FTXClientOptions _options;

        /// <inheritdoc />
        public IFTXSubClientConvert Convert { get; }
        /// <inheritdoc />
        public IFTXSubClientOptions Options { get; }
        /// <inheritdoc />
        public IFTXSubClientLeveragedTokens LeveragedTokens { get; }
        /// <inheritdoc />
        public IFTXSubClientStaking Staking { get; }
        /// <inheritdoc />
        public IFTXSubClientMargin Margin { get; }
        /// <inheritdoc />
        public IFTXSubClientNft NFT { get; }
        /// <inheritdoc />
        public IFTXSubClientPay FTXPay { get; }
        /// <inheritdoc />
        public IFTXSubClientSubaccounts Subaccounts { get; }

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of FTXClient using the default options
        /// </summary>
        public FTXClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of FTXClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public FTXClient(FTXClientOptions options) : base("FTX", options, options.ApiCredentials == null ? null
            : new FTXAuthenticationProvider(options.ApiCredentials))
        {
            if (options == null)
                throw new ArgumentException("Cant pass null options, use empty constructor for default");

            _options = options;
            _affiliateCode = options.AffiliateCode;
            if (!string.IsNullOrEmpty(options.SubaccountName)) 
            {
                StandardRequestHeaders = new Dictionary<string, string>
                {
                    { SubaccountHeaderName, WebUtility.UrlEncode(options.SubaccountName) }
                };
            }
            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InBody;

            Convert = new FTXSubClientConvert(this);
            Options = new FTXSubClientOptions(this);
            Margin = new FTXSubClientMargin(this);
            Staking = new FTXSubClientStaking(this);
            LeveragedTokens = new FTXSubClientLeveragedTokens(this);
            NFT = new FTXSubClientNft(this);
            FTXPay = new FTXSubClientPay(this);
            Subaccounts = new FTXSubClientSubaccounts(this);
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
        public static void SetDefaultOptions(FTXClientOptions newDefaultOptions)
        {
            _defaultOptions = newDefaultOptions;
        }

        internal static Dictionary<string, string>? GetSubaccountHeader(string? subaccountName) => subaccountName == null? null: new Dictionary<string, string>
            {
                { SubaccountHeaderName, WebUtility.UrlEncode(subaccountName) }
            };


        /// <summary>
        /// Get the server time
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            return await SendFTXRequest<DateTime>(new Uri("https://otc.ftx.com/api/time"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #region Markets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXSymbol>>> GetSymbolsAsync(CancellationToken ct = default)
        {
            return await SendFTXRequest<IEnumerable<FTXSymbol>>(GetUri("markets"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default)
        {
            return await SendFTXRequest<FTXSymbol>(GetUri("markets/" + symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXOrderbook>> GetOrderBookAsync(string symbol, int depth, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("depth", depth);
            return await SendFTXRequest<FTXOrderbook>(GetUri($"markets/{symbol}/orderbook"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXTrade>>> GetTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXTrade>>(GetUri($"markets/{symbol}/trades"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("resolution", GetResolutionFromKlineInterval(interval));
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXKline>>(GetUri($"markets/{symbol}/candles"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Futures

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXFuture>>> GetFuturesAsync(CancellationToken ct = default)
        {
            return await SendFTXRequest<IEnumerable<FTXFuture>>(GetUri("futures"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXFuture>> GetFutureAsync(string future, CancellationToken ct = default)
        {
            return await SendFTXRequest<FTXFuture>(GetUri("futures/" + future), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXFutureStat>> GetFutureStatsAsync(string future, CancellationToken ct = default)
        {
            return await SendFTXRequest<FTXFutureStat>(GetUri($"futures/{future}/stats"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXFundingRate>>> GetFundingRatesAsync(string? future = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            AddFilter(parameters, startTime, endTime);
            parameters.AddOptionalParameter("future", future);
            return await SendFTXRequest<IEnumerable<FTXFundingRate>>(GetUri($"funding_rates"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, decimal>>> GetIndexWeightsAsync(string index, CancellationToken ct = default)
        {
            return await SendFTXRequest<Dictionary<string, decimal>>(GetUri($"indexes/{index}/weights"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXFuture>>> GetExpiredFuturesAsync(CancellationToken ct = default)
        {
            return await SendFTXRequest<IEnumerable<FTXFuture>>(GetUri("expired_futures"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXKline>>> GetIndexKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("resolution", GetResolutionFromKlineInterval(interval));
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXKline>>(GetUri($"indexes/{symbol}/candles"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Account
        /// <inheritdoc />
        public async Task<WebCallResult<FTXAccountInfo>> GetAccountInfoAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await SendFTXRequest<FTXAccountInfo>(GetUri("account"), HttpMethod.Get, ct, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXPosition>>> GetPositionsAsync(bool? showAveragePrice = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("showAvgPrice", showAveragePrice);
            return await SendFTXRequest<IEnumerable<FTXPosition>>(GetUri("positions"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> ChangeAccountLeverageAsync(decimal leverage, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("leverage", leverage.ToString(CultureInfo.InvariantCulture));
            return await SendFTXRequest(GetUri("account/leverage"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        #endregion

        #region Wallet

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXAsset>>> GetAssetsAsync(CancellationToken ct = default)
        {
            return await SendFTXRequest<IEnumerable<FTXAsset>>(GetUri("wallet/coins"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXBalance>>> GetBalancesAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await SendFTXRequest<IEnumerable<FTXBalance>>(GetUri("wallet/balances"), HttpMethod.Get, ct, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, IEnumerable<FTXBalance>>>> GetAllAccountBalancesAsync(CancellationToken ct = default)
        {
            return await SendFTXRequest<Dictionary<string, IEnumerable<FTXBalance>>>(GetUri("wallet/all_balances"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("method", network);
            return await SendFTXRequest<FTXDepositAddress>(GetUri("wallet/deposit_address/" + asset), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXDeposit>>> GetDepositHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXDeposit>>(GetUri("wallet/deposits"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXWithdrawal>>> GetWithdrawalHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXWithdrawal>>(GetUri("wallet/withdrawals"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXWithdrawal>> WithdrawAsync(string asset, decimal quantity, string address, string? tag = null, string? network = null, string? password = null, string? code = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("coin", asset);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("address", address);
            parameters.AddOptionalParameter("tag", tag);
            parameters.AddOptionalParameter("method", network);
            parameters.AddOptionalParameter("password", password);
            parameters.AddOptionalParameter("code", code);
            return await SendFTXRequest<FTXWithdrawal>(GetUri("wallet/withdrawals"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXAirdrop>>> GetAirdropsAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXAirdrop>>(GetUri("wallet/airdrops"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXWithdrawalFee>> GetWithdrawalFeesAsync(string asset, decimal quantity, string address, string? tag = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("coin", asset);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("address", address);
            parameters.AddOptionalParameter("tag", tag);
            return await SendFTXRequest<FTXWithdrawalFee>(GetUri("wallet/withdrawal_fee"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXSavedAddress>>> GetSavedAddressesAsync(string? asset = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("coin", asset);
            return await SendFTXRequest<IEnumerable<FTXSavedAddress>>(GetUri("wallet/saved_addresses"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXSavedAddress>> CreateSavedAddressAsync(string asset, string address, string addressName, bool isPrimeTrust, string? tag = null, string? code = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("coin", asset);
            parameters.AddParameter("address", address);
            parameters.AddParameter("addressName", addressName);
            parameters.AddParameter("isPrimeTrust", isPrimeTrust);
            parameters.AddOptionalParameter("tag", tag);
            parameters.AddOptionalParameter("code", code);
            return await SendFTXRequest<FTXSavedAddress>(GetUri("wallet/saved_addresses"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<string>> DeleteSavedAddressAsync(long savedAddressId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await SendFTXRequest<string>(GetUri("wallet/saved_addresses/" + savedAddressId), HttpMethod.Delete, ct, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        #endregion

        #region Orders

        /// <inheritdoc />
        public async Task<WebCallResult<FTXOrder>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal quantity, decimal? price = null, bool? reduceOnly = null, bool? immediateOrCancel = null, bool? postOnly = null, string? clientOrderId = null, bool? rejectOnPriceBand = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "market", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) },
                { "size", quantity.ToString(CultureInfo.InvariantCulture) },
                { "price", price?.ToString(CultureInfo.InvariantCulture)! } // Should still be send even when it's null
            };

            parameters.AddOptionalParameter("reduceOnly", reduceOnly);
            parameters.AddOptionalParameter("ioc", immediateOrCancel);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("clientId", clientOrderId);
            parameters.AddOptionalParameter("rejectOnPriceBand", rejectOnPriceBand);
            parameters.AddOptionalParameter("externalReferralProgram", _affiliateCode);

            var result = await SendFTXRequest<FTXOrder>(GetUri("orders"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
            if (result)
                OnOrderPlaced?.Invoke(result.Data);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXTriggerOrder>> PlaceTriggerOrderAsync(
            // Basic params
            string symbol, 
            OrderSide side, 
            TriggerOrderType type, 
            decimal quantity,
            bool? reduceOnly = null,
            bool? retryUntilFilled = null,

            // Stop loss / take profit params
            decimal? triggerPrice = null, 
            decimal? orderPrice = null, 
            
            // Trailing stop params
            decimal? trailValue = null,

            string? subaccountName = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "market", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new TriggerOrderTypeConverter(false)) },
                { "size", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("triggerPrice", triggerPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("orderPrice", orderPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("trailValue", trailValue?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("reduceOnly", reduceOnly);
            parameters.AddOptionalParameter("retryUntilFilled", retryUntilFilled);
            parameters.AddOptionalParameter("externalReferralProgram", _affiliateCode);

            return await SendFTXRequest<FTXTriggerOrder>(GetUri("conditional_orders"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXOrder>> ModifyOrderAsync(long orderId, decimal? price = null, decimal? quantity = null, string? clientOrderId = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("clientId", clientOrderId);
            parameters.AddOptionalParameter("externalReferralProgram", _affiliateCode);

            return await SendFTXRequest<FTXOrder>(GetUri($"orders/{orderId}/modify"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXTriggerOrder>> ModifyTriggerOrderAsync(long orderId, decimal? quantity = null, decimal ? triggerPrice = null, decimal? orderPrice = null, decimal? trailingValue = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("triggerPrice", triggerPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("orderPrice", orderPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("trailValue", trailingValue?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("externalReferralProgram", _affiliateCode);

            return await SendFTXRequest<FTXTriggerOrder>(GetUri($"conditional_orders/{orderId}/modify"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXOrder>> ModifyOrderByClientOrderIdAsync(long clientOrderId, decimal? price = null, decimal? quantity = null, string? newClientOrderId = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("clientId", newClientOrderId);
            parameters.AddOptionalParameter("externalReferralProgram", _affiliateCode);

            return await SendFTXRequest<FTXOrder>(GetUri($"orders/by_client_id/{clientOrderId}/modify"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXOrder>> GetOrderAsync(long orderId, string? subaccountName = null, CancellationToken ct = default)
        {           
            return await SendFTXRequest<FTXOrder>(GetUri("orders/" + orderId), HttpMethod.Get, ct, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await SendFTXRequest<FTXOrder>(GetUri("orders/by_client_id/" + clientOrderId), HttpMethod.Get, ct, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXTriggerOrderTrigger>>> GetTriggerOrderTriggersAsync(long orderId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await SendFTXRequest<IEnumerable<FTXTriggerOrderTrigger>>(GetUri($"conditional_orders/{orderId}/triggers"), HttpMethod.Get, ct, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXOrder>>> GetOpenOrdersAsync(string? symbol = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            return await SendFTXRequest<IEnumerable<FTXOrder>>(GetUri("orders"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXTriggerOrder>>> GetOpenTriggerOrdersAsync(string? symbol = null, TriggerOrderType? type = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            parameters.AddOptionalParameter("type", type == null ? null: JsonConvert.SerializeObject(type, new TriggerOrderTypeConverter(false)));
            return await SendFTXRequest<IEnumerable<FTXTriggerOrder>>(GetUri("conditional_orders"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXOrder>>> GetOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXOrder>>(GetUri("orders/history"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXTriggerOrder>>> GetTriggerOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, OrderSide? side = null, TriggerOrderType? type = null, OrderType? orderType = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new TriggerOrderTypeConverter(false)));
            parameters.AddOptionalParameter("orderType", orderType == null ? null : JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)));
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXTriggerOrder>>(GetUri("conditional_orders/history"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<string>> CancelOrderAsync(long orderId, string? subaccountName = null, CancellationToken ct = default)
        {
            var result = await SendFTXRequest<string>(GetUri("orders/" + orderId), HttpMethod.Delete, ct, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);            
            if (result)
                OnOrderCanceled?.Invoke(new FTXOrder() { Id = orderId });
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<string>> CancelTriggerOrderAsync(long orderId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await SendFTXRequest<string>(GetUri("conditional_orders/" + orderId), HttpMethod.Delete, ct, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<string>> CancelOrderByClientIdAsync(string clientOrderId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await SendFTXRequest<string>(GetUri("orders/by_client_id/" + clientOrderId), HttpMethod.Delete, ct, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<string>> CancelAllOrdersAsync(string? symbol = null, OrderSide? side = null, bool? conditionalOrdersOnly = null, bool? limitOrdersOnly = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("conditionalOrdersOnly", conditionalOrdersOnly);
            parameters.AddOptionalParameter("limitOrdersOnly", limitOrdersOnly);
            return await SendFTXRequest<string>(GetUri("orders"), HttpMethod.Delete, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }
        #endregion

        #region Fills

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXUserTrade>>> GetUserTradesAsync(string? symbol = null, long? orderId = null, bool? ascendingOrder = null, DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            parameters.AddOptionalParameter("order", ascendingOrder == true ? "asc": null);
            parameters.AddOptionalParameter("orderId", orderId);
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXUserTrade>>(GetUri("fills"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        #endregion

        #region Funding Payments

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXFundingPayment>>> GetFundingPaymentsAsync(string? future = null, DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("future", future);
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXFundingPayment>>(GetUri("funding_payments"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        #endregion

        #region private

        internal async Task<WebCallResult<T>> SendFTXRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, bool checkResult = true, HttpMethodParameterPosition? postPosition = null, ArrayParametersSerialization? arraySerialization = null, int credits = 1, JsonSerializer? deserializer = null, Dictionary<string, string>? additionalHeaders = null)
        {
            if (signed)
                await FTXTimestampProvider.UpdateTimeAsync(this, log, _options).ConfigureAwait(false);

            var result = await SendRequestAsync<FTXResult<T>>(uri, method, cancellationToken, parameters, signed, checkResult, postPosition, arraySerialization, credits, deserializer, additionalHeaders).ConfigureAwait(false);
            if (result)
                return result.As(result.Data.Result);

            return WebCallResult<T>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);
        }

        internal async Task<WebCallResult> SendFTXRequest(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, bool checkResult = true, HttpMethodParameterPosition? postPosition = null, ArrayParametersSerialization? arraySerialization = null, int credits = 1, JsonSerializer? deserializer = null, Dictionary<string, string>? additionalHeaders = null)
        {
            var result = await SendRequestAsync<FTXResult>(uri, method, cancellationToken, parameters, signed, checkResult, postPosition, arraySerialization, credits, deserializer, additionalHeaders).ConfigureAwait(false);
            if (result)
                return new WebCallResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return WebCallResult.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);
        }

        internal CallResult<T> DeserializeInternal<T>(string data)
        {
            return base.Deserialize<T>(data);
        }

        internal Uri GetUri(string path)
        {
            return new Uri(BaseAddress + path);
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(JToken error)
        {
            if(error["error"] == null)
                return new ServerError(error.ToString());

            return new ServerError(error["error"]!.ToString());
        }

        internal static void AddFilter(Dictionary<string, object> parameters, DateTime? startTime, DateTime? endTime)
        {
            parameters.AddOptionalParameter("start_time", startTime == null ? null : JsonConvert.SerializeObject(startTime, new TimestampSecondsConverter()));
            parameters.AddOptionalParameter("end_time", endTime == null ? null : JsonConvert.SerializeObject(endTime, new TimestampSecondsConverter()));
        }

        private static int GetResolutionFromKlineInterval(KlineInterval interval)
        {
            return interval switch
            {
                KlineInterval.FifteenSeconds => 15,
                KlineInterval.OneMinute => 60,
                KlineInterval.FiveMinutes => 300,
                KlineInterval.FifteenMinutes => 900,
                KlineInterval.OneHour => 3600,
                KlineInterval.FourHours => 14400,
                KlineInterval.OneDay => 86400,
                KlineInterval.OneWeek => 86400 * 7,
                KlineInterval.OneMonth => 86400 * 30,
                _ => throw new Exception("Unknown kline interval"),
            };
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
            var symbols = await GetSymbolsAsync().ConfigureAwait(false);
            return symbols.As<IEnumerable<ICommonSymbol>>(symbols.Data);
        }

        Task<WebCallResult<IEnumerable<ICommonTicker>>> IExchangeClient.GetTickersAsync()
        {
            throw new InvalidOperationException("FTX API has no support for getting High/Low/Volume stats for all symbols in a call");
        }

        async Task<WebCallResult<ICommonTicker>> IExchangeClient.GetTickerAsync(string symbol)
        {
            var klines = await GetKlinesAsync(symbol, KlineInterval.OneHour).ConfigureAwait(false);
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
            var klines = await GetKlinesAsync(symbol, GetKlineIntervalFromTimeSpan(timespan), startTime, endTime).ConfigureAwait(false);
            return klines.As<IEnumerable<ICommonKline>>(klines.Data);
        }

        async Task<WebCallResult<ICommonOrderBook>> IExchangeClient.GetOrderBookAsync(string symbol)
        {
            var book = await GetOrderBookAsync(symbol, 100).ConfigureAwait(false);
            return book.As<ICommonOrderBook>(book.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonRecentTrade>>> IExchangeClient.GetRecentTradesAsync(string symbol)
        {
            var trades = await GetTradeHistoryAsync(symbol).ConfigureAwait(false);
            return trades.As<IEnumerable<ICommonRecentTrade>>(trades.Data);
        }

        async Task<WebCallResult<ICommonOrderId>> IExchangeClient.PlaceOrderAsync(string symbol, IExchangeClient.OrderSide side, IExchangeClient.OrderType type, decimal quantity, decimal? price = null, string? accountId = null)
        {
            var trades = await PlaceOrderAsync(
                symbol,
                side == IExchangeClient.OrderSide.Buy ? OrderSide.Buy: OrderSide.Sell,
                type == IExchangeClient.OrderType.Limit ? OrderType.Limit : OrderType.Market,
                quantity,
                price                
                ).ConfigureAwait(false);
            return trades.As<ICommonOrderId>(trades.Data);
        }

        async Task<WebCallResult<ICommonOrder>> IExchangeClient.GetOrderAsync(string orderId, string? symbol = null)
        {
            var trades = await GetOrderAsync(long.Parse(orderId)).ConfigureAwait(false);
            return trades.As<ICommonOrder>(trades.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonTrade>>> IExchangeClient.GetTradesAsync(string orderId, string? symbol = null)
        {
            var trades = await GetUserTradesAsync(orderId: long.Parse(orderId)).ConfigureAwait(false);
            return trades.As<IEnumerable<ICommonTrade>>(trades.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetOpenOrdersAsync(string? symbol = null)
        {
            var trades = await GetOpenOrdersAsync(symbol).ConfigureAwait(false);
            return trades.As<IEnumerable<ICommonOrder>>(trades.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetClosedOrdersAsync(string? symbol = null)
        {
            var trades = await GetOrdersAsync(symbol).ConfigureAwait(false);
            return trades.As<IEnumerable<ICommonOrder>>(trades.Data.Where(o => o.Status == OrderStatus.Closed));
        }

        async Task<WebCallResult<ICommonOrderId>> IExchangeClient.CancelOrderAsync(string orderId, string? symbol = null)
        {
            var trades = await CancelOrderAsync(long.Parse(orderId)).ConfigureAwait(false);
            return trades.As<ICommonOrderId>(new FTXOrder { Id = long.Parse(orderId) });
        }

        async Task<WebCallResult<IEnumerable<ICommonBalance>>> IExchangeClient.GetBalancesAsync(string? accountId = null)
        {
            var balances = await GetBalancesAsync().ConfigureAwait(false);
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
        #endregion
    }
}
