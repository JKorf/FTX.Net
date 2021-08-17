using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System.Collections.Generic;

namespace FTX.Net.Converters
{
    internal class FTXPayOrderStatusConverter : BaseConverter<FTXPayOrderStatus>
    {
        public FTXPayOrderStatusConverter() : this(true) { }
        public FTXPayOrderStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FTXPayOrderStatus, string>> Mapping => new List<KeyValuePair<FTXPayOrderStatus, string>>
        {
            new KeyValuePair<FTXPayOrderStatus, string>(FTXPayOrderStatus.Complete, "complete"),
            new KeyValuePair<FTXPayOrderStatus, string>(FTXPayOrderStatus.Incomplete, "incomplete"),
        };
    }
}
