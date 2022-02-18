using System;
using System.Collections.Generic;
using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.LeveragedTokens
{
    /// <summary>
    /// ETF rebalance info
    /// </summary>
    public class FTXETFRebalanceEntry
    {
        /// <summary>
        /// List of order sizes in the rebalance
        /// </summary>
        public IEnumerable<decimal> OrderSizeList { get; set; } = Array.Empty<decimal>();
        /// <summary>
        /// "buy" or "sell" depending on whether the rebalance involves buying or selling
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Time of the rebalance
        /// </summary>
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
    }
}
