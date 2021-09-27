using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using FTX.Net.Enums;
using FTX.Net.Interfaces.SubClients;
using FTX.Net.Objects;
using FTX.Net.Objects.Futures;
using FTX.Net.Objects.Spot;

namespace FTX.Net.Interfaces
{
    /// <summary>
    /// FTX client interface
    /// </summary>
    public interface IFTXClient: IRestClient
    {
        /// <summary>
        /// Convert endpoints
        /// </summary>
        IFTXSubClientConvert Convert { get; }

        /// <summary>
        /// Options endpoints
        /// </summary>
        IFTXSubClientOptions Options { get; }

        /// <summary>
        /// Leveraged token endpoints
        /// </summary>
        IFTXSubClientLeveragedTokens LeveragedTokens { get; }

        /// <summary>
        /// Staking endpoints
        /// </summary>
        IFTXSubClientStaking Staking { get; }

        /// <summary>
        /// Spot margin endpoints
        /// </summary>
        IFTXSubClientMargin Margin { get; }

        /// <summary>
        /// NFT endpoints
        /// </summary>
        IFTXSubClientNft NFT { get; }

        /// <summary>
        /// FTXPay endpoints
        /// </summary>
        IFTXSubClientPay FTXPay { get; }

        /// <summary>
        /// Subaccount endpoints
        /// </summary>
        IFTXSubClientSubaccounts Subaccounts { get; }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        void SetApiCredentials(string apiKey, string apiSecret);

        /// <summary>
        /// Get the list of supported symbols
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXSymbol>>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get symbol info
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the orderbook for a symbol
        /// </summary>
        /// <param name="symbol">Symbol to get the book for</param>
        /// <param name="depth">Depth of the book</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOrderbook>> GetOrderBookAsync(string symbol, int depth, CancellationToken ct = default);

        /// <summary>
        /// Get trades for a symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXTrade>>> GetTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get klines for a symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get the list of supported futures
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXFuture>>> GetFuturesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get info on a future
        /// </summary>
        /// <param name="future">Future name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXFuture>> GetFutureAsync(string future, CancellationToken ct = default);

        /// <summary>
        /// Get stats on a future
        /// </summary>
        /// <param name="future">Future name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXFutureStat>> GetFutureStatsAsync(string future, CancellationToken ct = default);

        /// <summary>
        /// Get funding rates
        /// </summary>
        /// <param name="future">Future name</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXFundingRate>>> GetFundingRatesAsync(string future, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get index weights
        /// </summary>
        /// <param name="index">Index name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, decimal>>> GetIndexWeightsAsync(string index, CancellationToken ct = default);

        /// <summary>
        /// Get the list of expired futures
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXFuture>>> GetExpiredFuturesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get index klines
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXKline>>> GetIndexKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get account info
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXAccountInfo>> GetAccountInfoAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get positions
        /// </summary>
        /// <param name="showAveragePrice"></param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXPosition>>> GetPositionsAsync(bool? showAveragePrice = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Change account leverage
        /// </summary>
        /// <param name="leverage">Desired acccount-wide leverage setting</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> ChangeAccountLeverageAsync(decimal leverage, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get the list of assets
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXAsset>>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a list of balances
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXBalance>>> GetBalancesAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of balances, including master and subaccounts
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, IEnumerable<FTXBalance>>>> GetAllAccountBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get deposit address for an asset
        /// </summary>
        /// <param name="asset">Asset to get address for</param>
        /// <param name="network">The network to use</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXDeposit>>> GetDepositHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal history
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXWithdrawal>>> GetWithdrawalHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

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
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXWithdrawal>> WithdrawAsync(string asset, decimal quantity, string address, string? tag = null, string? network = null, string? password = null, string? code = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get airdrops
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXAirdrop>>> GetAirdropsAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal fees
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="address">Address</param>
        /// <param name="tag">Tag</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXWithdrawalFee>> GetWithdrawalFeesAsync(string asset, decimal quantity, string address, string? tag = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get saved addresses
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXSavedAddress>>> GetSavedAddressesAsync(string? asset = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Create a saved address
        /// </summary>
        /// <param name="asset">Asset the address is for</param>
        /// <param name="address">The address</param>
        /// <param name="addressName">Name of the address</param>
        /// <param name="isPrimeTrust">Is prime trust</param>
        /// <param name="tag">Address tag</param>
        /// <param name="code">2FA code if needed</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXSavedAddress>> CreateSavedAddressAsync(string asset, string address, string addressName, bool isPrimeTrust, string? tag = null, string? code = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Delete a saved address
        /// </summary>
        /// <param name="savedAddressId">Id of the saved address to delete</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string>> DeleteSavedAddressAsync(long savedAddressId, string? subaccountName = null, CancellationToken ct = default);

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
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOrder>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal quantity, decimal? price = null, bool? reduceOnly = null, bool? immediateOrCancel = null, bool? postOnly = null, string? clientOrderId = null, bool? rejectOnPriceBand = null, string? subaccountName = null, CancellationToken ct = default);

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
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXTriggerOrder>> PlaceTriggerOrderAsync(
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
            CancellationToken ct = default);

        /// <summary>
        /// Modify an order. Will internally cancel the original order and place a new order with the new price/quantity. The new order will have a new order id. Note: there's a chance that the order meant to be cancelled gets filled and its replacement still gets placed.
        /// </summary>
        /// <param name="orderId">Id of order to modify</param>
        /// <param name="price">New price of the order</param>
        /// <param name="quantity">New quantity of the order</param>
        /// <param name="clientOrderId">New client order id</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOrder>> ModifyOrderAsync(long orderId, decimal? price = null, decimal? quantity = null, string? clientOrderId = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Modify a trigger order. Will internally cancel the original order and place a new order with the new price/quantity. The new order will have a new order id. Note: there's a chance that the order meant to be cancelled gets filled and its replacement still gets placed.
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="quantity">New quantity</param>
        /// <param name="triggerPrice">New trigger price</param>
        /// <param name="orderPrice">New order price</param>
        /// <param name="trailingValue">New trailing value</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXTriggerOrder>> ModifyTriggerOrderAsync(long orderId, decimal? quantity = null, decimal ? triggerPrice = null, decimal? orderPrice = null, decimal? trailingValue = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Modify an order. Will internally cancel the original order and place a new order with the new price/quantity. The new order will have a new order id. Note: there's a chance that the order meant to be cancelled gets filled and its replacement still gets placed.
        /// </summary>
        /// <param name="clientOrderId">Client order id of order to modify</param>
        /// <param name="price">New price of the order</param>
        /// <param name="quantity">New quantity of the order</param>
        /// <param name="newClientOrderId">New client order id</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOrder>> ModifyOrderByClientOrderIdAsync(long clientOrderId, decimal? price = null, decimal? quantity = null, string? newClientOrderId = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get the status of an order
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOrder>> GetOrderAsync(long orderId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get the status of an order
        /// </summary>
        /// <param name="clientOrderId">Client order id of the order</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list triggers for a trigger order
        /// </summary>
        /// <param name="orderId">Id of the trigger order</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXTriggerOrderTrigger>>> GetTriggerOrderTriggers(long orderId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of open orders
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXOrder>>> GetOpenOrdersAsync(string? symbol = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of open trigger orders
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="type">Filter by type</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXTriggerOrder>>> GetOpenTriggerOrdersAsync(string? symbol = null, TriggerOrderType? type = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of orders
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXOrder>>> GetOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of trigger orders
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by trigger type</param>
        /// <param name="orderType">Filter by order type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXTriggerOrder>>> GetTriggerOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, OrderSide? side = null, TriggerOrderType? type = null, OrderType? orderType = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string>> CancelOrderAsync(long orderId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a trigger order
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string>> CancelTriggerOrderAsync(long orderId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="clientOrderId">Client order id of the order</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string>> CancelOrderByClientIdAsync(string clientOrderId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders matching the parameters
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="side">Filter by side</param>
        /// <param name="conditionalOrdersOnly">Only cancel conditional orders</param>
        /// <param name="limitOrdersOnly">Only cancel limit orders</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string>> CancelAllOrdersAsync(string? symbol = null, OrderSide? side = null, bool? conditionalOrdersOnly = null, bool? limitOrdersOnly = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of trades based on the input parameters
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="ascendingOrder">Return results in ascending order in time</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXUserTrade>>> GetUserTradesAsync(string? symbol = null, long? orderId = null, bool? ascendingOrder = null, DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of funding payments
        /// </summary>
        /// <param name="future">Filter by future</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXFundingPayment>>> GetFundingPaymentsAsync(string? future = null, DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

    }
}