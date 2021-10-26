using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;

namespace FTX.Net.Objects.Spot
{
    /// <summary>
    /// Future info
    /// </summary>
    public class FTXFuture: FTXSymbolBase
    {
        /// <summary>
        /// Volume
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Expired
        /// </summary>
        public bool Expired { get; set; }
        /// <summary>
        /// Timestamp it expires
        /// </summary>
        [JsonProperty("expiry")]
        public DateTime? ExpiryTime { get; set; }
        /// <summary>
        /// Index
        /// </summary>
        public decimal Index { get; set; }
        /// <summary>
        /// IMF factor
        /// </summary>
        public decimal ImfFactor { get; set; }
        /// <summary>
        /// The lowest price the future can trade at
        /// </summary>
        public decimal LowerBound { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        public decimal Mark { get; set; }
        /// <summary>
        /// Open interest (in number of contracts)
        /// </summary>
        public decimal OpenInterest { get; set; }
        /// <summary>
        /// Open interest (in USD)
        /// </summary>
        public decimal OpenInterestUsd { get; set; }
        /// <summary>
        /// Whether or not this is a perpetual contract
        /// </summary>
        public bool Perpetual { get; set; }
        /// <summary>
        /// Position weight limit
        /// </summary>
        public decimal PositionLimitWeight { get; set; }
        /// <summary>
        /// The highest price the future can trade at
        /// </summary>
        public decimal UpperBound { get; set; }
        /// <summary>
        /// Future type
        /// </summary>
        [JsonConverter(typeof(FutureTypeConverter))]
        public FutureType Type { get; set; }

        /// <summary>
        /// Expiry description
        /// </summary>
        public string? ExpiryDescription { get; set; }
        /// <summary>
        /// Group
        /// </summary>
        public string? Group { get; set; }
        /// <summary>
        /// Margin price
        /// </summary>
        public decimal? MarginPrice { get; set; }
        /// <summary>
        /// Underlying description
        /// </summary>
        public string? UnderlyingDescription { get; set; }
    }
}
