using System;

namespace FTX.Net.Objects.Models
{
    /// <summary>
    /// Future stat
    /// </summary>
    public class FTXFutureStat
    {
        /// <summary>
        /// Volume in last 24H
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// Upcoming funding rate
        /// </summary>
        public decimal? NextFundingRate { get; set; }
        /// <summary>
        /// Upcoming funding time
        /// </summary>
        public DateTime? NextFundingTime { get; set; }
        /// <summary>
        /// Price to which the future expired 
        /// </summary>
        public decimal? ExpirationPrice { get; set; }
        /// <summary>
        /// Predited expiration price
        /// </summary>
        public decimal? PredictedExpirationPrice { get; set; }
        /// <summary>
        /// Price of the underlying at the beginning of the expiration day
        /// </summary>
        public decimal? StrikePrice { get; set; }
        /// <summary>
        /// Number of open contracts in this future
        /// </summary>
        public decimal OpenInterest { get; set; }
    }
}
