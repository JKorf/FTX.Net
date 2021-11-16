using System;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.Margin
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
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
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
        /// Quantity of asset you paid or got paid as interest on the borrow
        /// </summary>
        public decimal Cost { get; set; }
    }
}
