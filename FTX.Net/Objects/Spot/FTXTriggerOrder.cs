using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;


namespace FTX.Net.Objects.Spot
{
    /// <summary>
    /// Trigger order info
    /// </summary>
    public class FTXTriggerOrder: FTXOrderBase
    {
        /// <summary>
        /// The order type
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        [JsonProperty("orderType")]
        public override OrderType Type { get; set; }
        /// <summary>
        /// The order type
        /// </summary>
        [JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus OrderStatus { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonProperty("orderPrice")]
        public override decimal? Price { get; set; }

        /// <summary>
        /// Id of the order
        /// </summary>
        public long? OrderId { get; set; }
        /// <summary>
        /// The trigger price
        /// </summary>
        public decimal? TriggerPrice { get; set; }
        /// <summary>
        /// The trigger type
        /// </summary>
        [JsonConverter(typeof(TriggerOrderTypeConverter))]
        [JsonProperty("type")]
        public TriggerOrderType TriggerType { get; set; }
        /// <summary>
        /// Status of the trigger order
        /// </summary>
        [JsonConverter(typeof(TriggerOrderStatusConverter))]
        public TriggerOrderStatus Status { get; set; }
        /// <summary>
        /// Error message for order placing
        /// </summary>
        public string? Error { get; set; }
        /// <summary>
        /// Time at which the order was triggered
        /// </summary>
        [JsonProperty("triggeredAt")]
        public DateTime? TriggerTime { get; set; }
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
        /// Time at which the order was canceled
        /// </summary>
        [JsonProperty("cancelledAt")]
        public DateTime? CancelTime { get; set; }
        /// <summary>
        /// Cancelation reason
        /// </summary>
        public string? CancelReason { get; set; }
    }
}
