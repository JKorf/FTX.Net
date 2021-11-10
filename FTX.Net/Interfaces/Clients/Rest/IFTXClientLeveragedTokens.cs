using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.LeveragedTokens;

namespace FTX.Net.Interfaces.Clients.Rest
{
    /// <summary>
    /// Leveraged tokens endpoints
    /// </summary>
    public interface IFTXClientLeveragedTokens
    {
        /// <summary>
        /// Get list of funding payments
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLeveragedToken>>> GetLeveragedTokensAsync(CancellationToken ct = default);

        /// <summary>
        /// Get info on a token
        /// </summary>
        /// <param name="tokenName">Name of the token</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXLeveragedToken>> GetLeveragedTokenAsync(string tokenName, CancellationToken ct = default);

        /// <summary>
        /// Get token balances
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLeveragedTokenBalance>>> GetLeveragedTokenBalancesAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get creation requests
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLeveragedTokenCreationRequest>>> GetLeveragedTokenCreationRequestsAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Request leveraged token creation
        /// </summary>
        /// <param name="tokenName">Name of the token</param>
        /// <param name="size">Number of tokens to create</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXLeveragedTokenCreationRequest>> RequestLeveragedTokenCreationAsync(string tokenName, decimal size, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get redemption requests
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLeveragedTokenRedemption>>> GetLeveragedTokenRedemptionRequestsAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Request leveraged token creation
        /// </summary>
        /// <param name="tokenName">Name of the token</param>
        /// <param name="size">Number of tokens to create</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXLeveragedTokenRedeemRequest>> RequestLeveragedTokenRedemptionAsync(string tokenName, decimal size, string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        ///Provides information about the most recent rebalance of each ETF.
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, FTXETFRebalanceEntry>>> GetETFRebalanceInfoAsync(string? subaccountName = null, CancellationToken ct = default);
    }
}