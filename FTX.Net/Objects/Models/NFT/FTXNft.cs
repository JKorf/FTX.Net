using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.NFT
{
    /// <summary>
    /// NFT info
    /// </summary>
    public class FTXNft
    {
        /// <summary>
        /// NFT id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// entity issuing NFT
        /// </summary>
        public string Issuer { get; set; } = string.Empty;
        /// <summary>
        /// NFT collection name
        /// </summary>
        public string? Collection { get; set; }
        /// <summary>
        /// NFT series
        /// </summary>
        public string? Series { get; set; }
        /// <summary>
        /// SOL mint address
        /// </summary>
        public string SolMintAddress { get; set; } = string.Empty;
        /// <summary>
        /// Eth contract address
        /// </summary>
        public string? EthContractAddress { get; set; }
        /// <summary>
        /// Url to image
        /// </summary>
        public string? ImageUrl { get; set; }
        /// <summary>
        /// Url to video
        /// </summary>
        public string? VideoUrl { get; set; }
        /// <summary>
        /// Attributes
        /// </summary>
        public string? Attributes { get; set; }
        /// <summary>
        /// True if NFT is redeemable for goods
        /// </summary>
        public bool Redeemable { get; set; }
        /// <summary>
        /// True if NFT is redeemable and has been redeemed
        /// </summary>
        public bool Redeemed { get; set; }
        /// <summary>
        /// Offer price
        /// </summary>
        public decimal? OfferPrice { get; set; }
        /// <summary>
        /// Action info
        /// </summary>
        public FTXNftAuction Auction { get; set; } = default!;
    }
}
