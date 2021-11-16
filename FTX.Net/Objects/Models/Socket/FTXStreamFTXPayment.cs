using FTX.Net.Objects.Models.FTXPay;

namespace FTX.Net.Objects.Models.Socket
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
