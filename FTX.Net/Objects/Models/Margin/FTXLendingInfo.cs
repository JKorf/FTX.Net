using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Margin
{
    /// <summary>
    /// Lending info
    /// </summary>
    public class FTXLendingInfo
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Additional quantity you can lend
        /// </summary>
        public decimal Lendable { get; set; }
        /// <summary>
        /// Quantity either in lending offers or not yet unlocked from lending offers
        /// </summary>
        public decimal Locked { get; set; }
        /// <summary>
        /// Minimum rate at which your offers will lend
        /// </summary>
        public decimal? MinRate { get; set; }
        /// <summary>
        /// Quantity in your lending offers
        /// </summary>
        public decimal Offered { get; set; }
    }
}
