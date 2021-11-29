using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.Models.LeveragedTokens;

namespace FTX.Net.Interfaces.Clients.Rest
{
    /// <summary>
    /// FTX leveraged tokens endpoints
    /// </summary>
    public interface IFTXClientMarketLeveragedTokens
    {
        /// <summary>
        /// Get list of leveraged tokens
        /// <para><a href="https://docs.ftx.com/#list-leveraged-tokens" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLeveragedToken>>> GetLeveragedTokensAsync(CancellationToken ct = default);

        /// <summary>
        /// Get info on a token
        /// <para><a href="https://docs.ftx.com/#get-token-info" /></para>
        /// </summary>
        /// <param name="tokenName">Name of the token</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXLeveragedToken>> GetLeveragedTokenAsync(string tokenName, CancellationToken ct = default);

        /// <summary>
        /// Get token balances
        /// <para><a href="https://docs.ftx.com/#get-leveraged-token-balances" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLeveragedTokenBalance>>> GetLeveragedTokenBalancesAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get creation requests
        /// <para><a href="https://docs.ftx.com/#list-leveraged-token-creation-requests" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLeveragedTokenCreationRequest>>> GetLeveragedTokenCreationRequestsAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Request leveraged token creation
        /// <para><a href="https://docs.ftx.com/#request-leveraged-token-creation" /></para>
        /// </summary>
        /// <param name="tokenName">Name of the token</param>
        /// <param name="size">Number of tokens to create</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXLeveragedTokenCreationRequest>> RequestLeveragedTokenCreationAsync(string tokenName, decimal size, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get redemption requests
        /// <para><a href="https://docs.ftx.com/#list-leveraged-token-redemption-requests" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLeveragedTokenRedemption>>> GetLeveragedTokenRedemptionRequestsAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Request leveraged token redemption
        /// <para><a href="https://docs.ftx.com/#request-leveraged-token-redemption" /></para>
        /// </summary>
        /// <param name="tokenName">Name of the token</param>
        /// <param name="size">Number of tokens to create</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXLeveragedTokenRedeemRequest>> RequestLeveragedTokenRedemptionAsync(string tokenName, decimal size, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Provides information about the most recent rebalance of each ETF.
        /// <para><a href="https://docs.ftx.com/#request-etf-rebalance-info" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, FTXETFRebalanceEntry>>> GetETFRebalanceInfoAsync(string? subaccountName = null, CancellationToken ct = default);
    }
}