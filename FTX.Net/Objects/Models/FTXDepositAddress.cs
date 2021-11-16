namespace FTX.Net.Objects.Models
{
    /// <summary>
    /// Deposit address
    /// </summary>
    public class FTXDepositAddress
    {
        /// <summary>
        /// Deposit address
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Tag
        /// </summary>
        public string? Tag { get; set; }
    }
}
