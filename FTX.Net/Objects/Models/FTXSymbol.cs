using CryptoExchange.Net.ExchangeInterfaces;
using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models
{
    /// <summary>
    /// Symbol information
    /// </summary>
    public class FTXSymbol: FTXSymbolBase, ICommonSymbol
    {
        /// <summary>
        /// The base asset
        /// </summary>
        [JsonProperty("baseCurrency")]
        public string? BaseAsset { get; set; }
        /// <summary>
        /// The quote asset
        /// </summary>
        [JsonProperty("quoteCurrency")]
        public string? QuoteAsset { get; set; }
        /// <summary>
        /// The volume in quote
        /// </summary>
        public decimal QuoteVolume24H { get; set; }
        /// <summary>
        /// The type of symbol
        /// </summary>
        [JsonConverter(typeof(SymbolTypeConverter))]
        public SymbolType Type { get; set; }
        
        /// <summary>
        /// Minimum maker order quantity (if >10 orders per hour fall below this size)
        /// </summary>
        public decimal MinProvideSize { get; set; }
       
        /// <summary>
        /// If the market has nonstandard restrictions on which jurisdictions can trade it
        /// </summary>
        public bool Restricted { get; set; }
        /// <summary>
        /// High leverage fee exempt
        /// </summary>
        public bool HighLeverageFeeExempt { get; set; }
        /// <summary>
        /// The current price
        /// </summary>
        [JsonProperty("price")]
        public decimal? CurrentPrice { get; set; }

        string ICommonSymbol.CommonName => Name;

        decimal ICommonSymbol.CommonMinimumTradeQuantity => MinProvideSize;
    }
}
