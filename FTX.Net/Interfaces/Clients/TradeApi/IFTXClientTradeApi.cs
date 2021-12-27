using CryptoExchange.Net.ExchangeInterfaces;
using System;

namespace FTX.Net.Interfaces.Clients.TradeApi
{
    /// <summary>
    /// Trade endpoints
    /// </summary>
    public interface IFTXClientTradeApi : IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IFTXClientTradeApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IFTXClientTradeApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IFTXClientTradeApiTrading Trading { get; }

        /// <summary>
        /// Get the IExchangeClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.
        /// </summary>
        /// <returns></returns>
        IExchangeClient AsExchangeClient();
    }
}
