using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System.Collections.Generic;

namespace FTX.Net.Converters
{
    internal class QuoteRequestStatusConverter : BaseConverter<QuoteRequestStatus>
    {
        public QuoteRequestStatusConverter() : this(true) { }
        public QuoteRequestStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<QuoteRequestStatus, string>> Mapping => new List<KeyValuePair<QuoteRequestStatus, string>>
        {
            new KeyValuePair<QuoteRequestStatus, string>(QuoteRequestStatus.Canceled, "cancelled"),
            new KeyValuePair<QuoteRequestStatus, string>(QuoteRequestStatus.Open, "open"),
            new KeyValuePair<QuoteRequestStatus, string>(QuoteRequestStatus.Filled, "filled"),
        };
    }
}
