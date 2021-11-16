using System;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.Options
{
    /// <summary>
    /// Option trade
    /// </summary>
    public class FTXOptionTrade
    {
        /// <summary>
        /// Trade id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Quantity of trade
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Price of trade
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Option
        /// </summary>
        public FTXOption Option { get; set; } = default!;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
    }
}
