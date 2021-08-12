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
        public DateTime? QuoteExpiry { get; set; }
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
        /// Size
        /// </summary>
        public decimal Size { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(QuoteRequestStatusConverter))]
        public QuoteRequestStatus Status { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        public DateTime Time { get; set; }
    }
}
