using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Options
{
    /// <summary>
    /// Quote request
    /// </summary>
    public class FTXQuoteRequest
    {
        /// <summary>
        /// Id of the option
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The option
        /// </summary>
        public FTXOption Option { get; set; } = default!;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Side 
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(QuoteRequestStatusConverter))]
        public QuoteRequestStatus Status { get; set; }
        /// <summary>
        /// When the request expires
        /// </summary>
        [JsonProperty("requestExpiry")]
        public DateTime RequestExpiryTime { get; set; }
        /// <summary>
        /// When the request expires
        /// </summary>
        [JsonProperty("expiry")]
        public DateTime? ExpiryTime { get; set; }
        /// <summary>
        /// Limit price
        /// </summary>
        public decimal? LimitPrice { get; set; }
        /// <summary>
        /// Limit price
        /// </summary>
        public decimal? Strike { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [JsonConverter(typeof(OptionTypeConverter))]
        public OptionType Type { get; set; }
        /// <summary>
        /// Underlying type
        /// </summary>
        public string? Underlying { get; set; }
    }
}
