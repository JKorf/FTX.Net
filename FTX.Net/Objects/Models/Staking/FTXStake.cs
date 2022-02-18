using System;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.Staking
{
    /// <summary>
    /// Stake info
    /// </summary>
    public class FTXStake
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Creation time
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Stake id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
    }
}
