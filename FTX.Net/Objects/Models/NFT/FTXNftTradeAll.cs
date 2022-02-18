namespace FTX.Net.Objects.Models.NFT
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
