﻿using Newtonsoft.Json;

namespace FTX.Net.Objects.Models
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
        /// Fee that will be charged on the withdrawal (quantity - fee will be sent to the destination)
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// If this blockchain is currently congested
        /// </summary>
        public bool Congested { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public string? Address { get; set; }
    }
}
