using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Spot
{
    /// <summary>
    /// Trigger info
    /// </summary>
    public class FTXTriggerOrderTrigger
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// Size of the order
        /// </summary>
        [JsonProperty("orderSize")]
        public decimal OrderQuantity { get; set; }
        /// <summary>
        /// Filled order
        /// </summary>
        [JsonProperty("filledSize")]
        public decimal FilledQuantity { get; set; }
        /// <summary>
        /// Order id, null if failed to place
        /// </summary>
        public long? OrderId { get; set; }
        /// <summary>
        /// Error if order failed
        /// </summary>
        public string? Error { get; set; }
    }
}
