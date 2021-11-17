using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.Models;
using FTX.Net.Objects.Models.Subaccounts;

namespace FTX.Net.Interfaces.Clients.Rest
{
    /// <summary>
    /// Subaccount endpoints
    /// </summary>
    public interface IFTXClientSubaccounts
    {
        /// <summary>
        /// Get list of sub accounts
        /// <para><a href="https://docs.ftx.com/#get-all-subaccounts" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXSubaccount>>> GetSubaccountsAsync(CancellationToken ct = default);

        /// <summary>
        /// Create a new sub client
        /// <para><a href="https://docs.ftx.com/#create-subaccount" /></para>
        /// </summary>
        /// <param name="nickname">Name of the subaccount</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXSubaccount>> CreateSubaccountAsync(string nickname, CancellationToken ct = default);

        /// <summary>
        /// Change the name of a sub account
        /// <para><a href="https://docs.ftx.com/#change-subaccount-name" /></para>
        /// </summary>
        /// <param name="oldName">Old name</param>
        /// <param name="newName">New name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> ChangeSubaccountNameAsync(string oldName, string newName, CancellationToken ct = default);

        /// <summary>
        /// Delete a subaccount
        /// <para><a href="https://docs.ftx.com/#delete-subaccount" /></para>
        /// </summary>
        /// <param name="nickname">Nickname of account to delete</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> DeleteSubaccountAsync(string nickname, CancellationToken ct = default);

        /// <summary>
        /// Get subaccount balances
        /// <para><a href="https://docs.ftx.com/#get-subaccount-balances" /></para>
        /// </summary>
        /// <param name="nickname">Nickname to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXBalance>>> GetSubaccountBalancesAsync(string nickname, CancellationToken ct = default);

        /// <summary>
        /// Transfer funds between subaccounts
        /// <para><a href="https://docs.ftx.com/#transfer-between-subaccounts" /></para>
        /// </summary>
        /// <param name="source">Name of the source subaccount. Use 'main' for the main account</param>
        /// <param name="destination">Name of the destination subaccount. Use 'main' for the main account</param>
        /// <param name="asset">Asset to move</param>
        /// <param name="quantity">Quantity to move</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXSubaccountTransfer>> TransferAsync(string source, string destination, string asset, decimal quantity, CancellationToken ct = default);
    }
}