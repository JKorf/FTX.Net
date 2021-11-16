using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Stream tick
    /// </summary>
    public class FTXStreamTicker
    {
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonProperty("ask")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// Best bid quantity
        /// </summary>
        [JsonProperty("bidSize")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonProperty("bid")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// Best ask quantity
        /// </summary>
        [JsonProperty("askSize")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// Last trade price
        /// </summary>
        [JsonProperty("last")]
        public decimal? LastPrice { get; set; }
    }
}
