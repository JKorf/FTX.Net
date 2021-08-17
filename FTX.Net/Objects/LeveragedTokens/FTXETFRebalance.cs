using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.LeveragedTokens
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
        public DateTime Time { get; set; }
    }
}
