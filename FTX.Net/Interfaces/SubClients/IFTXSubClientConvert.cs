using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.Convert;

namespace FTX.Net.Interfaces.SubClients
{
    /// <summary>
    /// Convert endpoints
    /// </summary>
    public interface IFTXSubClientConvert
    {
        /// <summary>
        /// Create a new quote request
        /// </summary>
        /// <param name="fromAsset">From asset</param>
        /// <param name="toAsset">To asset</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXConvertQuoteResult>> CreateQuoteRequestAsync(string fromAsset, string toAsset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get quote status
        /// </summary>
        /// <param name="quoteId">Quote id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXConvertQuote>>> GetQuoteStatusAsync(long quoteId, CancellationToken ct = default);

        /// <summary>
        /// Accept a convert quote
        /// </summary>
        /// <param name="quoteId">Id of quote to accept</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> AcceptQuoteAsync(long quoteId, CancellationToken ct = default);
    }
}