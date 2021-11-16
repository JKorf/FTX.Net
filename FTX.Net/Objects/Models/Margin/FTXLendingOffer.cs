using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Margin
{
    /// <summary>
    /// Lending offer
    /// </summary>
    public class FTXLendingOffer
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Hourly rate at which you will lend, if matched
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// Quantity you will lend, if matched
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
    }
}
