using System;

namespace FTX.Net.Interfaces.Clients.GeneralApi
{
    public interface IFTXClientGeneralApi : IDisposable
    {
        /// <summary>
        /// Convert
        /// </summary>
        IFTXClientGeneralApiConvert Convert { get; }

        /// <summary>
        /// Staking endpoints
        /// </summary>
        IFTXClientGeneralApiStaking Staking { get; }

        /// <summary>
        /// Spot margin endpoints
        /// </summary>
        IFTXClientGeneralApiMargin Margin { get; }

        /// <summary>
        /// NFT endpoints
        /// </summary>
        IFTXClientGeneralApiNft NFT { get; }

        /// <summary>
        /// FTXPay endpoints
        /// </summary>
        IFTXClientGeneralApiPay FTXPay { get; }

        /// <summary>
        /// Subaccount endpoints
        /// </summary>
        IFTXClientGeneralApiSubaccounts Subaccounts { get; }
    }
}
