namespace FTX.Net.Objects
{
    /// <summary>
    /// Balance info
    /// </summary>
    public class FTXBalance
    {
        /// <summary>
        /// The coin the balance info is for
        /// </summary>
        public string Coin { get; set; } = string.Empty;
        /// <summary>
        /// Amount free
        /// </summary>
        public decimal Free { get; set; }
        /// <summary>
        /// Amount borrowed with spot margin
        /// </summary>
        public decimal SpotBorrow { get; set; }
        /// <summary>
        /// Total amount
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// Approximate total USD value
        /// </summary>
        public decimal UsdValue { get; set; }
        /// <summary>
        /// Amount available without borrowing
        /// </summary>
        public decimal AvailableWithoutBorrow { get; set; }
    }
}
