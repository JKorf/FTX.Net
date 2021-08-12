using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System.Collections.Generic;

namespace FTX.Net.Converters
{
    internal class OptionTypeConverter : BaseConverter<OptionType>
    {
        public OptionTypeConverter() : this(true) { }
        public OptionTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OptionType, string>> Mapping => new List<KeyValuePair<OptionType, string>>
        {
            new KeyValuePair<OptionType, string>(OptionType.Call, "call"),
            new KeyValuePair<OptionType, string>(OptionType.Put, "put"),
        };
    }
}
