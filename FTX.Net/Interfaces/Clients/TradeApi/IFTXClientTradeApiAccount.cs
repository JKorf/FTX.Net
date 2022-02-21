using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Objects.Models;
using FTX.Net.Objects.Models.Options;
using FTX.Net.Objects.Models.LeveragedTokens;

namespace FTX.Net.Interfaces.Clients.TradeApi
{
    /// <summary>
    /// FTX account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IFTXClientTradeApiAccount
    {
        /// <summary>
        /// Get account info
        /// <para><a href="https://docs.ftx.com/#get-account-information" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXAccountInfo>> GetAccountInfoAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get positions
        /// <para><a href="https://docs.ftx.com/#get-positions" /></para>
        /// </summary>
        /// <param name="showAveragePrice"></param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXPosition>>> GetPositionsAsync(bool? showAveragePrice = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Change account leverage
        /// <para><a href="https://docs.ftx.com/#change-account-leverage" /></para>
        /// </summary>
        /// <param name="leverage">Desired acccount-wide leverage setting</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> ChangeAccountLeverageAsync(decimal leverage, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of balances
        /// <para><a href="https://docs.ftx.com/#get-balances" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXBalance>>> GetBalancesAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of balances, including master and subaccounts
        /// <para><a href="https://docs.ftx.com/#get-balances-of-all-accounts" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, IEnumerable<FTXBalance>>>> GetAllAccountBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get deposit address for an asset
        /// <para><a href="https://docs.ftx.com/#get-deposit-address" /></para>
        /// </summary>
        /// <param name="asset">Asset to get address for</param>
        /// <param name="network">The network to use</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// <para><a href="https://docs.ftx.com/#get-deposit-history" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXDeposit>>> GetDepositHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal history
        /// <para><a href="https://docs.ftx.com/#get-withdrawal-history" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXWithdrawal>>> GetWithdrawalHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Submit a withdraw request
        /// <para><a href="https://docs.ftx.com/#request-withdrawal" /></para>
        /// </summary>
        /// <param name="asset">Asset to withdraw</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="address">Address to withdraw to</param>
        /// <param name="tag">Address tag</param>
        /// <param name="network">Network to use</param>
        /// <param name="password">Withdrawal password if required</param>
        /// <param name="code">Two factor authentication code if required</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXWithdrawal>> WithdrawAsync(string asset, decimal quantity, string address, string? tag = null, string? network = null, string? password = null, string? code = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get airdrops
        /// <para><a href="https://docs.ftx.com/#get-airdrops" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXAirdrop>>> GetAirdropsAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal fees
        /// <para><a href="https://docs.ftx.com/#get-withdrawal-fees" /></para>
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
        /// <para><a href="https://docs.ftx.com/#get-saved-addresses" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXSavedAddress>>> GetSavedAddressesAsync(string? asset = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Create a saved address
        /// <para><a href="https://docs.ftx.com/#create-saved-addresses" /></para>
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
        /// <para><a href="https://docs.ftx.com/#delete-saved-addresses" /></para>
        /// </summary>
        /// <param name="savedAddressId">Id of the saved address to delete</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string>> DeleteSavedAddressAsync(long savedAddressId, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of funding payments
        /// <para><a href="https://docs.ftx.com/#funding-payments" /></para>
        /// </summary>
        /// <param name="future">Filter by future</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXFundingPayment>>> GetFundingPaymentsAsync(string? future = null, DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get account options info
        /// <para><a href="https://docs.ftx.com/#get-account-options-info" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOptionsAccountInfo>> GetOptionsAccountInfoAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get options positions
        /// <para><a href="https://docs.ftx.com/#get-options-positions" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXOptionsPosition>>> GetOptionsPositionsAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get token balances
        /// <para><a href="https://docs.ftx.com/#get-leveraged-token-balances" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLeveragedTokenBalance>>> GetLeveragedTokenBalancesAsync(string? subaccountName = null, CancellationToken ct = default);

    }
}
