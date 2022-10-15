using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Objects.Models.Convert;
using FTX.Net.Interfaces.Clients.GeneralApi;

namespace FTX.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    public class FTXClientGeneralApiConvert : IFTXClientGeneralApiConvert
    {
        private readonly FTXClientGeneralApi _baseClient;

        internal FTXClientGeneralApiConvert(FTXClientGeneralApi baseClient)
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

            return await _baseClient.SendFTXRequest<FTXConvertQuoteResult>(_baseClient.GetUri("otc/quotes"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: _baseClient._baseClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXConvertQuote>> GetQuoteStatusAsync(long quoteId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXConvertQuote>(_baseClient.GetUri("otc/quotes/" + quoteId), HttpMethod.Get, ct, signed: true, additionalHeaders: _baseClient._baseClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> AcceptQuoteAsync(long quoteId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest(_baseClient.GetUri($"otc/quotes/{quoteId}/accept"), HttpMethod.Post, ct, signed: true, additionalHeaders: _baseClient._baseClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }
    }
}
