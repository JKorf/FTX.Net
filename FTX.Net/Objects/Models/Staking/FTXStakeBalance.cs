using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.Staking
{
    /// <summary>
    /// Stake balance
    /// </summary>
    public class FTXStakeBalance
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Total rewards
        /// </summary>
        public decimal LifetimeRewards { get; set; }
        /// <summary>
        /// Quantity scheduled to unstake
        /// </summary>
        public decimal ScheduledToUnstake { get; set; }
        /// <summary>
        /// Quantity staked
        /// </summary>
        public decimal Staked { get; set; }
    }
}
