using System;
using System.Collections.Generic;

namespace FTX.Net.Objects.Models.NFT
{
    /// <summary>
    /// Gallery info
    /// </summary>
    public class FTXNftGallery
    {
        /// <summary>
        /// Gallery name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Nfts
        /// </summary>
        public IEnumerable<FTXNft> Nfts { get; set; } = Array.Empty<FTXNft>();
    }
}
