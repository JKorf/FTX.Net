using FTX.Net.Enums;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models
{
    /// <summary>
    /// Position info
    /// </summary>
    public class FTXPosition
    {
        /// <summary>
        /// Quantity that was paid to enter this position, equal to quantiy * entry_price. Positive if long, negative if short.
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// Cumulative buy size
        /// </summary>
        [JsonProperty("cumulativeBuySize")]
        public decimal CumulativeBuyQuantity { get; set; }
        /// <summary>
        /// Cumulative sell size
        /// </summary>
        [JsonProperty("cumulativeSellSize")]
        public decimal CumulativeSellQuantity { get; set; }
        /// <summary>
        /// Average cost of this position after pnl was last realized: whenever unrealized pnl gets realized, this field gets set to mark price, unrealizedPnL is set to 0, and realizedPnl changes by the previous value for unrealizedPnl.
        /// </summary>
        public decimal? EntryPrice { get; set; }
        /// <summary>
        /// Estimated liquidation price
        /// </summary>
        public decimal? EstimatedLiquidationPrice { get; set; }
        /// <summary>
        /// Name of the future
        /// </summary>
        public string Future { get; set; } = string.Empty;
        /// <summary>
        /// Minimum margin fraction for opening new positions
        /// </summary>
        public decimal InitialMarginRequirement { get; set; }
        /// <summary>
        /// Cumulative size of all open bids
        /// </summary>
        [JsonProperty("longOrderSize")]
        public decimal LongOrderQuantity { get; set; }
        /// <summary>
        /// Minimum margin fraction to avoid liquidations
        /// </summary>
        public decimal MaintenanceMarginRequirement { get; set; }
        /// <summary>
        /// Size of position. Positive if long, negative if short.
        /// </summary>
        [JsonProperty("netSize")]
        public decimal NetQuantity { get; set; }
        /// <summary>
        /// Maximum possible absolute position size if some subset of open orders are filled
        /// </summary>
        [JsonProperty("openSize")]
        public decimal OpenQuantity { get; set; }
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// Recent average open price
        /// </summary>
        public decimal? RecentAverageOpenPrice { get; set; }
        /// <summary>
        /// Recent break even price
        /// </summary>
        public decimal? RecentBreakEvenPrice { get; set; }
        /// <summary>
        /// Recent profit and loss
        /// </summary>
        public decimal? RecentPnl { get; set; }
        /// <summary>
        /// Cumulative size of all open offers
        /// </summary>
        [JsonProperty("shortOrderSize")]
        public decimal ShortOrderQuantity { get; set; }
        /// <summary>
        /// Side, sell for short, buy for long
        /// </summary>
        public OrderSide Side { get; set; }
        /// <summary>
        /// Absolute value of netSize
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// For PRESIDENT: initialMarginRequirement* openSize * (risk price), 
        /// For MOVE: initialMarginRequirement* openSize * (index price), 
        /// Otherwise: initialMarginRequirement* openSize * (mark price)
        /// </summary>
        public decimal CollateralUsed { get; set; }
    }
}
