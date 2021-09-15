using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.LeveragedTokens;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Interfaces.SubClients;

namespace FTX.Net.SubClients
{
    /// <summary>
    /// Leveraged token endpoints
    /// </summary>
    public class FTXSubClientLeveragedTokens : IFTXSubClientLeveragedTokens
    {
        private readonly FTXClient _baseClient;

        internal FTXSubClientLeveragedTokens(FTXClient baseClient)
        {
            _baseClient = baseClient;
        }

        /// <summary>
        /// Get list of funding payments
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXLeveragedToken>>> GetLeveragedTokensAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLeveragedToken>>(_baseClient.GetUri("lt/tokens"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get info on a token
        /// </summary>
        /// <param name="tokenName">Name of the token</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXLeveragedToken>> GetLeveragedTokenAsync(string tokenName, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXLeveragedToken>(_baseClient.GetUri("lt/" + tokenName), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get token balances
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXLeveragedTokenBalance>>> GetLeveragedTokenBalancesAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLeveragedTokenBalance>>(_baseClient.GetUri("lt/balances"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <summary>
        /// Get creation requests
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXLeveragedTokenCreationRequest>>> GetLeveragedTokenCreationRequestsAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLeveragedTokenCreationRequest>>(_baseClient.GetUri("lt/creations"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <summary>
        /// Request leveraged token creation
        /// </summary>
        /// <param name="tokenName">Name of the token</param>
        /// <param name="size">Number of tokens to create</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXLeveragedTokenCreationRequest>>> RequestLeveragedTokenCreationAsync(string tokenName, decimal size, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("size", size.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLeveragedTokenCreationRequest>>(_baseClient.GetUri($"lt/{tokenName}/create"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <summary>
        /// Get redemption requests
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXLeveragedTokenRedemption>>> GetLeveragedTokenRedemptionRequestsAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLeveragedTokenRedemption>>(_baseClient.GetUri("lt/redemptions"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <summary>
        /// Request leveraged token creation
        /// </summary>
        /// <param name="tokenName">Name of the token</param>
        /// <param name="size">Number of tokens to create</param>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXLeveragedTokenRedeemRequest>>> RequestLeveragedTokenRedemptionAsync(string tokenName, decimal size, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("size", size.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLeveragedTokenRedeemRequest>>(_baseClient.GetUri($"lt/{tokenName}/redeem"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <summary>
        ///Provides information about the most recent rebalance of each ETF.
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<Dictionary<string, FTXETFRebalanceEntry>>> GetETFRebalanceInfoAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            // This call returns internal data with additional quotes which make direct deserialization fail. So first get the string value and then deserialize that
            var data = await _baseClient.SendFTXRequest<string>(_baseClient.GetUri("etfs/rebalance_info"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
            if (!data)
                return data.As<Dictionary<string, FTXETFRebalanceEntry>>(null);

            var deserializeResult = _baseClient.DeserializeInternal<Dictionary<string, FTXETFRebalanceEntry>>(data.Data);
            if (!deserializeResult)
                return data.As<Dictionary<string, FTXETFRebalanceEntry>>(null);

            return data.As(deserializeResult.Data);
        }
    }
}
