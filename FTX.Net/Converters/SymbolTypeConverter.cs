using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System.Collections.Generic;

namespace FTX.Net.Converters
{
    internal class SymbolTypeConverter: BaseConverter<SymbolType>
    {
        public SymbolTypeConverter() : this(true) { }
        public SymbolTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<SymbolType, string>> Mapping => new List<KeyValuePair<SymbolType, string>>
        {
            new KeyValuePair<SymbolType, string>(SymbolType.Future, "futures"),
            new KeyValuePair<SymbolType, string>(SymbolType.Spot, "spot"),
        };
    }
}
