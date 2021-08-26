using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.LeveragedTokens;

namespace FTX.Net.Interfaces.SubClients
{
    /// <summary>
    /// Leveraged tokens endpoints
    /// </summary>
    public interface IFTXSubClientLeveragedTokens
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
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLeveragedTokenBalance>>> GetLeveragedTokenBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get creation requests
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLeveragedTokenCreationRequest>>> GetLeveragedTokenCreationRequestsAsync(CancellationToken ct = default);

        /// <summary>
        /// Request leveraged token creation
        /// </summary>
        /// <param name="tokenName">Name of the token</param>
        /// <param name="size">Number of tokens to create</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLeveragedTokenCreationRequest>>> RequestLeveragedTokenCreationAsync(string tokenName, decimal size, CancellationToken ct = default);

        /// <summary>
        /// Get redemption requests
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLeveragedTokenRedemption>>> GetLeveragedTokenRedemptionRequestsAsync(CancellationToken ct = default);

        /// <summary>
        /// Request leveraged token creation
        /// </summary>
        /// <param name="tokenName">Name of the token</param>
        /// <param name="size">Number of tokens to create</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLeveragedTokenRedeemRequest>>> RequestLeveragedTokenRedemptionAsync(string tokenName, decimal size, CancellationToken ct = default);

        /// <summary>
        ///Provides information about the most recent rebalance of each ETF.
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, FTXETFRebalanceEntry>>> GetETFRebalanceInfoAsync(CancellationToken ct = default);
    }
}