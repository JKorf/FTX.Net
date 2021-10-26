using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System.Collections.Generic;

namespace FTX.Net.Converters
{
    internal class NFTWithdrawalStatusConverter : BaseConverter<NFTWithdrawalStatus>
    {
        public NFTWithdrawalStatusConverter() : this(true) { }
        public NFTWithdrawalStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<NFTWithdrawalStatus, string>> Mapping => new List<KeyValuePair<NFTWithdrawalStatus, string>>
        {
            new KeyValuePair<NFTWithdrawalStatus, string>(NFTWithdrawalStatus.Canceled, "cancelled"),
            new KeyValuePair<NFTWithdrawalStatus, string>(NFTWithdrawalStatus.Completed, "completed"),
            new KeyValuePair<NFTWithdrawalStatus, string>(NFTWithdrawalStatus.Processing, "processing"),
            new KeyValuePair<NFTWithdrawalStatus, string>(NFTWithdrawalStatus.Requested, "requested"),
            new KeyValuePair<NFTWithdrawalStatus, string>(NFTWithdrawalStatus.Sent, "sent"),
        };
    }
}
