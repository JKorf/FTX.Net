using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System.Collections.Generic;

namespace FTX.Net.Converters
{
    internal class UnstakeRequestStatusConverter : BaseConverter<UnstakeRequestStatus>
    {
        public UnstakeRequestStatusConverter() : this(true) { }
        public UnstakeRequestStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<UnstakeRequestStatus, string>> Mapping => new List<KeyValuePair<UnstakeRequestStatus, string>>
        {
            new KeyValuePair<UnstakeRequestStatus, string>(UnstakeRequestStatus.Pending, "pending"),
            new KeyValuePair<UnstakeRequestStatus, string>(UnstakeRequestStatus.Cancelled, "cancelled"),
            new KeyValuePair<UnstakeRequestStatus, string>(UnstakeRequestStatus.Processed, "processed"),
        };
    }
}
