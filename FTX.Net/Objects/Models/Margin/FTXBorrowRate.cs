using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Margin
{
    /// <summary>
    /// Borrow rate
    /// </summary>
    public class FTXBorrowRate
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Estimate
        /// </summary>
        public decimal Estimate { get; set; }
        /// <summary>
        /// Previous
        /// </summary>
        public decimal Previous { get; set; }
    }
}
