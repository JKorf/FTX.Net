using System;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.LeveragedTokens
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
        [JsonProperty("requestedAt")]
        public DateTime RequestTime { get; set; }
    }
}
