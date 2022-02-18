using Newtonsoft.Json;

namespace FTX.Net.Objects.Models
{
    /// <summary>
    /// Balance info
    /// </summary>
    public class FTXBalance
    {
        /// <summary>
        /// The asset the balance info is for
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity free
        /// </summary>
        [JsonProperty("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// Quantity borrowed with spot margin
        /// </summary>
        public decimal SpotBorrow { get; set; }
        /// <summary>
        /// Total Quantity
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// Approximate total USD value
        /// </summary>
        public decimal UsdValue { get; set; }
        /// <summary>
        /// Quantity available without borrowing
        /// </summary>
        public decimal AvailableWithoutBorrow { get; set; }
    }
}
