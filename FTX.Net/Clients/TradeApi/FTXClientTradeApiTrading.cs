using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Objects.Models;
using FTX.Net.Objects.Models.LeveragedTokens;
using FTX.Net.Objects.Models.Options;
using CryptoExchange.Net.Converters;
using FTX.Net.Interfaces.Clients.TradeApi;
using CryptoExchange.Net.ComonObjects;

namespace FTX.Net.Clients.TradeApi
{
    /// <inheritdoc />
    public class FTXClientTradeApiTrading : IFTXClientTradeApiTrading
    {
        private readonly FTXClientTradeApi _baseClient;

        internal FTXClientTradeApiTrading(FTXClientTradeApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXOrder>> PlaceOrderAsync(string symbol, Enums.OrderSide side, Enums.OrderType type, decimal quantity, decimal? price = null, bool? reduceOnly = null, bool? immediateOrCancel = null, bool? postOnly = null, string? clientOrderId = null, bool? rejectOnPriceBand = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "market", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) },
                { "size", quantity.ToString(CultureInfo.InvariantCulture) },
                { "price", price?.ToString(CultureInfo.InvariantCulture)! } // Should still be send even when it's null
            };

            parameters.AddOptionalParameter("reduceOnly", reduceOnly);
            parameters.AddOptionalParameter("ioc", immediateOrCancel);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("clientId", clientOrderId);
            parameters.AddOptionalParameter("rejectOnPriceBand", rejectOnPriceBand);
            parameters.AddOptionalParameter("externalReferralProgram", _baseClient.ClientOptions.AffiliateCode);

            var result = await _baseClient.SendFTXRequest<FTXOrder>(_baseClient.GetUri("orders"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderPlaced(new OrderId { SourceObject = result.Data, Id = result.Data.Id.ToString(CultureInfo.InvariantCulture) });
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXTriggerOrder>> PlaceTriggerOrderAsync(
            // Basic params
            string symbol,
            Enums.OrderSide side,
            TriggerOrderType type,
            decimal quantity,
            bool? reduceOnly = null,
            bool? retryUntilFilled = null,

            // Stop loss / take profit params
            decimal? triggerPrice = null,
            decimal? orderPrice = null,

            // Trailing stop params
            decimal? trailValue = null,

            string? subaccountName = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "market", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new TriggerOrderTypeConverter(false)) },
                { "size", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("triggerPrice", triggerPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("orderPrice", orderPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("trailValue", trailValue?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("reduceOnly", reduceOnly);
            parameters.AddOptionalParameter("retryUntilFilled", retryUntilFilled);
            parameters.AddOptionalParameter("externalReferralProgram", _baseClient.ClientOptions.AffiliateCode);

            return await _baseClient.SendFTXRequest<FTXTriggerOrder>(_baseClient.GetUri("conditional_orders"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXOrder>> ModifyOrderAsync(long orderId, decimal? price = null, decimal? quantity = null, string? clientOrderId = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("clientId", clientOrderId);
            parameters.AddOptionalParameter("externalReferralProgram", _baseClient.ClientOptions.AffiliateCode);

            return await _baseClient.SendFTXRequest<FTXOrder>(_baseClient.GetUri($"orders/{orderId}/modify"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXTriggerOrder>> ModifyTriggerOrderAsync(long orderId, decimal? quantity = null, decimal? triggerPrice = null, decimal? orderPrice = null, decimal? trailingValue = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("triggerPrice", triggerPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("orderPrice", orderPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("trailValue", trailingValue?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("externalReferralProgram", _baseClient.ClientOptions.AffiliateCode);

            return await _baseClient.SendFTXRequest<FTXTriggerOrder>(_baseClient.GetUri($"conditional_orders/{orderId}/modify"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXOrder>> ModifyOrderByClientOrderIdAsync(long clientOrderId, decimal? price = null, decimal? quantity = null, string? newClientOrderId = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("clientId", newClientOrderId);
            parameters.AddOptionalParameter("externalReferralProgram", _baseClient.ClientOptions.AffiliateCode);

            return await _baseClient.SendFTXRequest<FTXOrder>(_baseClient.GetUri($"orders/by_client_id/{clientOrderId}/modify"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXOrder>> GetOrderAsync(long orderId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXOrder>(_baseClient.GetUri("orders/" + orderId), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXOrder>(_baseClient.GetUri("orders/by_client_id/" + clientOrderId), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXTriggerOrderTrigger>>> GetTriggerOrderTriggersAsync(long orderId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXTriggerOrderTrigger>>(_baseClient.GetUri($"conditional_orders/{orderId}/triggers"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXOrder>>> GetOpenOrdersAsync(string? symbol = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXOrder>>(_baseClient.GetUri("orders"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXTriggerOrder>>> GetOpenTriggerOrdersAsync(string? symbol = null, TriggerOrderType? type = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new TriggerOrderTypeConverter(false)));
            return await _baseClient.SendFTXRequest<IEnumerable<FTXTriggerOrder>>(_baseClient.GetUri("conditional_orders"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXOrder>>> GetOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXOrder>>(_baseClient.GetUri("orders/history"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXTriggerOrder>>> GetTriggerOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, Enums.OrderSide? side = null, TriggerOrderType? type = null, Enums.OrderType? orderType = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new TriggerOrderTypeConverter(false)));
            parameters.AddOptionalParameter("orderType", orderType == null ? null : JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)));
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXTriggerOrder>>(_baseClient.GetUri("conditional_orders/history"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<string>> CancelOrderAsync(long orderId, string? subaccountName = null, CancellationToken ct = default)
        {
            var result = await _baseClient.SendFTXRequest<string>(_baseClient.GetUri("orders/" + orderId), HttpMethod.Delete, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderCanceled(new OrderId { SourceObject = result.Data, Id = orderId.ToString(CultureInfo.InvariantCulture) });
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<string>> CancelTriggerOrderAsync(long orderId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<string>(_baseClient.GetUri("conditional_orders/" + orderId), HttpMethod.Delete, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<string>> CancelOrderByClientIdAsync(string clientOrderId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<string>(_baseClient.GetUri("orders/by_client_id/" + clientOrderId), HttpMethod.Delete, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<string>> CancelAllOrdersAsync(string? symbol = null, Enums.OrderSide? side = null, bool? conditionalOrdersOnly = null, bool? limitOrdersOnly = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("conditionalOrdersOnly", conditionalOrdersOnly);
            parameters.AddOptionalParameter("limitOrdersOnly", limitOrdersOnly);
            return await _baseClient.SendFTXRequest<string>(_baseClient.GetUri("orders"), HttpMethod.Delete, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        #region Fills

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXUserTrade>>> GetUserTradesAsync(string? symbol = null, long? orderId = null, bool? ascendingOrder = null, DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", symbol);
            parameters.AddOptionalParameter("order", ascendingOrder == true ? "asc" : null);
            parameters.AddOptionalParameter("orderId", orderId);
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXUserTrade>>(_baseClient.GetUri("fills"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        #endregion

        #region Leveraged tokens

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXLeveragedTokenCreationRequest>>> GetLeveragedTokenCreationRequestsAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLeveragedTokenCreationRequest>>(_baseClient.GetUri("lt/creations"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXLeveragedTokenCreationRequest>> RequestLeveragedTokenCreationAsync(string tokenName, decimal size, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("size", size.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXLeveragedTokenCreationRequest>(_baseClient.GetUri($"lt/{tokenName}/create"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXLeveragedTokenRedemption>>> GetLeveragedTokenRedemptionRequestsAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXLeveragedTokenRedemption>>(_baseClient.GetUri("lt/redemptions"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXLeveragedTokenRedeemRequest>> RequestLeveragedTokenRedemptionAsync(string tokenName, decimal size, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("size", size.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXLeveragedTokenRedeemRequest>(_baseClient.GetUri($"lt/{tokenName}/redeem"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        #endregion

        #region Options

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXUserQuoteRequest>>> GetOptionsUserQuoteRequestsAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXUserQuoteRequest>>(_baseClient.GetUri("options/my_requests"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXQuoteRequest>> CreateOptionsQuoteRequestAsync(string underlying, OptionType type, decimal strike, DateTime expiry, Enums.OrderSide side, decimal size, decimal? limitPrice = null, bool? hideLimitPrice = null, DateTime? requestExpiry = null, long? counterPartyId = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("underlying", underlying);
            parameters.AddParameter("type", JsonConvert.SerializeObject(type, new OptionTypeConverter(false)));
            parameters.AddParameter("strike", strike.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("expiry", DateTimeConverter.ConvertToMilliseconds(expiry)!);
            parameters.AddParameter("side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddParameter("size", size.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limitPrice", limitPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("hideLimitPrice", hideLimitPrice);
            parameters.AddOptionalParameter("requestExpiry", DateTimeConverter.ConvertToMilliseconds(requestExpiry));
            parameters.AddOptionalParameter("counterPartyId", counterPartyId);
            return await _baseClient.SendFTXRequest<FTXQuoteRequest>(_baseClient.GetUri("options/requests"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXUserQuoteRequest>> CancelOptionsQuoteRequestAsync(long requestId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXUserQuoteRequest>(_baseClient.GetUri("options/requests/" + requestId), HttpMethod.Delete, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXQuoteRequestQuote>>> GetOptionsQuotesForQuoteRequestAsync(long requestId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXQuoteRequestQuote>>(_baseClient.GetUri($"options/requests/{requestId}/quotes"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXUserQuoteRequest>> CreateOptionsQuoteAsync(long requestId, decimal price, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("price", price.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXUserQuoteRequest>(_baseClient.GetUri($"options/requests/{requestId}/quotes"), HttpMethod.Post, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXQuoteRequestQuote>>> GetOptionsUserQuotesAsync(string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXQuoteRequestQuote>>(_baseClient.GetUri("options/my_quotes"), HttpMethod.Get, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXQuoteRequestQuote>> CancelOptionsQuoteAsync(long quoteId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXQuoteRequestQuote>(_baseClient.GetUri("options/quotes/" + quoteId), HttpMethod.Delete, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXQuoteRequestQuote>> AcceptOptionsQuoteAsync(long quoteId, string? subaccountName = null, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXQuoteRequestQuote>(_baseClient.GetUri($"options/quotes/{quoteId}/accept"), HttpMethod.Post, ct, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXUserOptionTrade>>> GetOptionsUserTradesAsync(DateTime? startTime = null, DateTime? endTime = null, string? subaccountName = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXUserOptionTrade>>(_baseClient.GetUri("options/fills"), HttpMethod.Get, ct, parameters, signed: true, additionalHeaders: FTXClient.GetSubaccountHeader(subaccountName)).ConfigureAwait(false);
        }
        #endregion
    }
}
