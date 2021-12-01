using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.Models.Margin;

namespace FTX.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// FTX margin endpoints
    /// </summary>
    public interface IFTXClientGeneralApiMargin
    {
        /// <summary>
        /// Get lending history
        /// <para><a href="https://docs.ftx.com/#get-lending-history" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLend>>> GetLendingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get borrow rates
        /// <para><a href="https://docs.ftx.com/#get-borrow-rates" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXBorrowRate>>> GetBorrowRatesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get lending rates
        /// <para><a href="https://docs.ftx.com/#get-lending-rates" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXBorrowRate>>> GetLendingRatesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get daily borrowed amount
        /// <para><a href="https://docs.ftx.com/#get-daily-borrowed-amounts" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXBorrowSummary>>> GetDailyBorrowedAmountAsync(CancellationToken ct = default);

        /// <summary>
        /// Get symbol info
        /// <para><a href="https://docs.ftx.com/#get-market-info" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to get info on</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXMarginMarketInfo>>> GetSymbolSummaryAsync(string symbol, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get user borrow history
        /// <para><a href="https://docs.ftx.com/#get-my-borrow-history" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXUserLend>>> GetUserBorrowHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get user lending history
        /// <para><a href="https://docs.ftx.com/#get-my-lending-history" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXUserLend>>> GetUserLendingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get lending offers
        /// <para><a href="https://docs.ftx.com/#get-lending-offers" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLendingOffer>>> GetLendingOffersAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get lending info
        /// <para><a href="https://docs.ftx.com/#get-lending-info" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLendingInfo>>> GetLendingInfoAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Submit a lending offer
        /// <para><a href="https://docs.ftx.com/#submit-lending-offer" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="rate">Rate</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> PlaceLendingOfferAsync(string asset, decimal quantity, decimal rate, string? subaccountName = null, CancellationToken ct = default);
    }
}