using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects;
using FTX.Net.Objects.Subaccounts;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Interfaces.SubClients;

namespace FTX.Net.SubClients
{
    /// <summary>
    /// Sub account endpoints
    /// </summary>
    public class FTXSubClientSubaccounts : IFTXSubClientSubaccounts
    {
        private readonly FTXClient _baseClient;

        internal FTXSubClientSubaccounts(FTXClient baseClient)
        {
            _baseClient = baseClient;
        }

        /// <summary>
        /// Get list of sub accounts
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXSubaccount>>> GetSubaccountsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXSubaccount>>(_baseClient.GetUri("subaccounts"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a new sub client
        /// </summary>
        /// <param name="nickname">Name of the subaccount</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXSubaccount>> CreateSubaccountAsync(string nickname, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nickname", nickname);
            return await _baseClient.SendFTXRequest<FTXSubaccount>(_baseClient.GetUri("subaccounts"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Change the name of a sub account
        /// </summary>
        /// <param name="oldName">Old name</param>
        /// <param name="newName">New name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult> ChangeSubaccountNameAsync(string oldName, string newName, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nickname", oldName);
            parameters.AddParameter("newNickname", newName);
            return await _baseClient.SendFTXRequest(_baseClient.GetUri("subaccounts/update_name"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a subaccount
        /// </summary>
        /// <param name="nickname">Nickname of account to delete</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult> DeleteSubaccountAsync(string nickname, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nickname", nickname);
            return await _baseClient.SendFTXRequest(_baseClient.GetUri("subaccounts"), HttpMethod.Delete, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get subaccount balances
        /// </summary>
        /// <param name="nickname">Nickname to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXBalance>>> GetSubaccountBalancesAsync(string nickname, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXBalance>>(_baseClient.GetUri($"subaccounts/{nickname}/balances"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Transfer funds between subaccounts
        /// </summary>
        /// <param name="source">Name of the source subaccount. Use 'main' for the main account</param>
        /// <param name="destination">Name of the destination subaccount. Use 'main' for the main account</param>
        /// <param name="asset">Asset to move</param>
        /// <param name="quantity">Quantity to move</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXSubaccountTransfer>> TransferAsync(string source, string destination, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("source", source);
            parameters.AddParameter("destination", destination);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("coin", asset);
            return await _baseClient.SendFTXRequest<FTXSubaccountTransfer>(_baseClient.GetUri($"subaccounts/transfer"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

    }
}
