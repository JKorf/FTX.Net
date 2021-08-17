using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.FTXPay;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FTX.Net.SubClients
{
    /// <summary>
    /// FTX Pay endpoints
    /// </summary>
    public class FTXSubClientPay
    {
        private readonly FTXClient _baseClient;

        internal FTXSubClientPay(FTXClient baseClient)
        {
            _baseClient = baseClient;
        }

        /// <summary>
        /// Get the details of an FTXPay app, along with a list of payments to that app. Note that UserId is the id of this app specific to your account as a merchant.
        /// </summary>
        /// <param name="appId">App id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Maximum results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXAppDetails>> GetAppAndPaymentsAsync(long appId, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            _baseClient.AddFilter(parameters, startTime, endTime);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            var result = await _baseClient.SendFTXRequest<FTXAppDetails>(_baseClient.GetUri($"ftxpay/apps/{appId}/details"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
            if (!result)
                return result;

            if (!result.Data.Exists)
                return WebCallResult<FTXAppDetails>.CreateErrorResult(new ServerError("App not found"));

            return result;
        }

        /// <summary>
        /// you can pre-register an order, specifying its size and currency, and track its status. When you supply an ID identifying the order to an FTX Pay popup, completion of the payment will also update the status of the order.
        /// To supply an ID, the link you should send payers to(or spawn in a popup for them) is: https://ftx.com/pay/request?id=APP_ID&orderId=ORDER_ID or https://ftx.com/pay/request?id=APP_ID&clientOrderId=CLIENT_ORDER_ID
        /// </summary>
        /// <param name="asset">The currency of the payment</param>
        /// <param name="notes">Notes about this order that are private to the merchant</param>
        /// <param name="quantity">Size of the desired payment</param>
        /// <param name="allowTip">Whether or not tips are allowed for the payment</param>
        /// <param name="clientOrderId">ID for you to track the order with (must be unique to your FTX Pay app)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXAppOrder>> CreateOrderAsync(long appId, string asset, decimal quantity, bool allowTip, string? notes = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("coin", asset);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("allowTip", allowTip.ToString());
            parameters.AddOptionalParameter("notes", notes);
            parameters.AddOptionalParameter("clientOrderId", clientOrderId);
            return await _baseClient.SendFTXRequest<FTXAppOrder>(_baseClient.GetUri($"ftxpay/apps/{appId}/orders"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get orders for an app
        /// </summary>
        /// <param name="appId">App id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXAppOrder>>> GetOrdersAsync(long appId, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            _baseClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXAppOrder>>(_baseClient.GetUri($"ftxpay/apps/{appId}/orders"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);     
        }

        /// <summary>
        /// Cancels an order, preventing it from being filled by a future FTX Pay payment. Can only be used on orders that have not already been filled or cancelled.
        /// </summary>
        /// <param name="appId">App id</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult> CancelOrderAsync(long appId, long orderId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest(_baseClient.GetUri($"ftxpay/apps/{appId}/{orderId}/orders"), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// You can return a payment by specifying your app ID and the payment ID. The amount paid to you (includig the tip, but without the fee that was already applied) will be returned to the payer.
        /// </summary>
        /// <param name="appId">App id</param>
        /// <param name="paymentId">Payment id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult> ReturnPaymentAsync(long appId, long paymentId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest(_baseClient.GetUri($"ftxpay/apps/{appId}/{paymentId}/return"), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }
    }
}
