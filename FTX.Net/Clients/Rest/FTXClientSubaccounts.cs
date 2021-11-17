using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Interfaces.Clients.Rest;
using FTX.Net.Objects.Models;
using FTX.Net.Objects.Models.Subaccounts;

namespace FTX.Net.Clients.Rest
{
    /// <summary>
    /// Sub account endpoints
    /// </summary>
    public class FTXClientSubaccounts : IFTXClientSubaccounts
    {
        private readonly FTXClient _baseClient;

        internal FTXClientSubaccounts(FTXClient baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXSubaccount>>> GetSubaccountsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXSubaccount>>(_baseClient.GetUri("subaccounts"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXSubaccount>> CreateSubaccountAsync(string nickname, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nickname", nickname);
            return await _baseClient.SendFTXRequest<FTXSubaccount>(_baseClient.GetUri("subaccounts"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> ChangeSubaccountNameAsync(string oldName, string newName, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nickname", oldName);
            parameters.AddParameter("newNickname", newName);
            return await _baseClient.SendFTXRequest(_baseClient.GetUri("subaccounts/update_name"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> DeleteSubaccountAsync(string nickname, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nickname", nickname);
            return await _baseClient.SendFTXRequest(_baseClient.GetUri("subaccounts"), HttpMethod.Delete, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXBalance>>> GetSubaccountBalancesAsync(string nickname, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXBalance>>(_baseClient.GetUri($"subaccounts/{nickname}/balances"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXSubaccountTransfer>> TransferAsync(string source, string destination, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("source", source);
            parameters.AddParameter("destination", destination);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("coin", asset);
            return await _baseClient.SendFTXRequest<FTXSubaccountTransfer>(_baseClient.GetUri("subaccounts/transfer"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }
    }
}
