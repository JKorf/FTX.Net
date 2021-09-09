using Newtonsoft.Json;

namespace FTX.Net.Objects.SocketObjects
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
        public decimal? BestAsk { get; set; }
        /// <summary>
        /// Best bid quantity
        /// </summary>
        [JsonProperty("bidSize")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonProperty("bid")]
        public decimal? BestBid { get; set; }
        /// <summary>
        /// Best ask quantity
        /// </summary>
        [JsonProperty("askSize")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// Last trade price
        /// </summary>
        [JsonProperty("last")]
        public decimal? LastTrade { get; set; }
    }
}
