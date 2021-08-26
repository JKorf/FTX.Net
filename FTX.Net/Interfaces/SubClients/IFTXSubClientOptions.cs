using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using FTX.Net.Enums;
using FTX.Net.Objects.Options;

namespace FTX.Net.Interfaces.SubClients
{
    /// <summary>
    /// Option endpoints
    /// </summary>
    public interface IFTXSubClientOptions
    {
        /// <summary>
        /// Get list of quote requests
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXQuoteRequest>>> GetQuoteRequestsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get list of quote requests for the user
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXQuoteRequest>>> GetUserQuoteRequestsAsync(CancellationToken ct = default);

        /// <summary>
        /// Create quote request
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
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXUserQuoteRequest>> CreateQuoteRequestAsync(string underlying, OptionType type, decimal strike, DateTime expiry, OrderSide side, decimal size, decimal? limitPrice = null, bool? hideLimitPrice = null, DateTime? requestExpiry = null, long? counterPartyId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a quote request
        /// </summary>
        /// <param name="requestId">Request id to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXUserQuoteRequest>> CancelQuoteRequestAsync(long requestId, CancellationToken ct = default);

        /// <summary>
        /// Get quotes for your quote request
        /// </summary>
        /// <param name="requestId">Request id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXQuoteRequestQuote>>> GetQuotesForQuoteRequestAsync(long requestId, CancellationToken ct = default);

        /// <summary>
        /// Create quote
        /// </summary>
        /// <param name="requestId">Request id</param>
        /// <param name="price">Price of the quote</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXUserQuoteRequest>> CreateQuoteAsync(long requestId, decimal price, CancellationToken ct = default);

        /// <summary>
        /// Get quotes for user
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXQuoteRequestQuote>>> GetUserQuotesAsync(CancellationToken ct = default);

        /// <summary>
        /// Cancel a quote
        /// </summary>
        /// <param name="quoteId">Quote id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXQuoteRequestQuote>> CancelQuoteAsync(long quoteId, CancellationToken ct = default);

        /// <summary>
        /// Accept options quote
        /// </summary>
        /// <param name="quoteId">Quote id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXQuoteRequestQuote>> AcceptQuoteAsync(long quoteId, CancellationToken ct = default);

        /// <summary>
        /// Get account options info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOptionsAccountInfo>> GetAccountOptionsInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get options positions
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXOptionsPosition>>> GetOptionsPositionsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get public options positions
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXOptionTrade>>> GetOptionTradesAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get options fills
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXUserOptionTrade>>> GetUserOptionTradesAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get 24H option volume
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOptionsVolume>> GetOptionVolumeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get historical option volume
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXOptionsHistoricalVolume>>> GetOptionsHistoricalVolumeAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get open interest
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOptionOpenInterest>> GetOptionsOpenInterestAsync(CancellationToken ct = default);

        /// <summary>
        /// Get open interest history
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXOptionHistoricalOpenInterest>>> GetOptionHistoricalOpenInterestAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);
    }
}