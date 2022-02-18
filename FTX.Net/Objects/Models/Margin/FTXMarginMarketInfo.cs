using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.Margin
{
    /// <summary>
    /// Margin market info
    /// </summary>
    public class FTXMarginMarketInfo
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity of asset currently borrowed
        /// </summary>
        public decimal Borrowed { get; set; }
        /// <summary>
        /// Quantity of asset that can be spent buying the other asset in the supplied market, including what's borrowable with margin
        /// </summary>
        public decimal Free { get; set; }
        /// <summary>
        /// Estimated hourly borrow rate for the next spot margin cycle
        /// </summary>
        public decimal? EstimatedRate { get; set; }
        /// <summary>
        /// Hourly borrow rate in the previous spot margin cycle
        /// </summary>
        public decimal? PreviousRate { get; set; }
    }
}
