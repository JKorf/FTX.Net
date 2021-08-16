using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System.Collections.Generic;

namespace FTX.Net.Converters
{
    internal class NFTDepositStatusConverter : BaseConverter<NFTDepositStatus>
    {
        public NFTDepositStatusConverter() : this(true) { }
        public NFTDepositStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<NFTDepositStatus, string>> Mapping => new List<KeyValuePair<NFTDepositStatus, string>>
        {
            new KeyValuePair<NFTDepositStatus, string>(NFTDepositStatus.Confirmed, "confirmed"),
            new KeyValuePair<NFTDepositStatus, string>(NFTDepositStatus.Unconfirmed, "unconfirmed"),
            new KeyValuePair<NFTDepositStatus, string>(NFTDepositStatus.Cancelled, "cancelled"),
        };
    }
}
