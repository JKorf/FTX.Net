using FTX.Net.Interfaces.Clients.Rest;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Interfaces.Clients.Market
{
    public interface IFTXClientMarket: IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IFTXClientMarketAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IFTXClientMarketExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IFTXClientMarketTrading Trading { get; }
    }
}
