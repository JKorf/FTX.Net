using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.Margin
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
