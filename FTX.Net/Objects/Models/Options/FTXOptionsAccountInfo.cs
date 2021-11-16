namespace FTX.Net.Objects.Models.Options
{
    /// <summary>
    /// Account info
    /// </summary>
    public class FTXOptionsAccountInfo
    {
        /// <summary>
        /// Usd balance
        /// </summary>
        public decimal UsdBalance { get; set; }
        /// <summary>
        /// Estimated liquidation price
        /// </summary>
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// If the account is currently liquidating
        /// </summary>
        public bool Liquidated { get; set; }
        /// <summary>
        /// You will be liquidated if your account collateral + options usdBalance drops below this number
        /// </summary>
        public decimal? MaintenanceMarginRequirement { get; set; }
        /// <summary>
        /// Initial margin requirement
        /// </summary>
        public decimal? InitialMarginRequirement { get; set; }
    }
}
