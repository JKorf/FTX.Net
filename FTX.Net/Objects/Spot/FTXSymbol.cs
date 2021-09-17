using CryptoExchange.Net.ExchangeInterfaces;
using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;

namespace FTX.Net.Objects
{
    /// <summary>
    /// Symbol information
    /// </summary>
    public class FTXSymbol: ICommonSymbol
    {
        /// <summary>
        /// The name of the symbol
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The base currency
        /// </summary>
        public string? BaseCurrency { get; set; }
        /// <summary>
        /// The quote currency
        /// </summary>
        public string? QuoteCurrency { get; set; }
        /// <summary>
        /// The volume in quote
        /// </summary>
        public decimal QuoteVolume24H { get; set; }
        /// <summary>
        /// The volume in quote
        /// </summary>
        [JsonProperty("volumeUsd24h")]
        public decimal USDVolume24H { get; set; }
        /// <summary>
        /// The type of symbol
        /// </summary>
        [JsonConverter(typeof(SymbolTypeConverter))]
        public SymbolType Type { get; set; }
        /// <summary>
        /// The underlying asset
        /// </summary>
        public string Underlying { get; set; } = string.Empty;
        /// <summary>
        /// Whether the symbol is enabled or not
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// Best current ask price
        /// </summary>
        [JsonProperty("ask")]
        public decimal BestAsk { get; set; }
        /// <summary>
        /// Best current bid price
        /// </summary>
        [JsonProperty("bid")]
        public decimal BestBid { get; set; }
        /// <summary>
        /// Last trade price
        /// </summary>
        [JsonProperty("last")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// If the market is in post-only mode
        /// </summary>
        public bool PostOnly { get; set; }
        /// <summary>
        /// Price step
        /// </summary>
        [JsonProperty("priceIncrement")]
        public decimal PriceStep { get; set; }
        /// <summary>
        /// Minimum maker order size (if >10 orders per hour fall below this size)
        /// </summary>
        public decimal MinProvideSize { get; set; }
        /// <summary>
        /// Quantity step
        /// </summary>
        [JsonProperty("sizeIncrement")]
        public decimal QuantityStep { get; set; }
        /// <summary>
        /// If the market has nonstandard restrictions on which jurisdictions can trade it
        /// </summary>
        public bool Restricted { get; set; }
        /// <summary>
        /// High leverage fee exempt
        /// </summary>
        public bool HighLeverageFeeExempt { get; set; }

        /// <summary>
        /// Change in the price of the token over the past hour
        /// </summary>
        [JsonProperty("change1h")]
        public decimal Change1Hour { get; set; }
        /// <summary>
        /// Change in the price of the token over the past day
        /// </summary>
        [JsonProperty("change24h")]
        public decimal Change24Hour { get; set; }
        /// <summary>
        /// Change in price since 00:00 UTC
        /// </summary>
        [JsonProperty("changeBod")]
        public decimal ChangeBeginingOfDay { get; set; }


        string ICommonSymbol.CommonName => Name;

        decimal ICommonSymbol.CommonMinimumTradeSize => MinProvideSize;
    }
}
