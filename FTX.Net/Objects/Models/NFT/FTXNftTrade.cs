using System;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.NFT
{
    /// <summary>
    /// NFT trade info
    /// </summary>
    public class FTXNftTrade
    {
        /// <summary>
        /// Trade id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
    }
}
