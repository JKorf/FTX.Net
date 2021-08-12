using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Spot
{
    /// <summary>
    /// User trade info
    /// </summary>
    public class FTXUserTrade
    {
        /// <summary>
        /// Fill id
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
        /// Fee currency
        /// </summary>
        public string FeeCurrency { get; set; } = string.Empty;
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
        /// Base currency
        /// </summary>
        public string? BaseCurrency { get; set; }
        /// <summary>
        /// Quote currency
        /// </summary>
        public string? QuoteCurrency { get; set; }
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
        public DateTime Time { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}
