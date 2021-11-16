using System;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.LeveragedTokens
{
    /// <summary>
    /// Leveraged token creation request
    /// </summary>
    public class FTXLeveragedTokenCreationRequest
    {
        /// <summary>
        /// Id of the request
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Token name
        /// </summary>
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// Number of tokens originally requested
        /// </summary>
        public int RequestedSize { get; set; }
        /// <summary>
        /// Is pending
        /// </summary>
        public bool Pending { get; set; }
        /// <summary>
        /// Number of tokens created; may be less than the requested number
        /// </summary>
        public int? CreatedSize { get; set; }
        /// <summary>
        /// Price at which the creation request was fulfilled
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// Cost of creating the tokens, not including fees
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// Fee for creating the tokens
        /// </summary>
        public decimal? Fee { get; set; }
        /// <summary>
        /// Time the request was submitted
        /// </summary>
        [JsonProperty("requestedAt")]
        public DateTime RequestTime { get; set; }
        /// <summary>
        /// Time the request was processed
        /// </summary>
        [JsonProperty("fulfilledAt")]
        public DateTime? FullFillTime { get; set; }
    }
}
