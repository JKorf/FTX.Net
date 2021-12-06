using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;

namespace FTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Symbol update
    /// </summary>
    public class FTXStreamSymbol
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Enabled
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// Price increment
        /// </summary>
        public decimal PriceIncrement { get; set; }
        /// <summary>
        /// Quantity increment
        /// </summary>
        [JsonProperty("sizeIncrement")]
        public decimal QuantityIncrement { get; set; }
        /// <summary>
        /// Symbol type
        /// </summary>
        [JsonConverter(typeof(SymbolTypeConverter))]
        public SymbolType Type { get; set; }
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonProperty("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonProperty("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Underlying
        /// </summary>
        public string Underlying { get; set; } = string.Empty;
        /// <summary>
        /// Restricted
        /// </summary>
        public bool Restricted { get; set; }
        /// <summary>
        /// Future
        /// </summary>
        public FTXStreamFuture? Future { get; set; }
    }

    /// <summary>
    /// Future info
    /// </summary>
    public class FTXStreamFuture
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Underlying
        /// </summary>
        public string Underlying { get; set; } = string.Empty;
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Future type
        /// </summary>
        [JsonConverter(typeof(FutureTypeConverter))]
        public FutureType Type { get; set; }
        /// <summary>
        /// Expiry date
        /// </summary>
        public DateTime? Expiry { get; set; }
        /// <summary>
        /// Is perpetual
        /// </summary>
        public bool Perpetual { get; set; }
        /// <summary>
        /// Has expired
        /// </summary>
        public bool Expired { get; set; }
        /// <summary>
        /// Is enabled
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// Is post only
        /// </summary>
        public bool PostOnly { get; set; }
        /// <summary>
        /// IMF factor
        /// </summary>
        public decimal ImfFactor { get; set; }
        /// <summary>
        /// Underlying description
        /// </summary>
        public string UnderlyingDescription { get; set; } = string.Empty;
        /// <summary>
        /// Expiry description
        /// </summary>
        public string ExpiryDescription { get; set; } = string.Empty;
        /// <summary>
        /// Move start
        /// </summary>
        public DateTime? MoveStart { get; set; }
        /// <summary>
        /// Position limit weight
        /// </summary>
        public decimal PositionLimitWeight { get; set; }
        /// <summary>
        /// Group
        /// </summary>
        public string Group { get; set; } = string.Empty;
    }
}
