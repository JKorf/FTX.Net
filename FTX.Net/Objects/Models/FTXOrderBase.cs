using System;
using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models
{
    /// <summary>
    /// Base class for order/trigger order
    /// </summary>
    public abstract class FTXOrderBase
    {
        /// <summary>
        /// Order id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// When the order was created
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime CreateTime { get; set; }

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
        public abstract decimal? Price { get; set; }
        /// <summary>
        /// The order type
        /// </summary>
        public abstract OrderType Type { get; set; }
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
        public decimal? QuantityFilled { get; set; }
    }
}
