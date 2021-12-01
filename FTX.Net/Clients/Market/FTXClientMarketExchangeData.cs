using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using FTX.Net.Enums;
using FTX.Net.Interfaces.Clients.Rest;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Objects.Models;
using FTX.Net.Clients.Market;
using FTX.Net.Objects.Models.LeveragedTokens;
using FTX.Net.Objects.Models.Options;

namespace FTX.Net.Clients.Rest
{
    public class FTXClientMarketExchangeData : IFTXClientMarketExchangeData
    {
        private readonly FTXClientMarket _baseClient;

        internal FTXClientMarketExchangeData(FTXClientMarket baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXAsset>>> GetAssetsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXAsset>>(_baseClient.GetUri("wallet/coins"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<DateTime>(new Uri("https://otc.ftx.com/api/time"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXSymbol>>> GetSymbolsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXSymbol>>(_baseClient.GetUri("markets"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXSymbol>(_baseClient.GetUri("markets/" + symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXOrderbook>> GetOrderBookAsync(string symbol, int depth, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("depth", depth);
            return await _baseClient.SendFTXRequest<FTXOrderbook>(_baseClient.GetUri($"markets/{symbol}/orderbook"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXTrade>>> GetTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXTrade>>(_baseClient.GetUri($"markets/{symbol}/trades"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("resolution", GetResolutionFromKlineInterval(interval));
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXKline>>(_baseClient.GetUri($"markets/{symbol}/candles"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXFuture>>> GetFuturesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXFuture>>(_baseClient.GetUri("futures"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXFuture>> GetFutureAsync(string future, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXFuture>(_baseClient.GetUri("futures/" + future), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXFutureStat>> GetFutureStatsAsync(string future, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXFutureStat>(_baseClient.GetUri($"futures/{future}/stats"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXFundingRate>>> GetFundingRatesAsync(string? future = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            parameters.AddOptionalParameter("future", future);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXFundingRate>>(_baseClient.GetUri("funding_rates"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, decimal>>> GetIndexWeightsAsync(string index, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<Dictionary<string, decimal>>(_baseClient.GetUri($"indexes/{index}/weights"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXFuture>>> GetExpiredFuturesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXFuture>>(_baseClient.GetUri("expired_futures"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXKline>>> GetIndexKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("resolution", GetResolutionFromKlineInterval(interval));
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXKline>>(_baseClient.GetUri($"indexes/{symbol}/candles"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #region Leveraged tokens

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXLeveragedToken>>> GetLeveragedTokensAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLeveragedToken>>(_baseClient.GetUri("lt/tokens"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXLeveragedToken>> GetLeveragedTokenAsync(string tokenName, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXLeveragedToken>(_baseClient.GetUri("lt/" + tokenName), HttpMethod.Get, ct).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, FTXETFRebalanceEntry>>> GetETFRebalanceInfoAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            // This call returns internal data with additional quotes which make direct deserialization fail. So first get the string value and then deserialize that
            //return await _baseClient.SendFTXRequest<Dictionary<string, FTXETFRebalanceEntry>>(_baseClient.GetUri("etfs/rebalance_info"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);

            var data = await _baseClient.SendFTXRequest<string>(_baseClient.GetUri("etfs/rebalance_info"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);

            if (!data)
                return data.As<Dictionary<string, FTXETFRebalanceEntry>>(null);

            var deserializeResult = _baseClient.DeserializeInternal<Dictionary<string, FTXETFRebalanceEntry>>(data.Data);
            if (!deserializeResult)
                return data.As<Dictionary<string, FTXETFRebalanceEntry>>(null);

            return data.As(deserializeResult.Data);
        }

        #endregion

        #region Options


        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXQuoteRequest>>> GetOptionsQuoteRequestsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXQuoteRequest>>(_baseClient.GetUri("options/requests"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXOptionTrade>>> GetOptionsTradesAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXOptionTrade>>(_baseClient.GetUri("options/trades"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXOptionsVolume>> GetOptionsVolumeAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXOptionsVolume>(_baseClient.GetUri("stats/24h_options_volume"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXOptionsHistoricalVolume>>> GetOptionsHistoricalVolumeAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXOptionsHistoricalVolume>>(_baseClient.GetUri("options/historical_volumes/BTC"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXOptionOpenInterest>> GetOptionsOpenInterestAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXOptionOpenInterest>(_baseClient.GetUri("options/open_interest/BTC"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXOptionHistoricalOpenInterest>>> GetOptionsHistoricalOpenInterestAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXOptionHistoricalOpenInterest>>(_baseClient.GetUri("options/historical_open_interest/BTC"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        private static int GetResolutionFromKlineInterval(KlineInterval interval)
        {
            return interval switch
            {
                KlineInterval.FifteenSeconds => 15,
                KlineInterval.OneMinute => 60,
                KlineInterval.FiveMinutes => 300,
                KlineInterval.FifteenMinutes => 900,
                KlineInterval.OneHour => 3600,
                KlineInterval.FourHours => 14400,
                KlineInterval.OneDay => 86400,
                KlineInterval.OneWeek => 86400 * 7,
                KlineInterval.OneMonth => 86400 * 30,
                _ => throw new Exception("Unknown kline interval"),
            };
        }
    }
}
