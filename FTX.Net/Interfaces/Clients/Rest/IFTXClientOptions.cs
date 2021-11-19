using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using FTX.Net.Enums;
using FTX.Net.Objects.Models.Options;

namespace FTX.Net.Interfaces.Clients.Rest
{
    /// <summary>
    /// FTX options endpoints
    /// </summary>
    public interface IFTXClientOptions
    {
        /// <summary>
        /// Get list of quote requests
        /// <para><a href="https://docs.ftx.com/#list-quote-requests" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXQuoteRequest>>> GetQuoteRequestsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get list of quote requests for the user
        /// <para><a href="https://docs.ftx.com/#your-quote-requests" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXUserQuoteRequest>>> GetUserQuoteRequestsAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Create quote request
        /// <para><a href="https://docs.ftx.com/#create-quote-request" /></para>
        /// </summary>
        /// <param name="underlying">Underlying</param>
        /// <param name="type">Type</param>
        /// <param name="strike">Strike</param>
        /// <param name="expiry">Must be in the future and at 03:00 UTC.</param>
        /// <param name="side">Side</param>
        /// <param name="size">Size</param>
        /// <param name="limitPrice">Limit price</param>
        /// <param name="hideLimitPrice">Whether or not to hide your limit price from potential quoters, default true</param>
        /// <param name="requestExpiry">Request expiry</param>
        /// <param name="counterPartyId">When specified, makes the request private to the specified counterparty</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXQuoteRequest>> CreateQuoteRequestAsync(string underlying, OptionType type, decimal strike, DateTime expiry, OrderSide side, decimal size, decimal? limitPrice = null, bool? hideLimitPrice = null, DateTime? requestExpiry = null, long? counterPartyId = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a quote request
        /// <para><a href="https://docs.ftx.com/#cancel-quote-request" /></para>
        /// </summary>
        /// <param name="requestId">Request id to cancel</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXUserQuoteRequest>> CancelQuoteRequestAsync(long requestId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get quotes for your quote request
        /// <para><a href="https://docs.ftx.com/#get-quotes-for-your-quote-request" /></para>
        /// </summary>
        /// <param name="requestId">Request id</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXQuoteRequestQuote>>> GetQuotesForQuoteRequestAsync(long requestId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Create quote
        /// <para><a href="https://docs.ftx.com/#create-quote" /></para>
        /// </summary>
        /// <param name="requestId">Request id</param>
        /// <param name="price">Price of the quote</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXUserQuoteRequest>> CreateQuoteAsync(long requestId, decimal price, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get quotes for user
        /// <para><a href="https://docs.ftx.com/#get-my-quotes" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXQuoteRequestQuote>>> GetUserQuotesAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a quote
        /// <para><a href="https://docs.ftx.com/#cancel-quote" /></para>
        /// </summary>
        /// <param name="quoteId">Quote id</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXQuoteRequestQuote>> CancelQuoteAsync(long quoteId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Accept options quote
        /// <para><a href="https://docs.ftx.com/#accept-options-quote" /></para>
        /// </summary>
        /// <param name="quoteId">Quote id</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXQuoteRequestQuote>> AcceptQuoteAsync(long quoteId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get account options info
        /// <para><a href="https://docs.ftx.com/#get-account-options-info" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOptionsAccountInfo>> GetAccountOptionsInfoAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get options positions
        /// <para><a href="https://docs.ftx.com/#get-options-positions" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXOptionsPosition>>> GetOptionsPositionsAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get public options positions
        /// <para><a href="https://docs.ftx.com/#get-public-options-trades" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXOptionTrade>>> GetOptionTradesAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get options fills
        /// <para><a href="https://docs.ftx.com/#get-options-fills" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXUserOptionTrade>>> GetUserOptionTradesAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get 24H option volume
        /// <para><a href="https://docs.ftx.com/#get-24h-option-volume" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOptionsVolume>> GetOptionVolumeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get historical option volume
        /// <para><a href="https://docs.ftx.com/#get-option-open-interest" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXOptionsHistoricalVolume>>> GetOptionsHistoricalVolumeAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get open interest
        /// <para><a href="https://docs.ftx.com/#get-option-open-interest" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOptionOpenInterest>> GetOptionsOpenInterestAsync(CancellationToken ct = default);

        /// <summary>
        /// Get open interest history
        /// <para><a href="https://docs.ftx.com/#get-option-open-interest-3" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXOptionHistoricalOpenInterest>>> GetOptionHistoricalOpenInterestAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);
    }
}