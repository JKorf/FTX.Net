using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Converters
{
    internal class OrderTypeConverter : BaseConverter<OrderType>
    {
        public OrderTypeConverter() : this(true) { }
        public OrderTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OrderType, string>> Mapping => new List<KeyValuePair<OrderType, string>>
        {
            new KeyValuePair<OrderType, string>(OrderType.Limit, "limit"),
            new KeyValuePair<OrderType, string>(OrderType.Market, "market"),
        };
    }
}
