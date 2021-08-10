using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Spot
{
    /// <summary>
    /// Airdrop info
    /// </summary>
    public class FTXAirdrop
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(AirdropStatusConverter))]
        public AirdropStatus Status { get; set; }
    }
}
