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

namespace FTX.Net.Clients.Rest
{
    public class FTXClientExchangeData : IFTXClientExchangeData
    {
        private readonly FTXClient _baseClient;

        internal FTXClientExchangeData(FTXClient baseClient)
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
