using FTX.Net.Interfaces.Clients.Rest;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Interfaces.Clients.General
{
    public interface IFTXClientGeneral
    {
        /// <summary>
        /// Convert
        /// </summary>
        IFTXClientGeneralConvert Convert { get; }

        /// <summary>
        /// Staking endpoints
        /// </summary>
        IFTXClientGeneralStaking Staking { get; }

        /// <summary>
        /// Spot margin endpoints
        /// </summary>
        IFTXClientGeneralMargin Margin { get; }

        /// <summary>
        /// NFT endpoints
        /// </summary>
        IFTXClientGeneralNft NFT { get; }

        /// <summary>
        /// FTXPay endpoints
        /// </summary>
        IFTXClientGeneralPay FTXPay { get; }

        /// <summary>
        /// Subaccount endpoints
        /// </summary>
        IFTXClientGeneralSubaccounts Subaccounts { get; }
    }
}
