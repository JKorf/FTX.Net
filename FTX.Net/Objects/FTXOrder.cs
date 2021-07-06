using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;

namespace FTX.Net.Objects
{
    /// <summary>
    /// Order info
    /// </summary>
    public class FTXOrder
    {
        /// <summary>
        /// When the order was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The quantity that is filled
        /// </summary>
        [JsonProperty("filledSize")]
        public decimal FilledQuantity { get; set; }
        /// <summary>
        /// The future the order is for
        /// </summary>
        public string Future { get; set; }
        /// <summary>
        /// The market to order is for
        /// </summary>
        public string Market { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The remaining quantity
        /// </summary>
        public decimal RemainingQuantity { get; set; }
        /// <summary>
        /// The side
        /// </summary>
        public OrderSide Side { get; set; }
        /// <summary>
        /// The order type
        /// </summary>
        public OrderType Type { get; set; }
        /// <summary>
        /// The total quantity of the order
        /// </summary>
        public decimal Quantity { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        [JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Reduce only order
        /// </summary>
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// Immediate or cancel order
        /// </summary>
        [JsonProperty("ioc")]
        public bool ImmediateOrCancel { get; set; }
        /// <summary>
        /// Post only order
        /// </summary>
        public bool PostOnly { get; set; }
        /// <summary>
        /// Client id
        /// </summary>
        public string? ClientId { get; set; }
    }
}
