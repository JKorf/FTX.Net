using System;
using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    public class FTXOrder: FTXOrderBase
    {
        /// <summary>
        /// The order type
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        [JsonProperty("type")]
        public override OrderType Type { get; set; }

        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonProperty("price")]
        public override decimal? Price { get; set; }

        /// <summary>
        /// The remaining quantity
        /// </summary>
        [JsonProperty("remainingSize")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        [JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }
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
        [JsonProperty("clientId")]
        public string? ClientOrderId { get; set; }
    }
}
