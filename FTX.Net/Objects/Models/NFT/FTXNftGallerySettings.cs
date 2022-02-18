namespace FTX.Net.Objects.Models.NFT
{
    /// <summary>
    /// Settings
    /// </summary>
    public class FTXNftGallerySettings
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Is public
        /// </summary>
        public bool? Public { get; set; }
        /// <summary>
        /// Donations public
        /// </summary>
        public bool? DonationsPublic { get; set; }
    }
}
