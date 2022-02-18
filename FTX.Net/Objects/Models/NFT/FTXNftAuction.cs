using System;

namespace FTX.Net.Objects.Models.NFT
{
    /// <summary>
    /// NFT auction
    /// </summary>
    public class FTXNftAuction
    {
        /// <summary>
        /// Current best bid
        /// </summary>
        public decimal? BestBid { get; set; }
        /// <summary>
        /// Minimal overbid
        /// </summary>
        public decimal MinNextBid { get; set; }
        /// <summary>
        /// Auction end time
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Current amount of bids
        /// </summary>
        public int Bids { get; set; }
    }
}
