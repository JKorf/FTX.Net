namespace FTX.Net.Objects.Models.LeveragedTokens
{
    /// <summary>
    /// Balance info
    /// </summary>
    public class FTXLeveragedTokenBalance
    {
        /// <summary>
        /// Token name
        /// </summary>
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// Balance
        /// </summary>
        public decimal Balance { get; set; }
    }
}
