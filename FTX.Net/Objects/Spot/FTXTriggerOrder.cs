using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


namespace FTX.Net.Objects.Spot
{
    /// <summary>
    /// Trigger order info
    /// </summary>
    public class FTXTriggerOrder
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Id of the order
        /// </summary>
        public long? OrderId { get; set; }
        /// <summary>
        /// When the order was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The future the order is for
        /// </summary>
        public string Future { get; set; } = string.Empty;
        /// <summary>
        /// The market to order is for
        /// </summary>
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// The price of the order
        /// </summary>
        public decimal? OrderPrice { get; set; }
        /// <summary>
        /// The trigger price
        /// </summary>
        public decimal? TriggerPrice { get; set; }
        /// <summary>
        /// The side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// The trigger type
        /// </summary>
        [JsonConverter(typeof(TriggerOrderTypeConverter))]
        public TriggerOrderType Type { get; set; }
        /// <summary>
        /// The total quantity of the order
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Status of the trigger order
        /// </summary>
        [JsonConverter(typeof(TriggerOrderStatusConverter))]
        public TriggerOrderStatus Status { get; set; }
        /// <summary>
        /// Reduce only order
        /// </summary>
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// Error message for orrder placing
        /// </summary>
        public string? Error { get; set; }
        /// <summary>
        /// Time at which the order was triggered
        /// </summary>
        public DateTime? TriggeredAt { get; set; }
        /// <summary>
        /// The order type
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Whether or not to keep re-triggering until filled
        /// </summary>
        public bool RetryUntilFilled { get; set; }
        /// <summary>
        /// Trail start
        /// </summary>
        public decimal? TrailStart { get; set; }
        /// <summary>
        /// Trail start
        /// </summary>
        public decimal? TrailValue { get; set; }
        /// <summary>
        /// Filled quantity
        /// </summary>
        [JsonProperty("filledSize")]
        public decimal? FilledQuantity { get; set; }
        /// <summary>
        /// Average fill price
        /// </summary>
        [JsonProperty("avgFillPrice")]
        public decimal? AverageFillPrice { get; set; }
        /// <summary>
        /// Time at which the order was cancelled
        /// </summary>
        public DateTime? CancelledAt { get; set; }
        /// <summary>
        /// Cancellation reason
        /// </summary>
        public string? CancelReason { get; set; }
    }
}
