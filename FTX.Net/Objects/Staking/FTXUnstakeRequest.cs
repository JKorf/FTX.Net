using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Staking
{
    /// <summary>
    /// Unstake request
    /// </summary>
    public class FTXUnstakeRequest
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Creation time
        /// </summary>
        public DateTime CreatedAt { get; set; }
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
        /// Request status
        /// </summary>
        [JsonConverter(typeof(UnstakeRequestStatusConverter))]
        public UnstakeRequestStatus Status { get; set; }
        /// <summary>
        /// Unlock at
        /// </summary>
        [JsonProperty("unlockAt")]
        public DateTime UnlockAt { get; set; }
    }
}
