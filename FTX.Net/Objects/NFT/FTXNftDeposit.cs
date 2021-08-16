using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.NFT
{
    /// <summary>
    /// Deposit info
    /// </summary>
    public class FTXNftDeposit
    {
        /// <summary>
        /// Deposit id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// NFT info
        /// </summary>
        public FTXNft Nft { get; set; } = default!;
        /// <summary>
        /// Deposit status
        /// </summary>
        [JsonConverter(typeof(NFTDepositStatusConverter))]
        public NFTDepositStatus Status { get; set; }
        /// <summary>
        /// NFT Creation time
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// Sent time
        /// </summary>
        public DateTime SentTime { get; set; }
        /// <summary>
        /// Confirmed time
        /// </summary>
        public DateTime ConfirmedTime { get; set; }
        /// <summary>
        /// Confirmation count
        /// </summary>
        public int Confirmations { get; set; }
    }
}
