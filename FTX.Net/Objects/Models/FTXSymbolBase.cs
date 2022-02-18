using Newtonsoft.Json;

namespace FTX.Net.Objects.Models
{
    /// <summary>
    /// Base symbol info
    /// </summary>
    public abstract class FTXSymbolBase
    {
        /// <summary>
        /// The name of the symbol
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Change in the price of the token over the past hour
        /// </summary>
        [JsonProperty("change1h")]
        public decimal Change1Hour { get; set; }
        /// <summary>
        /// Change in the price of the token over the past day
        /// </summary>
        [JsonProperty("change24h")]
        public decimal Change24Hour { get; set; }
        /// <summary>
        /// Change in price since 00:00 UTC
        /// </summary>
        [JsonProperty("changeBod")]
        public decimal ChangeBeginingOfDay { get; set; }
        /// <summary>
        /// The volume in quote
        /// </summary>
        [JsonProperty("volumeUsd24h")]
        public decimal USDVolume24H { get; set; }
        /// <summary>
        /// The underlying asset
        /// </summary>
        public string Underlying { get; set; } = string.Empty;
        /// <summary>
        /// Whether the symbol is enabled or not
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// Best current ask price
        /// </summary>
        [JsonProperty("ask")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// Best current bid price
        /// </summary>
        [JsonProperty("bid")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// Last trade price
        /// </summary>
        [JsonProperty("last")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// If the market is in post-only mode
        /// </summary>
        public bool PostOnly { get; set; }
        /// <summary>
        /// Price step
        /// </summary>
        [JsonProperty("priceIncrement")]
        public decimal PriceStep { get; set; }
        /// <summary>
        /// Quantity step
        /// </summary>
        [JsonProperty("sizeIncrement")]
        public decimal QuantityStep { get; set; }
    }
}
