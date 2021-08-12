using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.FTXPay
{
    /// <summary>
    /// Payment info
    /// </summary>
    public class FTXAppPayment
    {
        /// <summary>
        /// Id of the payment
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Fee amount
        /// </summary>
        [JsonProperty("feeSize")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Received amount
        /// </summary>
        [JsonProperty("netSize")]
        public decimal NetQuantity { get; set; }
        /// <summary>
        /// Tipped amount
        /// </summary>
        [JsonProperty("tipSize")]
        public decimal TipQuantity { get; set; }
        /// <summary>
        /// Asset received
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Creation time
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Payer-provided info for payment identification
        /// </summary>
        public string? Memo { get; set; }
        /// <summary>
        /// Payer-provided notes
        /// </summary>
        public string? Notes { get; set; }
        /// <summary>
        /// Email of the sender
        /// </summary>
        public string SenderEmail { get; set; } = string.Empty;
    }
}
