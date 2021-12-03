using System;

namespace FTX.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// General API endpoints
    /// </summary>
    public interface IFTXClientGeneralApi : IDisposable
    {
        /// <summary>
        /// Convert endpoints
        /// <para><a href="https://docs.ftx.com/#convert" /></para>
        /// </summary>
        IFTXClientGeneralApiConvert Convert { get; }

        /// <summary>
        /// Staking endpoints
        /// <para><a href="https://docs.ftx.com/#staking" /></para>
        /// </summary>
        IFTXClientGeneralApiStaking Staking { get; }

        /// <summary>
        /// Spot margin endpoints
        /// <para><a href="https://docs.ftx.com/#spot-margin" /></para>
        /// </summary>
        IFTXClientGeneralApiMargin Margin { get; }

        /// <summary>
        /// NFT endpoints
        /// <para><a href="https://docs.ftx.com/#nfts" /></para>
        /// </summary>
        IFTXClientGeneralApiNft NFT { get; }

        /// <summary>
        /// FTXPay endpoints
        /// </summary>
        IFTXClientGeneralApiPay FTXPay { get; }

        /// <summary>
        /// Subaccount endpoints
        /// <para><a href="https://docs.ftx.com/#subaccounts" /></para>
        /// </summary>
        IFTXClientGeneralApiSubaccounts Subaccounts { get; }
    }
}
