using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System.Collections.Generic;

namespace FTX.Net.Converters
{
    internal class TriggerOrderStatusConverter : BaseConverter<TriggerOrderStatus>
    {
        public TriggerOrderStatusConverter() : this(true) { }
        public TriggerOrderStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<TriggerOrderStatus, string>> Mapping => new List<KeyValuePair<TriggerOrderStatus, string>>
        {
            new KeyValuePair<TriggerOrderStatus, string>(TriggerOrderStatus.Open, "open"),
            new KeyValuePair<TriggerOrderStatus, string>(TriggerOrderStatus.Cancelled, "cancelled"),
            new KeyValuePair<TriggerOrderStatus, string>(TriggerOrderStatus.Triggered, "triggered")
        };
    }
}
