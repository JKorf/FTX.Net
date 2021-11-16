using System;
using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.NFT
{
    /// <summary>
    /// Nft withdrawal
    /// </summary>
    public class FTXNftWithdrawal
    {
        /// <summary>
        /// Withdrawal id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// NFT info
        /// </summary>
        public FTXNft Nft { get; set; } = default!;
        /// <summary>
        /// Withdraw address
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Network used
        /// </summary>
        [JsonProperty("method")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("txid")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Fee
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(NFTWithdrawalStatusConverter))]
        public NFTWithdrawalStatus Status { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Notes
        /// </summary>
        public string? Notes { get; set; }
    }
}
