using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System.Collections.Generic;

namespace FTX.Net.Converters
{
    internal class DepositStatusConverter : BaseConverter<DepositStatus>
    {
        public DepositStatusConverter() : this(true) { }
        public DepositStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<DepositStatus, string>> Mapping => new List<KeyValuePair<DepositStatus, string>>
        {
            new KeyValuePair<DepositStatus, string>(DepositStatus.Confirmed, "confirmed"),
            new KeyValuePair<DepositStatus, string>(DepositStatus.Unconfirmed, "unconfirmed"),
            new KeyValuePair<DepositStatus, string>(DepositStatus.Canceled, "cancelled"),
            new KeyValuePair<DepositStatus, string>(DepositStatus.Completed, "completed"),
        };
    }
}
