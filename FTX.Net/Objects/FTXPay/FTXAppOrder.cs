using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.FTXPay
{
    /// <summary>
    /// App order
    /// </summary>
    public class FTXAppOrder
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The currency of the payment
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Notes about this order that are private to the merchant
        /// </summary>
        public string? Notes { get; set; }
        /// <summary>
        /// Size of the desired payment
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Whether or not tips are allowed for the payment
        /// </summary>
        public bool AllowTips { get; set; }
        /// <summary>
        /// ID for you to track the order with (must be unique to your FTX Pay app)
        /// </summary>
        public string? ClientId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(FTXPayOrderStatusConverter))]
        public FTXPayOrderStatus Status { get; set; }
        /// <summary>
        /// Payment
        /// </summary>
        public FTXAppPayment? Payment { get; set; }
    }
}
