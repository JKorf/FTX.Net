using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Objects.Models.FTXPay;
using FTX.Net.Interfaces.Clients.GeneralApi;

namespace FTX.Net.Clients.GeneralApi
{
    /// <summary>
    /// FTX Pay endpoints
    /// </summary>
    public class FTXClientGeneralApiPay : IFTXClientGeneralApiPay
    {
        private readonly FTXClientGeneralApi _baseClient;

        internal FTXClientGeneralApiPay(FTXClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXAppDetails>> GetAppAndPaymentsAsync(long appId, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            var result = await _baseClient.SendFTXRequest<FTXAppDetails>(_baseClient.GetUri($"ftxpay/apps/{appId}/details"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
            if (!result)
                return result;

            if (!result.Data.Exists)
                return WebCallResult<FTXAppDetails>.CreateErrorResult(new ServerError("App not found"));

            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXAppOrder>> CreateOrderAsync(long appId, string asset, decimal quantity, bool allowTips, string? notes = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("coin", asset);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("allowTips", allowTips.ToString());
            parameters.AddOptionalParameter("notes", notes);
            parameters.AddOptionalParameter("clientOrderId", clientOrderId);
            return await _baseClient.SendFTXRequest<FTXAppOrder>(_baseClient.GetUri($"ftxpay/apps/{appId}/orders"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXAppOrder>>> GetOrdersAsync(long appId, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXAppOrder>>(_baseClient.GetUri($"ftxpay/apps/{appId}/orders"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> CancelOrderAsync(long appId, long orderId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest(_baseClient.GetUri($"ftxpay/apps/{appId}/{orderId}/orders"), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> ReturnPaymentAsync(long appId, long paymentId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest(_baseClient.GetUri($"ftxpay/apps/{appId}/{paymentId}/return"), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }
    }
}
