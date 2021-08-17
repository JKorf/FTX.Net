using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.Convert;
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
    /// Convert endpoints
    /// </summary>
    public class FTXSubClientConvert
    {
        private readonly FTXClient _baseClient;

        internal FTXSubClientConvert(FTXClient baseClient)
        {
            _baseClient = baseClient;
        }

        /// <summary>
        /// Create a new quote request
        /// </summary>
        /// <param name="fromAsset">From asset</param>
        /// <param name="toAsset">To asset</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXConvertQuoteResult>> CreateQuoteRequestAsync(string fromAsset, string toAsset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("fromCoin", fromAsset);
            parameters.AddParameter("toCoin", toAsset);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendFTXRequest<FTXConvertQuoteResult>(_baseClient.GetUri("otc/quotes"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get quote status
        /// </summary>
        /// <param name="quoteId">Quote id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXConvertQuote>>> GetQuoteStatusAsync(long quoteId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXConvertQuote>>(_baseClient.GetUri("otc/quotes/" + quoteId), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Accept a convert quote
        /// </summary>
        /// <param name="quoteId">Id of quote to accept</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult> AcceptQuoteAsync(long quoteId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest(_baseClient.GetUri("otc/quotes"), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
        }
    }
}
