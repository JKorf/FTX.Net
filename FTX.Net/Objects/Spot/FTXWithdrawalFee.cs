using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Spot
{
    /// <summary>
    /// Withdrawal fee info
    /// </summary>
    public class FTXWithdrawalFee
    {
        /// <summary>
        /// Network
        /// </summary>
        [JsonProperty("method")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Fee that will be charged on the withdrawal (size - fee will be sent to the destination)
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// If this blockchain is currently congested
        /// </summary>
        public bool Congested { get; set; }
    }
}
