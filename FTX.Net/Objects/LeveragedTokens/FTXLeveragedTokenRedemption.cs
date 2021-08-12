using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.LeveragedTokens
{
    /// <summary>
    /// Redemption info
    /// </summary>
    public class FTXLeveragedTokenRedemption
    {
        /// <summary>
        /// Redemption id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Token name
        /// </summary>
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// Number of tokens redeemed
        /// </summary>
        public decimal Size { get; set; }
        /// <summary>
        /// Is pending
        /// </summary>
        public bool Pending { get; set; }
        /// <summary>
        /// Price at which the redemption request was fulfilled
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Proceeds from the redemption, before fees
        /// </summary>
        public decimal Proceeds { get; set; }
        /// <summary>
        /// Fee for redeeming the tokens
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// Time the request was submitted
        /// </summary>
        public DateTime RequestedAt { get; set; }
        /// <summary>
        /// Time the request was processed
        /// </summary>
        public DateTime FulFilledAt { get; set; }
    }
}
