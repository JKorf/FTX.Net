using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Objects.Models.Margin;
using FTX.Net.Interfaces.Clients.GeneralApi;

namespace FTX.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    public class FTXClientGeneralApiMargin : IFTXClientGeneralApiMargin
    {
        private readonly FTXClientGeneralApi _baseClient;

        internal FTXClientGeneralApiMargin(FTXClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXLend>>> GetLendingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLend>>(_baseClient.GetUri("spot_margin/history"), HttpMethod.Get, ct, parameters, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXBorrowRate>>> GetBorrowRatesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXBorrowRate>>(_baseClient.GetUri("spot_margin/borrow_rates"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXBorrowRate>>> GetLendingRatesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXBorrowRate>>(_baseClient.GetUri("spot_margin/lending_rates"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXBorrowSummary>>> GetDailyBorrowedAmountAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXBorrowSummary>>(_baseClient.GetUri("spot_margin/borrow_summary"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXMarginMarketInfo>>> GetSymbolSummaryAsync(string symbol, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("market", symbol);
            var result = await _baseClient.SendFTXRequest<IEnumerable<FTXMarginMarketInfo>>(_baseClient.GetUri("spot_margin/market_info"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
            if (result && result.Data == null)
                return new WebCallResult<IEnumerable<FTXMarginMarketInfo>>(result.ResponseStatusCode, result.ResponseHeaders, null, new ServerError("No data returned"));

            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXUserLend>>> GetUserBorrowHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXUserLend>>(_baseClient.GetUri("spot_margin/borrow_history"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXUserLend>>> GetUserLendingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXUserLend>>(_baseClient.GetUri("spot_margin/lending_history"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXLendingOffer>>> GetLendingOffersAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLendingOffer>>(_baseClient.GetUri("spot_margin/offers"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXLendingInfo>>> GetLendingInfoAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLendingInfo>>(_baseClient.GetUri("spot_margin/lending_info"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> PlaceLendingOfferAsync(string asset, decimal quantity, decimal rate, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("coin", asset);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("rate", rate.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest(_baseClient.GetUri("spot_margin/offers"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }
    }
}
