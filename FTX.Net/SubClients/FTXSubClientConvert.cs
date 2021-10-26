using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.Convert;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Interfaces.SubClients;

namespace FTX.Net.SubClients
{
    /// <summary>
    /// Convert endpoints
    /// </summary>
    public class FTXSubClientConvert : IFTXSubClientConvert
    {
        private readonly FTXClient _baseClient;

        internal FTXSubClientConvert(FTXClient baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXConvertQuoteResult>> CreateQuoteRequestAsync(string fromAsset, string toAsset, decimal quantity, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("fromCoin", fromAsset);
            parameters.AddParameter("toCoin", toAsset);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendFTXRequest<FTXConvertQuoteResult>(_baseClient.GetUri("otc/quotes"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXConvertQuote>>> GetQuoteStatusAsync(long quoteId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXConvertQuote>>(_baseClient.GetUri("otc/quotes/" + quoteId), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> AcceptQuoteAsync(long quoteId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest(_baseClient.GetUri("otc/quotes"), HttpMethod.Post, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }
    }
}
