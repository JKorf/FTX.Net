using System;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.Staking
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
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
    }
}
