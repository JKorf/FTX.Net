using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Spot
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
        /// The asset
        /// </summary>
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
        public DateTime LastUsedAt { get; set; }
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
        public DateTime? WhitelistedAfter { get; set; }
    }
}
