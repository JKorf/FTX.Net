using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.NFT
{
    /// <summary>
    /// NFT trade info
    /// </summary>
    public class FTXNftTradeAll: FTXNftTrade
    {
        /// <summary>
        /// NFT details
        /// </summary>
        public FTXNft Nft { get; set; } = default!;
    }
}
