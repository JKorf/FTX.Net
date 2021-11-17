using CryptoExchange.Net.Objects;
using FTX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Objects.Models;

namespace FTX.Net.Interfaces.Clients.Rest
{
    public interface IFTXClientTrading
    {
        /// <summary>
        /// Place a new order
        /// <para><a href="https://docs.ftx.com/#place-order" /></para>
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
        /// <para><a href="https://docs.ftx.com/#place-trigger-order" /></para>
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
        /// Modify an order. Will internally cancel the original order and place a new order with the new price/quantity. The new order will have a new order id. Note: there's a chance that the order meant to be canceled gets filled and its replacement still gets placed.
        /// <para><a href="https://docs.ftx.com/#modify-order" /></para>
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
        /// Modify a trigger order. Will internally cancel the original order and place a new order with the new price/quantity. The new order will have a new order id. Note: there's a chance that the order meant to be canceled gets filled and its replacement still gets placed.
        /// <para><a href="https://docs.ftx.com/#modify-trigger-order" /></para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="quantity">New quantity</param>
        /// <param name="triggerPrice">New trigger price</param>
        /// <param name="orderPrice">New order price</param>
        /// <param name="trailingValue">New trailing value</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXTriggerOrder>> ModifyTriggerOrderAsync(long orderId, decimal? quantity = null, decimal? triggerPrice = null, decimal? orderPrice = null, decimal? trailingValue = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Modify an order. Will internally cancel the original order and place a new order with the new price/quantity. The new order will have a new order id. Note: there's a chance that the order meant to be canceled gets filled and its replacement still gets placed.
        /// <para><a href="https://docs.ftx.com/#modify-order-by-client-id" /></para>
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
        /// <para><a href="https://docs.ftx.com/#get-order-status" /></para>
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOrder>> GetOrderAsync(long orderId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get the status of an order
        /// <para><a href="https://docs.ftx.com/#get-order-status-by-client-id" /></para>
        /// </summary>
        /// <param name="clientOrderId">Client order id of the order</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list triggers for a trigger order
        /// <para><a href="https://docs.ftx.com/#get-trigger-order-triggers" /></para>
        /// </summary>
        /// <param name="orderId">Id of the trigger order</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXTriggerOrderTrigger>>> GetTriggerOrderTriggersAsync(long orderId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of open orders
        /// <para><a href="https://docs.ftx.com/#get-open-orders" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXOrder>>> GetOpenOrdersAsync(string? symbol = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of open trigger orders
        /// <para><a href="https://docs.ftx.com/#get-open-trigger-orders" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="type">Filter by type</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXTriggerOrder>>> GetOpenTriggerOrdersAsync(string? symbol = null, TriggerOrderType? type = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of orders
        /// <para><a href="https://docs.ftx.com/#get-order-history" /></para>
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
        /// <para><a href="https://docs.ftx.com/#get-trigger-order-history" /></para>
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
        /// <para><a href="https://docs.ftx.com/#cancel-order" /></para>
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string>> CancelOrderAsync(long orderId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a trigger order
        /// <para><a href="https://docs.ftx.com/#cancel-open-trigger-order" /></para>
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string>> CancelTriggerOrderAsync(long orderId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para><a href="https://docs.ftx.com/#cancel-order-by-client-id" /></para>
        /// </summary>
        /// <param name="clientOrderId">Client order id of the order</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string>> CancelOrderByClientIdAsync(string clientOrderId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders matching the parameters
        /// <para><a href="https://docs.ftx.com/#cancel-all-orders" /></para>
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
        /// <para><a href="https://docs.ftx.com/#fills" /></para>
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

    }
}
