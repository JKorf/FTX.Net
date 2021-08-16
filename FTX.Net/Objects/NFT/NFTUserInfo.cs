using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.NFT
{
    /// <summary>
    /// User info for NFT
    /// </summary>
    public class NFTUserInfo
    {
        /// <summary>
        /// Current bid on the NFT
        /// </summary>
        public decimal? Bid { get; set; }
        /// <summary>
        /// Buy fee
        /// </summary>
        public decimal? BuyFee { get; set; }
        /// <summary>
        /// Do you have the best bid
        /// </summary>
        public bool IsBestBid { get; set; }
        /// <summary>
        /// Do you own the NFT
        /// </summary>
        public bool Owned { get; set; }
    }
}
