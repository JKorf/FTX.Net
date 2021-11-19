using CryptoExchange.Net.Interfaces;

namespace FTX.Net.Interfaces.Clients.Rest
{
    /// <summary>
    /// Client for accessing the FTX API. 
    /// </summary>
    public interface IFTXClient : IRestClient
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IFTXClientAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IFTXClientExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IFTXClientTrading Trading { get; }

        /// <summary>
        /// Convert
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