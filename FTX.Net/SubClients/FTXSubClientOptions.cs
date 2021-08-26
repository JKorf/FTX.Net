using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using FTX.Net.Converters;
using FTX.Net.Enums;
using FTX.Net.Objects.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Interfaces.SubClients;

namespace FTX.Net.SubClients
{
    /// <summary>
    /// Options endpoints
    /// </summary>
    public class FTXSubClientOptions : IFTXSubClientOptions
    {
        private readonly FTXClient _baseClient;
        internal FTXSubClientOptions(FTXClient baseClient)
        {
            _baseClient = baseClient;
        }

        /// <summary>
        /// Get list of quote requests
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXQuoteRequest>>> GetQuoteRequestsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXQuoteRequest>>(_baseClient.GetUri("options/requests"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get list of quote requests for the user
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXQuoteRequest>>> GetUserQuoteRequestsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXQuoteRequest>>(_baseClient.GetUri("options/my_requests"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }


        /// <summary>
        /// Create quote request
        /// </summary>
        /// <param name="underlying">Underlying</param>
        /// <param name="type">Type</param>
        /// <param name="strike">Strike</param>
        /// <param name="expiry">Must be in the future and at 03:00 UTC.</param>
        /// <param name="side">Side</param>
        /// <param name="size">Size</param>
        /// <param name="limitPrice">Limit price</param>
        /// <param name="hideLimitPrice">Whether or not to hide your limit price from potential quoters, default true</param>
        /// <param name="requestExpiry">Request expiry</param>
        /// <param name="counterPartyId">When specified, makes the request private to the specified counterparty</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXUserQuoteRequest>> CreateQuoteRequestAsync(string underlying, OptionType type, decimal strike, DateTime expiry, OrderSide side, decimal size, decimal? limitPrice = null, bool? hideLimitPrice = null, DateTime? requestExpiry = null, long? counterPartyId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("underlying", underlying);
            parameters.AddParameter("type", JsonConvert.SerializeObject(type, new OptionTypeConverter(false)));
            parameters.AddParameter("strike", strike.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("expiry", JsonConvert.SerializeObject(expiry, new TimestampConverter()));
            parameters.AddParameter("side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddParameter("size", size.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limitPrice", limitPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("hideLimitPrice", hideLimitPrice);
            parameters.AddOptionalParameter("requestExpiry", requestExpiry.HasValue ? JsonConvert.SerializeObject(expiry, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("counterPartyId", counterPartyId);
            return await _baseClient.SendFTXRequest<FTXUserQuoteRequest>(_baseClient.GetUri("options/requests"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel a quote request
        /// </summary>
        /// <param name="requestId">Request id to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXUserQuoteRequest>> CancelQuoteRequestAsync(long requestId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXUserQuoteRequest>(_baseClient.GetUri("options/requests/" + requestId), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get quotes for your quote request
        /// </summary>
        /// <param name="requestId">Request id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXQuoteRequestQuote>>> GetQuotesForQuoteRequestAsync(long requestId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXQuoteRequestQuote>>(_baseClient.GetUri($"options/requests/{requestId}/quotes"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Create quote
        /// </summary>
        /// <param name="requestId">Request id</param>
        /// <param name="price">Price of the quote</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXUserQuoteRequest>> CreateQuoteAsync(long requestId, decimal price, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("price", price.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXUserQuoteRequest>(_baseClient.GetUri($"options/requests/{requestId}/quotes"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get quotes for user
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXQuoteRequestQuote>>> GetUserQuotesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXQuoteRequestQuote>>(_baseClient.GetUri($"options/my_quotes"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }


        /// <summary>
        /// Cancel a quote
        /// </summary>
        /// <param name="quoteId">Quote id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXQuoteRequestQuote>> CancelQuoteAsync(long quoteId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXQuoteRequestQuote>(_baseClient.GetUri($"options/quotes/" + quoteId), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Accept options quote
        /// </summary>
        /// <param name="quoteId">Quote id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXQuoteRequestQuote>> AcceptQuoteAsync(long quoteId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXQuoteRequestQuote>(_baseClient.GetUri($"options/quotes/{quoteId}/accept"), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get account options info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXOptionsAccountInfo>> GetAccountOptionsInfoAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXOptionsAccountInfo>(_baseClient.GetUri($"options/account_info"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get options positions
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXOptionsPosition>>> GetOptionsPositionsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXOptionsPosition>>(_baseClient.GetUri($"options/positions"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get public options positions
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXOptionTrade>>> GetOptionTradesAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            _baseClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXOptionTrade>>(_baseClient.GetUri($"options/trades"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get options fills
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXUserOptionTrade>>> GetUserOptionTradesAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            _baseClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXUserOptionTrade>>(_baseClient.GetUri($"options/fills"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get 24H option volume
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXOptionsVolume>> GetOptionVolumeAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXOptionsVolume>(_baseClient.GetUri($"stats/24h_options_volume"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get historical option volume
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXOptionsHistoricalVolume>>> GetOptionsHistoricalVolumeAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            _baseClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXOptionsHistoricalVolume>>(_baseClient.GetUri($"options/historical_volumes/BTC"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get open interest
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXOptionOpenInterest>> GetOptionsOpenInterestAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXOptionOpenInterest>(_baseClient.GetUri($"options/open_interest/BTC"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get open interest history
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXOptionHistoricalOpenInterest>>> GetOptionHistoricalOpenInterestAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            _baseClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXOptionHistoricalOpenInterest>>(_baseClient.GetUri($"options/historical_open_interest/BTC"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }
    }
}
