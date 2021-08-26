using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.Margin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Interfaces.SubClients;

namespace FTX.Net.SubClients
{
    /// <summary>
    /// Spot margin endpoints
    /// </summary>
    public class FTXSubClientMargin : IFTXSubClientMargin
    {
        private readonly FTXClient _baseClient;

        internal FTXSubClientMargin(FTXClient baseClient)
        {
            _baseClient = baseClient;
        }

        /// <summary>
        /// Get lending history
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXLend>>> GetLendingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            _baseClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLend>>(_baseClient.GetUri("spot_margin/history"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get borrow rates
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXBorrowRate>>> GetBorrowRatesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXBorrowRate>>(_baseClient.GetUri("spot_margin/borrow_rates"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get lending rates
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXBorrowRate>>> GetLendingRatesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXBorrowRate>>(_baseClient.GetUri("spot_margin/lending_rates"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get daily borrowed amount
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXBorrowSummary>>> GetDailyBorrowedAmountAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXBorrowSummary>>(_baseClient.GetUri("spot_margin/borrow_summary"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get symbol info
        /// </summary>
        /// <param name="symbol">Symbol to get info on</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXMarginMarketInfo>>> GetSymbolSummaryAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("market", symbol);
            var result = await _baseClient.SendFTXRequest<IEnumerable<FTXMarginMarketInfo>>(_baseClient.GetUri("spot_margin/market_info"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
            if (result && result.Data == null)
                return new WebCallResult<IEnumerable<FTXMarginMarketInfo>>(result.ResponseStatusCode, result.ResponseHeaders, null, new ServerError("No data returned"));

            return result;
        }

        /// <summary>
        /// Get user borrow history
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXUserLend>>> GetUserBorrowHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            _baseClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXUserLend>>(_baseClient.GetUri("spot_margin/borrow_history"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get user lending history
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXUserLend>>> GetUserLendingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            _baseClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXUserLend>>(_baseClient.GetUri("spot_margin/lending_history"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get lending offers
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXLendingOffer>>> GetLendingOffersAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLendingOffer>>(_baseClient.GetUri("spot_margin/offers"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get lending info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXLendingInfo>>> GetLendingInfoAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLendingInfo>>(_baseClient.GetUri("spot_margin/lending_info"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Submit a lending offer
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="rate">Rate</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult> PlaceLendingOfferAsync(string asset, decimal quantity, decimal rate, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("coin", asset);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("rate", rate.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest(_baseClient.GetUri("spot_margin/offers"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }
    }
}
