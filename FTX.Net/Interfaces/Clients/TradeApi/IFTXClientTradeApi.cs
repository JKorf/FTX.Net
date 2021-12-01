using System;

namespace FTX.Net.Interfaces.Clients.TradeApi
{
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
    }
}
