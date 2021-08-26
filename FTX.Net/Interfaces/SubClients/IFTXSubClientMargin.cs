using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.Margin;

namespace FTX.Net.Interfaces.SubClients
{
    /// <summary>
    /// Margin endpoints
    /// </summary>
    public interface IFTXSubClientMargin
    {
        /// <summary>
        /// Get lending history
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLend>>> GetLendingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get borrow rates
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXBorrowRate>>> GetBorrowRatesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get lending rates
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXBorrowRate>>> GetLendingRatesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get daily borrowed amount
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXBorrowSummary>>> GetDailyBorrowedAmountAsync(CancellationToken ct = default);

        /// <summary>
        /// Get symbol info
        /// </summary>
        /// <param name="symbol">Symbol to get info on</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXMarginMarketInfo>>> GetSymbolSummaryAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get user borrow history
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXUserLend>>> GetUserBorrowHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get user lending history
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXUserLend>>> GetUserLendingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get lending offers
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLendingOffer>>> GetLendingOffersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get lending info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLendingInfo>>> GetLendingInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Submit a lending offer
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="rate">Rate</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> PlaceLendingOfferAsync(string asset, decimal quantity, decimal rate, CancellationToken ct = default);
    }
}