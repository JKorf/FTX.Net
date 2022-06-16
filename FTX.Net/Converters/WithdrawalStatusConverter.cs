using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System.Collections.Generic;

namespace FTX.Net.Converters
{
    internal class WithdrawalStatusConverter : BaseConverter<WithdrawalStatus>
    {
        public WithdrawalStatusConverter() : this(true) { }
        public WithdrawalStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<WithdrawalStatus, string>> Mapping => new List<KeyValuePair<WithdrawalStatus, string>>
        {
            new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Requested, "requested"),
            new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Processing, "processing"),
            new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Complete, "complete"),
            new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Sent, "sent"),
            new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Canceled, "cancelled"),
        };
    }
}
