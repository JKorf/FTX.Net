using CryptoExchange.Net.Interfaces;

namespace FTX.Net.Interfaces.Clients.Rest
{
    /// <summary>
    /// FTX client interface
    /// </summary>
    public interface IFTXClient : IRestClient
    {
        /// <summary>
        /// Convert endpoints
        /// </summary>
        IFTXClientAccount Account { get; }

        /// <summary>
        /// Convert endpoints
        /// </summary>
        IFTXClientExchangeData ExchangeData { get; }

        /// <summary>
        /// Convert endpoints
        /// </summary>
        IFTXClientTrading Trading { get; }

        /// <summary>
        /// Convert endpoints
        /// </summary>
        IFTXClientConvert Convert { get; }

        /// <summary>
        /// Options endpoints
        /// </summary>
        IFTXClientOptions Options { get; }

        /// <summary>
        /// Leveraged token endpoints
        /// </summary>
        IFTXClientLeveragedTokens LeveragedTokens { get; }

        /// <summary>
        /// Staking endpoints
        /// </summary>
        IFTXClientStaking Staking { get; }

        /// <summary>
        /// Spot margin endpoints
        /// </summary>
        IFTXClientMargin Margin { get; }

        /// <summary>
        /// NFT endpoints
        /// </summary>
        IFTXClientNft NFT { get; }

        /// <summary>
        /// FTXPay endpoints
        /// </summary>
        IFTXClientPay FTXPay { get; }

        /// <summary>
        /// Subaccount endpoints
        /// </summary>
        IFTXClientSubaccounts Subaccounts { get; }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        void SetApiCredentials(string apiKey, string apiSecret);
    }
}