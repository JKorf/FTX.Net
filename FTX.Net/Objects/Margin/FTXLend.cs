using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Margin
{
    /// <summary>
    /// Lending info
    /// </summary>
    public class FTXLend
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
    }

    /// <summary>
    /// User lend info
    /// </summary>
    public class FTXUserLend: FTXLend
    {
        /// <summary>
        /// Amount of coin you paid or got paid as interest on the borrow
        /// </summary>
        public decimal Cost { get; set; }
    }
}
