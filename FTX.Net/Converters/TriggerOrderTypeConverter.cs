using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System.Collections.Generic;

namespace FTX.Net.Converters
{
    internal class TriggerOrderTypeConverter : BaseConverter<TriggerOrderType>
    {
        public TriggerOrderTypeConverter() : this(true) { }
        public TriggerOrderTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<TriggerOrderType, string>> Mapping => new List<KeyValuePair<TriggerOrderType, string>>
        {
            new KeyValuePair<TriggerOrderType, string>(TriggerOrderType.Stop, "stop"),
            new KeyValuePair<TriggerOrderType, string>(TriggerOrderType.TakeProfit, "takeProfit"),
            new KeyValuePair<TriggerOrderType, string>(TriggerOrderType.TrailingStop, "trailingStop")
        };
    }
}
