using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using FTX.Net.Converters;
using FTX.Net.Enums;
using FTX.Net.Objects;
using FTX.Net.Objects.Convert;
using FTX.Net.Objects.Futures;
using FTX.Net.Objects.LeveragedTokens;
using FTX.Net.Objects.Options;
using FTX.Net.Objects.Spot;
using FTX.Net.Objects.Staking;
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

namespace FTX.Net
{
    public class FTXClient : RestClient, IExchangeClient
    {
        private static FTXClientOptions defaultOptions = new FTXClientOptions();

        public event Action<ICommonOrderId>? OnOrderPlaced;
        public event Action<ICommonOrderId>? OnOrderCanceled;

        private static FTXClientOptions DefaultOptions => defaultOptions.Copy<FTXClientOptions>();

        private string _affiliateCode;

        public FTXSubClientConvert Convert { get; }
        public FTXSubClientOptions Options { get; }
        public FTXSubClientLeveragedTokens LeveragedTokens { get; }
        public FTXSubClientStaking Staking { get; }
        public FTXSubClientMargin Margin { get; }
        public FTXSubClientNFT NFT { get; }

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

            _affiliateCode = options.AffiliateCode;

            Convert = new FTXSubClientConvert(this);
            Options = new FTXSubClientOptions(this);
            Margin = new FTXSubClientMargin(this);
            Staking = new FTXSubClientStaking(this);
            LeveragedTokens = new FTXSubClientLeveragedTokens(this);
            NFT = new FTXSubClientNFT(this);
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

        #region Markets

        /// <summary>
        /// Get the list of supported symbols
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXSymbol>>> GetSymbolsAsync(CancellationToken ct = default)
        {
            return await SendFTXRequest<IEnumerable<FTXSymbol>>(GetUri("markets"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get market info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXSymbol>> GetSymbolAsync(string market, CancellationToken ct = default)
        {
            return await SendFTXRequest<FTXSymbol>(GetUri("markets/" + market), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the orderbook for a symbol
        /// </summary>
        /// <param name="symbol">Symbol to get the book for</param>
        /// <param name="depth">Depth of the book</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXOrderbook>> GetOrderBookAsync(string symbol, int depth, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("depth", depth);
            return await SendFTXRequest<FTXOrderbook>(GetUri($"markets/{symbol}/orderbook"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get trades for a symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXTrade>>> GetTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXTrade>>(GetUri($"markets/{symbol}/trades"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get klines for a symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("resolution", GetResolutionFromKlineInterval(interval));
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXKline>>(GetUri($"markets/{symbol}/candles"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Account

        /// <summary>
        /// Get account info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default)
        {
            return await SendFTXRequest<FTXAccountInfo>(GetUri("account"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get positions
        /// </summary>
        /// <param name="showAveragePrice"></param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXPosition>>> GetPositionsAsync(bool? showAveragePrice = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("showAvgPrice", showAveragePrice);
            return await SendFTXRequest<IEnumerable<FTXPosition>>(GetUri("positions"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Change account leverage
        /// </summary>
        /// <param name="leverage">Desired acccount-wide leverage setting</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult> ChangeAccountLeverageAsync(decimal leverage, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("leverage", leverage.ToString(CultureInfo.InvariantCulture));
            return await SendFTXRequest(GetUri("account/leverage"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        #endregion

        #region Wallet

        /// <summary>
        /// Get the list asset
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXAsset>>> GetAssetsAsync(CancellationToken ct = default)
        {
            return await SendFTXRequest<IEnumerable<FTXAsset>>(GetUri("wallet/coins"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a list of balances
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXBalance>>> GetBalancesAsync(CancellationToken ct = default)
        {
            return await SendFTXRequest<IEnumerable<FTXBalance>>(GetUri("wallet/balances"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a list of balances, including master and subaccounts
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<Dictionary<string, IEnumerable<FTXBalance>>>> GetAllAccountBalancesAsync(CancellationToken ct = default)
        {
            return await SendFTXRequest<Dictionary<string, IEnumerable<FTXBalance>>>(GetUri("wallet/all_balances"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get deposit address for an asset
        /// </summary>
        /// <param name="asset">Asset to get address for/param>
        /// <param name="network">The network to use</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("method", network);
            return await SendFTXRequest<FTXDepositAddress>(GetUri("wallet/deposit_address/" + asset), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get deposit history
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXDeposit>>> GetDepositHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXDeposit>>(GetUri("wallet/deposits"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get withdrawal history
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXWithdrawal>>> GetWithdrawalHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXWithdrawal>>(GetUri("wallet/withdrawals"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Submit a withdraw request
        /// </summary>
        /// <param name="asset">Asset to withdraw</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="address">Address to withdraw to</param>
        /// <param name="tag">Address tag</param>
        /// <param name="network">Network to us</param>
        /// <param name="password">Withdrawal password if required</param>
        /// <param name="code">Two factor authentication code if required</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXWithdrawal>> WithdrawAsync(string asset, decimal quantity, string address, string? tag = null, string? network = null, string? password = null, string? code = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("coin", asset);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("address", address);
            parameters.AddOptionalParameter("tag", tag);
            parameters.AddOptionalParameter("method", network);
            parameters.AddOptionalParameter("password", password);
            parameters.AddOptionalParameter("code", code);
            return await SendFTXRequest<FTXWithdrawal>(GetUri("wallet/withdrawals"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get airdrops
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXAirdrop>>> GetAirdropsAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXAirdrop>>(GetUri("wallet/airdrops"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get withdrawal fees
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXWithdrawalFee>> GetWithdrawalFeesAsync(string asset, decimal quantity, string address, string? tag = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("coin", asset);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("address", address);
            parameters.AddOptionalParameter("tag", tag);
            return await SendFTXRequest<FTXWithdrawalFee>(GetUri("wallet/withdrawal_fee"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get saved addresses
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXSavedAddress>>> GetSavedAddressesAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("coin", asset);
            return await SendFTXRequest<IEnumerable<FTXSavedAddress>>(GetUri("wallet/saved_addresses"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a saved address
        /// </summary>
        /// <param name="asset">Asset the address is for</param>
        /// <param name="address">The address</param>
        /// <param name="addressName">Name of the address</param>
        /// <param name="isPrimeTrust">Is prime trust</param>
        /// <param name="tag">Address tag</param>
        /// <param name="code">2FA code if needed</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXSavedAddress>> CreateSavedAddressAsync(string asset, string address, string addressName, bool isPrimeTrust, string? tag = null, string? code = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("coin", asset);
            parameters.AddParameter("address", address);
            parameters.AddParameter("addressName", addressName);
            parameters.AddParameter("isPrimeTrust", isPrimeTrust);
            parameters.AddOptionalParameter("tag", tag);
            parameters.AddOptionalParameter("code", code);
            return await SendFTXRequest<FTXSavedAddress>(GetUri("wallet/saved_addresses"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a saved address
        /// </summary>
        /// <param name="savedAddressId">Id of the saved address to delete</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<string>> DeleteSavedAddressAsync(long savedAddressId, CancellationToken ct = default)
        {
            return await SendFTXRequest<string>(GetUri("wallet/saved_addresses/" + savedAddressId), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        #endregion

        #region Orders

        /// <summary>
        /// Place a new order
        /// </summary>
        /// <param name="symbol">The symbol to place on</param>
        /// <param name="side">The side of the order</param>
        /// <param name="type">The type of order</param>
        /// <param name="quantity">The quantity to buy or sell</param>
        /// <param name="price">The price of the order (null for market orders)</param>
        /// <param name="reduceOnly">Reduce only</param>
        /// <param name="immediateOrCancel">Immediate or cancel</param>
        /// <param name="postOnly">Post only</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="rejectOnPriceBand">If the order should be rejected if its price would instead be adjusted due to price bands</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXOrder>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal quantity, decimal? price = null, bool? reduceOnly = null, bool? immediateOrCancel = null, bool? postOnly = null, string? clientOrderId = null, bool? rejectOnPriceBand = null, CancellationToken ct = default)
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
            parameters.AddOptionalParameter("clientId", clientOrderId);
            parameters.AddOptionalParameter("rejectOnPriceBand", rejectOnPriceBand);
            parameters.AddOptionalParameter("externalReferralProgram", _affiliateCode);

            return await SendFTXRequest<FTXOrder>(GetUri("orders"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Place a new trigger order
        /// </summary>
        /// <param name="symbol">The symbol to place on</param>
        /// <param name="side">The side of the order</param>
        /// <param name="type">The trigger type</param>
        /// <param name="quantity">The quantity to buy or sell</param>
        /// <param name="triggerPrice">Trigger price for stop loss/take profit</param>
        /// <param name="orderPrice">Order price, specifying this makes the order a limit order</param>
        /// <param name="trailValue">Tailing value for trailing stop orders, negative for sell, positive for buy</param>
        /// <param name="reduceOnly">Reduce only</param>
        /// <param name="retryUntilFilled">Whether or not to keep re-triggering until filled. optional, default true for market orders</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
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

            return await SendFTXRequest<FTXTriggerOrder>(GetUri("conditional_orders"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Modify an order. Will internally cancel the original order and place a new order with the new price/quantity. The new order will have a new order id. Note: there's a chance that the order meant to be cancelled gets filled and its replacement still gets placed.
        /// </summary>
        /// <param name="orderId">Id of order to modify</param>
        /// <param name="price">New price of the order</param>
        /// <param name="quantity">New quantity of the order</param>
        /// <param name="clientOrderID">New client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXOrder>> ModifyOrderAsync(long orderId, decimal? price = null, decimal? quantity = null, string? clientOrderID = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("clientId", clientOrderID);
            parameters.AddOptionalParameter("externalReferralProgram", _affiliateCode);

            return await SendFTXRequest<FTXOrder>(GetUri($"orders/{orderId}/modify"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Modify a trigger order. Will internally cancel the original order and place a new order with the new price/quantity. The new order will have a new order id. Note: there's a chance that the order meant to be cancelled gets filled and its replacement still gets placed.
        /// </summary>
        /// <param name="orderId">Id of order to modify</param>
        /// <param name="price">New price of the order</param>
        /// <param name="quantity">New quantity of the order</param>
        /// <param name="clientOrderID">New client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXTriggerOrder>> ModifyTriggerOrderAsync(long orderId, decimal? quantity = null, decimal ? triggerPrice = null, decimal? orderPrice = null, decimal? trailingValue = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("triggerPrice", triggerPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("orderPrice", orderPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("trailValue", trailingValue?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("externalReferralProgram", _affiliateCode);

            return await SendFTXRequest<FTXTriggerOrder>(GetUri($"conditional_orders/{orderId}/modify"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Modify an order. Will internally cancel the original order and place a new order with the new price/quantity. The new order will have a new order id. Note: there's a chance that the order meant to be cancelled gets filled and its replacement still gets placed.
        /// </summary>
        /// <param name="clientOrderId">Client order id of order to modify</param>
        /// <param name="price">New price of the order</param>
        /// <param name="quantity">New quantity of the order</param>
        /// <param name="clientOrderID">New client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXOrder>> ModifyOrderByClientOrderIdAsync(long clientOrderId, decimal? price = null, decimal? quantity = null, string? clientOrderID = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("clientId", clientOrderID);
            parameters.AddOptionalParameter("externalReferralProgram", _affiliateCode);

            return await SendFTXRequest<FTXOrder>(GetUri($"orders/by_client_id/{clientOrderId}/modify"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the status of an order
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
        {           
            return await SendFTXRequest<FTXOrder>(GetUri("orders/" + orderId), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the status of an order
        /// </summary>
        /// <param name="clientOrderId">Client order id of the order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            return await SendFTXRequest<FTXOrder>(GetUri("orders/by_client_id/" + clientOrderId), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a list of open trigger orders
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXTriggerOrderTrigger>>> GetTriggerOrderTriggers(long orderId, CancellationToken ct = default)
        {
            return await SendFTXRequest<IEnumerable<FTXTriggerOrderTrigger>>(GetUri($"conditional_orders/{orderId}/triggers"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a list of open orders
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXOrder>>> GetOpenOrderAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            return await SendFTXRequest<IEnumerable<FTXOrder>>(GetUri("orders"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a list of open trigger orders
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXTriggerOrder>>> GetOpenTriggerOrderAsync(string? symbol = null, TriggerOrderType? type = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            parameters.AddOptionalParameter("type", type == null ? null: JsonConvert.SerializeObject(type, new TriggerOrderTypeConverter(false)));
            return await SendFTXRequest<IEnumerable<FTXTriggerOrder>>(GetUri("conditional_orders"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get list of orders
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXOrder>>> GetOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXOrder>>(GetUri("orders/history"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get list of trigger orders
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by trigger type</param>
        /// <param name="orderType">Filter by order type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXTriggerOrder>>> GetTriggerOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, OrderSide? side = null, TriggerOrderType? type = null, OrderType? orderType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new TriggerOrderTypeConverter(false)));
            parameters.AddOptionalParameter("orderType", orderType == null ? null : JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)));
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXTriggerOrder>>(GetUri("conditional_orders/history"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<string>> CancelOrderAsync(long orderId, CancellationToken ct = default)
        {
            return await SendFTXRequest<string>(GetUri("orders/" + orderId), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel a trigger order
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<string>> CancelTriggerOrderAsync(long orderId, CancellationToken ct = default)
        {
            return await SendFTXRequest<string>(GetUri("conditional_orders/" + orderId), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="clientOrderId">Client order id of the order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<string>> CancelOrderByClientIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            return await SendFTXRequest<string>(GetUri("orders/by_client_id/" + clientOrderId), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel all orders matching the parameters
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="side">Filter by side</param>
        /// <param name="conditionalOrdersOnly">Only cancel conditional orders</param>
        /// <param name="limitOrdersOnly">Only cancel limit orders</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<string>> CancelAllOrdersAsync(string? symbol = null, OrderSide? side = null, bool? conditionalOrdersOnly = null, bool? limitOrdersOnly = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("conditionalOrdersOnly", conditionalOrdersOnly);
            parameters.AddOptionalParameter("limitOrdersOnly", limitOrdersOnly);
            return await SendFTXRequest<string>(GetUri("orders"), HttpMethod.Delete, ct, parameters, signed: true).ConfigureAwait(false);
        }
        #endregion

        #region Fills

        /// <summary>
        /// Get list of trades based on the input parameters
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="ascendingOrder">Return results in ascending order in time</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXUserTrade>>> GetUserTradesAsync(string? symbol = null, long? orderId = null, bool? ascendingOrder = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            parameters.AddOptionalParameter("order", ascendingOrder == true ? "asc": null);
            parameters.AddOptionalParameter("orderId", orderId);
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXUserTrade>>(GetUri("fills"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        #endregion

        #region Funding Payments

        /// <summary>
        /// Get list of funding payments
        /// </summary>
        /// <param name="future">Filter by future</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXFundingPayment>>> GetFundingPaymentsAsync(string? future = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("future", future);
            AddFilter(parameters, startTime, endTime);
            return await SendFTXRequest<IEnumerable<FTXFundingPayment>>(GetUri("funding_payments"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        #endregion

        #region Spot Margin

        // TODO

        #endregion

        #region NFTs

        // TODO

        #endregion

        #region FTX pay

        // TODO

        #endregion

        #region private
        internal async Task<WebCallResult<T>> SendFTXRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, bool checkResult = true, PostParameters? postPosition = null, ArrayParametersSerialization? arraySerialization = null, int credits = 1, JsonSerializer? deserializer = null) where T : class
        {
            var result = await SendRequestAsync<FTXResult<T>>(uri, method, cancellationToken, parameters, signed, checkResult, postPosition, arraySerialization, credits, deserializer).ConfigureAwait(false);
            if (result)
                return result.As(result.Data.Result);

            return WebCallResult<T>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
        }

        internal async Task<WebCallResult> SendFTXRequest(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, bool checkResult = true, PostParameters? postPosition = null, ArrayParametersSerialization? arraySerialization = null, int credits = 1, JsonSerializer? deserializer = null)
        {
            var result = await SendRequestAsync<FTXResult>(uri, method, cancellationToken, parameters, signed, checkResult, postPosition, arraySerialization, credits, deserializer).ConfigureAwait(false);
            if (result)
                return new WebCallResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return WebCallResult.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
        }

        internal CallResult<T> DeserializeInternal<T>(string data)
        {
            return base.Deserialize<T>(data);
        }

        internal Uri GetUri(string path)
        {
            return new Uri(BaseAddress + path);
        }

        protected override Error ParseErrorResponse(JToken error)
        {
            if(error["error"] == null)
                return new ServerError(error.ToString());

            return new ServerError(error["error"].ToString());
        }

        internal void AddFilter(Dictionary<string, object> parameters, DateTime? startTime, DateTime? endTime)
        {
            parameters.AddOptionalParameter("start_time", startTime == null ? null : JsonConvert.SerializeObject(startTime, new TimestampConverter()));
            parameters.AddOptionalParameter("end_time", endTime == null ? null : JsonConvert.SerializeObject(endTime, new TimestampConverter()));
        }

        // Override because FTX requires HttpMethod.Delete requests to have the parameters in the request body instead of uri
        protected override IRequest ConstructRequest(Uri uri, HttpMethod method, Dictionary<string, object>? parameters, bool signed, PostParameters postPosition, ArrayParametersSerialization arraySerialization, int requestId)
        {
            if (parameters == null)
                parameters = new Dictionary<string, object>();

            var uriString = uri.ToString();
            if (authProvider != null)
                parameters = authProvider.AddAuthenticationToParameters(uriString, method, parameters, signed, postPosition, arraySerialization);

            if ((method == HttpMethod.Get || postPosition == PostParameters.InUri) && parameters?.Any() == true)
                uriString += "?" + parameters.CreateParamString(true, arraySerialization);

            var contentType = requestBodyFormat == RequestBodyFormat.Json ? Constants.JsonContentHeader : Constants.FormContentHeader;
            var request = RequestFactory.Create(method, uriString, requestId);
            request.Accept = Constants.JsonContentHeader;

            var headers = new Dictionary<string, string>();
            if (authProvider != null)
                headers = authProvider.AddAuthenticationToHeaders(uriString, method, parameters!, signed, postPosition, arraySerialization);

            foreach (var header in headers)
                request.AddHeader(header.Key, header.Value);

            if ((method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Delete) && postPosition != PostParameters.InUri)
            {
                if (parameters?.Any() == true)
                    WriteParamBody(request, parameters, contentType);
                else
                    request.SetContent(requestBodyEmptyContent, contentType);
            }

            return request;
        }

        private int GetResolutionFromKlineInterval(KlineInterval interval)
        {
            switch (interval)
            {
                case KlineInterval.FifteenSeconds: return 15;
                case KlineInterval.OneMinute: return 60;
                case KlineInterval.FiveMinutes: return 300;
                case KlineInterval.FifteenMinutes: return 900;
                case KlineInterval.OneHour: return 3600;
                case KlineInterval.FourHour: return 14400;
                case KlineInterval.OneDay: return 86400;
                case KlineInterval.OneWeek: return 86400 * 7;
                case KlineInterval.OneMonth: return 86400 * 30;
                default: throw new Exception("Unknown kline interval");
            }
        }
        #endregion

        #region common interface
        public string GetSymbolName(string baseAsset, string quoteAsset)
        {
            return baseAsset + "/" + quoteAsset;
        }

        Task<WebCallResult<IEnumerable<ICommonSymbol>>> IExchangeClient.GetSymbolsAsync()
        {
            throw new NotImplementedException();
        }

        Task<WebCallResult<IEnumerable<ICommonTicker>>> IExchangeClient.GetTickersAsync()
        {
            throw new NotImplementedException();
        }

        Task<WebCallResult<ICommonTicker>> IExchangeClient.GetTickerAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        Task<WebCallResult<IEnumerable<ICommonKline>>> IExchangeClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            throw new NotImplementedException();
        }

        Task<WebCallResult<ICommonOrderBook>> IExchangeClient.GetOrderBookAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        Task<WebCallResult<IEnumerable<ICommonRecentTrade>>> IExchangeClient.GetRecentTradesAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        Task<WebCallResult<ICommonOrderId>> IExchangeClient.PlaceOrderAsync(string symbol, IExchangeClient.OrderSide side, IExchangeClient.OrderType type, decimal quantity, decimal? price = null, string? accountId = null)
        {
            throw new NotImplementedException();
        }

        Task<WebCallResult<ICommonOrder>> IExchangeClient.GetOrderAsync(string orderId, string? symbol = null)
        {
            throw new NotImplementedException();
        }

        Task<WebCallResult<IEnumerable<ICommonTrade>>> IExchangeClient.GetTradesAsync(string orderId, string? symbol = null)
        {
            throw new NotImplementedException();
        }

        Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetOpenOrdersAsync(string? symbol = null)
        {
            throw new NotImplementedException();
        }

        Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetClosedOrdersAsync(string? symbol = null)
        {
            throw new NotImplementedException();
        }

        Task<WebCallResult<ICommonOrderId>> IExchangeClient.CancelOrderAsync(string orderId, string? symbol = null)
        {
            throw new NotImplementedException();
        }

        Task<WebCallResult<IEnumerable<ICommonBalance>>> IExchangeClient.GetBalancesAsync(string? accountId = null)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}
