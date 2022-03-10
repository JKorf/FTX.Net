using System;
using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models
{
    /// <summary>
    /// Deposit info
    /// </summary>
    public class FTXDeposit
    {
        /// <summary>
        /// Deposit asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Confimations
        /// </summary>
        public int Confirmations { get; set; }
        /// <summary>
        /// Time of confirmation
        /// </summary>
        public DateTime ConfirmationTime { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Sent time
        /// </summary>
        public DateTime SentTime { get; set; }
        /// <summary>
        /// Quantity of the deposit
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Status of the deposit
        /// </summary>
        [JsonConverter(typeof(DepositStatusConverter))]
        public DepositStatus Status { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Confirm time
        /// </summary>
        [JsonProperty("confirmedTime")]
        public DateTime ConfirmTime { get; set; }
        /// <summary>
        /// Protocol used for the blockchain transfer
        /// </summary>
        [JsonProperty("method")]
        public string? Network { get; set; }
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
