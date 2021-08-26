using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.Staking;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Interfaces.SubClients;

namespace FTX.Net.SubClients
{
    /// <summary>
    /// Staking endpoints
    /// </summary>
    public class FTXSubClientStaking : IFTXSubClientStaking
    {
        private readonly FTXClient _baseClient;

        internal FTXSubClientStaking(FTXClient baseClient)
        {
            _baseClient = baseClient;
        }

        /// <summary>
        /// Get list of stakes for the user
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXStake>>> GetStakesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXStake>>(_baseClient.GetUri("staking/stakes"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get list of unstake requests for the user
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXUnstakeRequest>>> GetUnstakeRequestsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXUnstakeRequest>>(_baseClient.GetUri("staking/unstake_requests"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get list of stake balances
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXStakeBalance>>> GetStakeBalancesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXStakeBalance>>(_baseClient.GetUri("staking/balances"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a new unstake request
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="quantity">Quantity to unstake</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXUnstakeRequest>> RequestUnstakeAsync(string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("coin", asset);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXUnstakeRequest>(_baseClient.GetUri("staking/unstake_requests"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel an unstake request
        /// </summary>
        /// <param name="requestId">Id of request to unstake</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXUnstakeRequest>> CancelUnstakeRequestAsync(long requestId, CancellationToken ct = default)
        {
            // Doesn't seem to work?
            return await _baseClient.SendFTXRequest<FTXUnstakeRequest>(_baseClient.GetUri("staking/unstake_requests/" + requestId), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get list of staking rewards
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXStakeReward>>> GetStakingRewardsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXStakeReward>>(_baseClient.GetUri("staking/staking_rewards"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }


        /// <summary>
        /// Create a new stake request
        /// </summary>
        /// <param name="asset">Asset to stake</param>
        /// <param name="quantity">Quantity to stake</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXStake>> StakeAsync(string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXStake>(_baseClient.GetUri("staking/stakes"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }
    }
}
