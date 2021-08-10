using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System.Collections.Generic;

namespace FTX.Net.Converters
{
    internal class AirdropStatusConverter : BaseConverter<AirdropStatus>
    {
        public AirdropStatusConverter() : this(true) { }
        public AirdropStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<AirdropStatus, string>> Mapping => new List<KeyValuePair<AirdropStatus, string>>
        {
            new KeyValuePair<AirdropStatus, string>(AirdropStatus.Pending, "pending"),
            new KeyValuePair<AirdropStatus, string>(AirdropStatus.Confirmed, "confirmed"),
        };
    }
}
