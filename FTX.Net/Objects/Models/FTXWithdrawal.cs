using System;
using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models
{
    /// <summary>
    /// Withdrawal info
    /// </summary>
    public class FTXWithdrawal
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Address the withdrawal was to
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Tag
        /// </summary>
        public string? Tag { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Quantity of the withdrawal
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Status of the withdrawal
        /// </summary>
        [JsonConverter(typeof(WithdrawalStatusConverter))]
        public WithdrawalStatus Status { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("txid")]
        public string? TransactionId { get; set; }
        /// <summary>
        /// Notes
        /// </summary>
        public string? Notes { get; set; }
    }
}
