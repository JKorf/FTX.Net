using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Objects.Models;

namespace FTX.Net.Interfaces.Clients.Rest
{
    public interface IFTXClientAccount
    {

        /// <summary>
        /// Get account info
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXAccountInfo>> GetAccountInfoAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get positions
        /// </summary>
        /// <param name="showAveragePrice"></param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXPosition>>> GetPositionsAsync(bool? showAveragePrice = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Change account leverage
        /// </summary>
        /// <param name="leverage">Desired acccount-wide leverage setting</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> ChangeAccountLeverageAsync(decimal leverage, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of balances
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXBalance>>> GetBalancesAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of balances, including master and subaccounts
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, IEnumerable<FTXBalance>>>> GetAllAccountBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get deposit address for an asset
        /// </summary>
        /// <param name="asset">Asset to get address for</param>
        /// <param name="network">The network to use</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXDeposit>>> GetDepositHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal history
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXWithdrawal>>> GetWithdrawalHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Submit a withdraw request
        /// </summary>
        /// <param name="asset">Asset to withdraw</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="address">Address to withdraw to</param>
        /// <param name="tag">Address tag</param>
        /// <param name="network">Network to us</param>
        /// <param name="password">Withdrawal password if required</param>
        /// <param name="code">Two factor authentication code if required</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXWithdrawal>> WithdrawAsync(string asset, decimal quantity, string address, string? tag = null, string? network = null, string? password = null, string? code = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get airdrops
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXAirdrop>>> GetAirdropsAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal fees
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="address">Address</param>
        /// <param name="tag">Tag</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXWithdrawalFee>> GetWithdrawalFeesAsync(string asset, decimal quantity, string address, string? tag = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get saved addresses
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXSavedAddress>>> GetSavedAddressesAsync(string? asset = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Create a saved address
        /// </summary>
        /// <param name="asset">Asset the address is for</param>
        /// <param name="address">The address</param>
        /// <param name="addressName">Name of the address</param>
        /// <param name="isPrimeTrust">Is prime trust</param>
        /// <param name="tag">Address tag</param>
        /// <param name="code">2FA code if needed</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXSavedAddress>> CreateSavedAddressAsync(string asset, string address, string addressName, bool isPrimeTrust, string? tag = null, string? code = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Delete a saved address
        /// </summary>
        /// <param name="savedAddressId">Id of the saved address to delete</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string>> DeleteSavedAddressAsync(long savedAddressId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of funding payments
        /// </summary>
        /// <param name="future">Filter by future</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXFundingPayment>>> GetFundingPaymentsAsync(string? future = null, DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

    }
}
