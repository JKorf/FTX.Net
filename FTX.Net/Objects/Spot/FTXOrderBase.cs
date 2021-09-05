using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Spot
{
    /// <summary>
    /// Base class for order/trigger order
    /// </summary>
    public abstract class FTXOrderBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// When the order was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The future the order is for
        /// </summary>
        public string Future { get; set; } = string.Empty;
        /// <summary>
        /// The symbol to order is for
        /// </summary>
        [JsonProperty("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The price of the order
        /// </summary>
        public abstract decimal? OrderPrice { get; set; }
        /// <summary>
        /// The order type
        /// </summary>
        public abstract OrderType OrderType { get; set; }
        /// <summary>
        /// The side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }

        /// <summary>
        /// The total quantity of the order
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Reduce only order
        /// </summary>
        public bool ReduceOnly { get; set; }

        /// <summary>
        /// Average fill price
        /// </summary>
        [JsonProperty("avgFillPrice")]
        public decimal? AverageFillPrice { get; set; }

        /// <summary>
        /// Filled quantity
        /// </summary>
        [JsonProperty("filledSize")]
        public decimal? FilledQuantity { get; set; }
    }
}
