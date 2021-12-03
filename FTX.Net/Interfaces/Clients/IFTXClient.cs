using CryptoExchange.Net.Interfaces;
using FTX.Net.Interfaces.Clients.GeneralApi;
using FTX.Net.Interfaces.Clients.TradeApi;

namespace FTX.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the FTX API. 
    /// </summary>
    public interface IFTXClient : IRestClient
    {
        /// <summary>
        /// General API endpoints
        /// </summary>
        public IFTXClientGeneralApi GeneralApi { get; }
        /// <summary>
        /// Trade API endpoints
        /// </summary>
        public IFTXClientTradeApi TradeApi { get; }
    }
}