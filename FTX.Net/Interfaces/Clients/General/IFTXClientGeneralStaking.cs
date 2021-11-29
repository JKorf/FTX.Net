using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.Models.Staking;

namespace FTX.Net.Interfaces.Clients.Rest
{
    /// <summary>
    /// FTX staking endpoints
    /// </summary>
    public interface IFTXClientGeneralStaking
    {
        /// <summary>
        /// Get list of stakes for the user
        /// <para><a href="https://docs.ftx.com/#get-stakes" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXStake>>> GetStakesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get list of unstake requests for the user
        /// <para><a href="https://docs.ftx.com/#unstake-request" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXUnstakeRequest>>> GetUnstakeRequestsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get list of stake balances
        /// <para><a href="https://docs.ftx.com/#get-stake-balances" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXStakeBalance>>> GetStakeBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Create a new unstake request
        /// <para><a href="https://docs.ftx.com/#unstake-request-2" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="quantity">Quantity to unstake</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXUnstakeRequest>> RequestUnstakeAsync(string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Cancel an unstake request
        /// <para><a href="https://docs.ftx.com/#cancel-unstake-request" /></para>
        /// </summary>
        /// <param name="requestId">Id of request to unstake</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string[]>> CancelUnstakeRequestAsync(long requestId, CancellationToken ct = default);

        /// <summary>
        /// Get list of staking rewards
        /// <para><a href="https://docs.ftx.com/#get-staking-rewards" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXStakeReward>>> GetStakingRewardsAsync(CancellationToken ct = default);

        /// <summary>
        /// Create a new stake request
        /// <para><a href="https://docs.ftx.com/#stake-request" /></para>
        /// </summary>
        /// <param name="asset">Asset to stake</param>
        /// <param name="quantity">Quantity to stake</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXStake>> StakeAsync(string asset, decimal quantity, CancellationToken ct = default);
    }
}