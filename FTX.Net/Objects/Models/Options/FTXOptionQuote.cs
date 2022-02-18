using System;
using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.Options
{
    /// <summary>
    /// Quote info
    /// </summary>
    public class FTXOptionQuote
    {
        /// <summary>
        /// Quote id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(QuoteRequestStatusConverter))]
        public QuoteRequestStatus Status { get; set; }
        /// <summary>
        /// Collateral locked by the quote
        /// </summary>
        public decimal Collateral { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Expiry
        /// </summary>
        [JsonProperty("quoteExpiry")]
        public DateTime? QuoteExpiryTime { get; set; }
        /// <summary>
        /// When the quote was created
        /// </summary>
        [JsonProperty("time")]
        public DateTime CreateTime { get; set; }
    }
}
