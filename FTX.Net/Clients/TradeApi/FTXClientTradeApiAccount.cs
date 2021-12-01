using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Objects.Models;
using FTX.Net.Objects.Models.LeveragedTokens;
using FTX.Net.Objects.Models.Options;
using FTX.Net.Interfaces.Clients.TradeApi;

namespace FTX.Net.Clients.TradeApi
{
    public class FTXClientTradeApiAccount : IFTXClientTradeApiAccount
    {
        private readonly FTXClientTradeApi _baseClient;

        internal FTXClientTradeApiAccount(FTXClientTradeApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXBalance>>> GetBalancesAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXBalance>>(_baseClient.GetUri("wallet/balances"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, IEnumerable<FTXBalance>>>> GetAllAccountBalancesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<Dictionary<string, IEnumerable<FTXBalance>>>(_baseClient.GetUri("wallet/all_balances"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("method", network);
            return await _baseClient.SendFTXRequest<FTXDepositAddress>(_baseClient.GetUri("wallet/deposit_address/" + asset), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXDeposit>>> GetDepositHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXDeposit>>(_baseClient.GetUri("wallet/deposits"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXWithdrawal>>> GetWithdrawalHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXWithdrawal>>(_baseClient.GetUri("wallet/withdrawals"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXWithdrawal>> WithdrawAsync(string asset, decimal quantity, string address, string? tag = null, string? network = null, string? password = null, string? code = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("coin", asset);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("address", address);
            parameters.AddOptionalParameter("tag", tag);
            parameters.AddOptionalParameter("method", network);
            parameters.AddOptionalParameter("password", password);
            parameters.AddOptionalParameter("code", code);
            return await _baseClient.SendFTXRequest<FTXWithdrawal>(_baseClient.GetUri("wallet/withdrawals"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXAirdrop>>> GetAirdropsAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXAirdrop>>(_baseClient.GetUri("wallet/airdrops"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXWithdrawalFee>> GetWithdrawalFeesAsync(string asset, decimal quantity, string address, string? tag = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("coin", asset);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("address", address);
            parameters.AddOptionalParameter("tag", tag);
            return await _baseClient.SendFTXRequest<FTXWithdrawalFee>(_baseClient.GetUri("wallet/withdrawal_fee"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXSavedAddress>>> GetSavedAddressesAsync(string? asset = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("coin", asset);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXSavedAddress>>(_baseClient.GetUri("wallet/saved_addresses"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXSavedAddress>> CreateSavedAddressAsync(string asset, string address, string addressName, bool isPrimeTrust, string? tag = null, string? code = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("coin", asset);
            parameters.AddParameter("address", address);
            parameters.AddParameter("addressName", addressName);
            parameters.AddParameter("isPrimeTrust", isPrimeTrust);
            parameters.AddOptionalParameter("tag", tag);
            parameters.AddOptionalParameter("code", code);
            return await _baseClient.SendFTXRequest<FTXSavedAddress>(_baseClient.GetUri("wallet/saved_addresses"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<string>> DeleteSavedAddressAsync(long savedAddressId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<string>(_baseClient.GetUri("wallet/saved_addresses/" + savedAddressId), HttpMethod.Delete, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXFundingPayment>>> GetFundingPaymentsAsync(string? future = null, DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("future", future);
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXFundingPayment>>(_baseClient.GetUri("funding_payments"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXAccountInfo>> GetAccountInfoAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXAccountInfo>(_baseClient.GetUri("account"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXPosition>>> GetPositionsAsync(bool? showAveragePrice = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("showAvgPrice", showAveragePrice);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXPosition>>(_baseClient.GetUri("positions"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> ChangeAccountLeverageAsync(decimal leverage, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("leverage", leverage.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest(_baseClient.GetUri("account/leverage"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        #region Leveraged tokens

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXLeveragedTokenBalance>>> GetLeveragedTokenBalancesAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLeveragedTokenBalance>>(_baseClient.GetUri("lt/balances"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        #endregion

        #region Options

        /// <inheritdoc />
        public async Task<WebCallResult<FTXOptionsAccountInfo>> GetOptionsAccountInfoAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXOptionsAccountInfo>(_baseClient.GetUri("options/account_info"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXOptionsPosition>>> GetOptionsPositionsAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXOptionsPosition>>(_baseClient.GetUri("options/positions"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        #endregion
    }
}
