using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.NFT
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
        [JsonProperty("txId")]
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
        public DateTime Time { get; set; }
        /// <summary>
        /// Notes
        /// </summary>
        public string? Notes { get; set; }
    }
}
