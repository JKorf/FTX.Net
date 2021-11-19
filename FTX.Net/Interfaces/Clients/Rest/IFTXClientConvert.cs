using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.Models.Convert;

namespace FTX.Net.Interfaces.Clients.Rest
{
    /// <summary>
    /// FTX convert endpoints
    /// </summary>
    public interface IFTXClientConvert
    {
        /// <summary>
        /// Create a new quote request
        /// <para><a href="https://docs.ftx.com/#request-quote" /></para>
        /// </summary>
        /// <param name="fromAsset">From asset</param>
        /// <param name="toAsset">To asset</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXConvertQuoteResult>> CreateQuoteRequestAsync(string fromAsset, string toAsset, decimal quantity, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get quote status
        /// <para><a href="https://docs.ftx.com/#get-quote-status" /></para>
        /// </summary>
        /// <param name="quoteId">Quote id</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXConvertQuote>>> GetQuoteStatusAsync(long quoteId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Accept a convert quote
        /// <para><a href="https://docs.ftx.com/#accept-quote" /></para>
        /// </summary>
        /// <param name="quoteId">Id of quote to accept</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> AcceptQuoteAsync(long quoteId, string? subaccountName = null, CancellationToken ct = default);
    }
}