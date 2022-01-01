using System;
using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models
{
    /// <summary>
    /// User trade info
    /// </summary>
    public class FTXUserTrade
    {
        /// <summary>
        /// Trade id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Fee paid
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee rate
        /// </summary>
        public decimal FeeRate { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonProperty("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Future
        /// </summary>
        public string Future { get; set; } = string.Empty;
        /// <summary>
        /// Liquidity
        /// </summary>
        [JsonConverter(typeof(LiquidityTypeConverter))]
        public LiquidityType Liquidity { get; set; }
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonProperty("baseCurrency")]
        public string? BaseAsset { get; set; }
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonProperty("quoteCurrency")]
        public string? QuoteAsset { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        public long TradeId { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}
