using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.Models.FTXPay;

namespace FTX.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// FTX pay endpoints
    /// </summary>
    public interface IFTXClientGeneralApiPay
    {
        /// <summary>
        /// Get the details of an FTXPay app, along with a list of payments to that app. Note that UserId is the id of this app specific to your account as a merchant.
        /// </summary>
        /// <param name="appId">App id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Maximum results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXAppDetails>> GetAppAndPaymentsAsync(long appId, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

#pragma warning disable 1570
        /// <summary>
        /// you can pre-register an order, specifying its size and currency, and track its status. When you supply an ID identifying the order to an FTX Pay popup, completion of the payment will also update the status of the order.
        /// To supply an ID, the link you should send payers to(or spawn in a popup for them) is: https://ftx.com/pay/request?id=APP_ID&orderId=ORDER_ID or https://ftx.com/pay/request?id=APP_ID&clientOrderId=CLIENT_ORDER_ID
        /// </summary>
        /// <param name="appId">Application id</param>
        /// <param name="asset">The asset of the payment</param>
        /// <param name="notes">Notes about this order that are private to the merchant</param>
        /// <param name="quantity">Size of the desired payment</param>
        /// <param name="allowTips">Whether or not tips are allowed for the payment</param>
        /// <param name="clientOrderId">ID for you to track the order with (must be unique to your FTX Pay app)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
#pragma warning restore 1570
        Task<WebCallResult<FTXAppOrder>> CreateOrderAsync(long appId, string asset, decimal quantity, bool allowTips, string? notes = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get orders for an app
        /// </summary>
        /// <param name="appId">App id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXAppOrder>>> GetOrdersAsync(long appId, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Cancels an order, preventing it from being filled by a future FTX Pay payment. Can only be used on orders that have not already been filled or canceled.
        /// </summary>
        /// <param name="appId">App id</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> CancelOrderAsync(long appId, long orderId, CancellationToken ct = default);

        /// <summary>
        /// You can return a payment by specifying your app ID and the payment ID. The quantity paid to you (includig the tip, but without the fee that was already applied) will be returned to the payer.
        /// </summary>
        /// <param name="appId">App id</param>
        /// <param name="paymentId">Payment id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> ReturnPaymentAsync(long appId, long paymentId, CancellationToken ct = default);
    }
}