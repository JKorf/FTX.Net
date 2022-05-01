namespace FTX.Net.Enums
{
    /// <summary>
    /// Kline intervals, int value represents the time in seconds
    /// </summary>
    public enum KlineInterval
    {
        /// <summary>
        /// 15s
        /// </summary>
        FifteenSeconds = 15,
        /// <summary>
        /// 1m
        /// </summary>
        OneMinute = 60,
        /// <summary>
        /// 5m
        /// </summary>
        FiveMinutes = 60 * 5,
        /// <summary>
        /// 15m
        /// </summary>
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// 1h
        /// </summary>
        OneHour = 60 * 60,
        /// <summary>
        /// 4h
        /// </summary>
        FourHours = 60 * 60 * 4,
        /// <summary>
        /// 1d
        /// </summary>
        OneDay = 60 * 60 * 24,
        /// <summary>
        /// 1w
        /// </summary>
        OneWeek = 60 * 60 * 24 * 7,
        /// <summary>
        /// 1M
        /// </summary>
        OneMonth = 60 * 60 * 24 * 30
    }
}
