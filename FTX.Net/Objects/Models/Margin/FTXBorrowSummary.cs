using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.Margin
{
    /// <summary>
    /// Borrow summary
    /// </summary>
    public class FTXBorrowSummary
    {
        /// <summary>
        /// Asset 
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
    }
}
