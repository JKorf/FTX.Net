using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.FTXPay.Socket
{
    /// <summary>
    /// Payment update
    /// </summary>
    public class FTXStreamFTXPayment
    {
        /// <summary>
        /// Payment info
        /// </summary>
        public FTXAppPayment Payment { get; set; } = default!;
        /// <summary>
        /// App details
        /// </summary>
        public FTXAppDetails App { get; set; } = default!;
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}
