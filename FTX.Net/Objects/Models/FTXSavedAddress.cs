using System;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models
{
    /// <summary>
    /// Saved address
    /// </summary>
    public class FTXSavedAddress
    {
        /// <summary>
        /// The address
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// The name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Is fiat
        /// </summary>
        public bool Fiat { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Is prime trust
        /// </summary>
        public bool IsPrimeTrust { get; set; }
        /// <summary>
        /// Last used time
        /// </summary>
        [JsonProperty("lastUsedAt")]
        public DateTime LastUseTime { get; set; }
        /// <summary>
        /// Address tag
        /// </summary>
        public string? Tag { get; set; }
        /// <summary>
        /// True if address is currently whitelisted
        /// </summary>
        public bool? Whitelisted { get; set; }
        /// <summary>
        /// Time the address was whitelisted
        /// </summary>
        [JsonProperty("whitelistedAfter")]
        public DateTime? WhitelistedAfterTime { get; set; }
    }
}
