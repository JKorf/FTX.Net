using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Options
{
    /// <summary>
    /// Quote info for quote request
    /// </summary>
    public class FTXQuoteRequestQuote
    {
        /// <summary>
        /// Collateral locked by the quote
        /// </summary>
        public decimal Collateral { get; set; }
        /// <summary>
        /// Quote id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Option
        /// </summary>
        public FTXOption Option { get; set; } = default!;
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Quote expiry
        /// </summary>
        [JsonProperty("quoteExpiry")]
        public DateTime? QuoteExpiryTime { get; set; }
        /// <summary>
        /// Quoter side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide QuoterSide { get; set; }
        /// <summary>
        /// Request id
        /// </summary>
        public long RequestId { get; set; }
        /// <summary>
        /// Request side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide RequestSide { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(QuoteRequestStatusConverter))]
        public QuoteRequestStatus Status { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("time")]
        public DateTime CreateTime { get; set; }
    }
}
