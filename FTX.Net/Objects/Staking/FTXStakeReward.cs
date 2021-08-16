using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Staking
{
    /// <summary>
    /// Stake reward info
    /// </summary>
    public class FTXStakeReward
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Reward quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Time { get; set; }
    }
}
