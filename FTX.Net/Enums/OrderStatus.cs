namespace FTX.Net.Enums
{
    /// <summary>
    /// The status of an order
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// New order, not processed yet
        /// </summary>
        New,
        /// <summary>
        /// Open order
        /// </summary>
        Open,
        /// <summary>
        /// Closed order, either canceled or filled
        /// </summary>
        Closed
    }
}
