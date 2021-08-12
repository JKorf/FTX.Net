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
        /// Size
        /// </summary>
        public decimal Size { get; set; }
        /// <summary>
        /// Side 
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(QuoteRequestStatusConverter))]
        public QuoteRequestStatus Status { get; set; }
        /// <summary>
        /// When the request expires
        /// </summary>
        public DateTime RequestExpiry { get; set; }
        /// <summary>
        /// Limit price
        /// </summary>
        public decimal? LimitPrice { get; set; }
    }
}
