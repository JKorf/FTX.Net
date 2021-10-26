using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System.Collections.Generic;

namespace FTX.Net.Converters
{
    internal class NFTRedeemStatusConverter : BaseConverter<NFTRedeemStatus>
    {
        public NFTRedeemStatusConverter() : this(true) { }
        public NFTRedeemStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<NFTRedeemStatus, string>> Mapping => new List<KeyValuePair<NFTRedeemStatus, string>>
        {
            new KeyValuePair<NFTRedeemStatus, string>(NFTRedeemStatus.Requested, "requested"),
            new KeyValuePair<NFTRedeemStatus, string>(NFTRedeemStatus.PendingReview, "pending_review"),
            new KeyValuePair<NFTRedeemStatus, string>(NFTRedeemStatus.Processing, "processing"),
            new KeyValuePair<NFTRedeemStatus, string>(NFTRedeemStatus.Sent, "sent"),
            new KeyValuePair<NFTRedeemStatus, string>(NFTRedeemStatus.Completed, "complete"),
            new KeyValuePair<NFTRedeemStatus, string>(NFTRedeemStatus.Canceled, "cancelled"),
            new KeyValuePair<NFTRedeemStatus, string>(NFTRedeemStatus.Failed, "failed"),
        };
    }
}
