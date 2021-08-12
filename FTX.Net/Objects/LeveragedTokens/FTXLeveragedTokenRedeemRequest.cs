using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.LeveragedTokens
{
    /// <summary>
    /// Redeem request result
    /// </summary>
    public class FTXLeveragedTokenRedeemRequest
    {
        /// <summary>
        /// Redeem request id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Token name
        /// </summary>
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// Number of tokens requsted to be redeemed
        /// </summary>
        public decimal Size { get; set; }
        /// <summary>
        /// Estimated proceeds from the redemption
        /// </summary>
        public decimal ProjectedProceeds { get; set; }
        /// <summary>
        /// Is pending
        /// </summary>
        public bool Pending { get; set; }
        /// <summary>
        /// Time the request was submitted
        /// </summary>
        public DateTime RequestedAt { get; set; }
    }
}
