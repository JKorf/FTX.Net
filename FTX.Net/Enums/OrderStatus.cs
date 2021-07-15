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
        /// Closed order, either cancelled or filled
        /// </summary>
        Closed
    }
}
