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
        public IFTXClientGeneralApi GeneralApi { get; }
        public IFTXClientTradeApi TradeApi { get; }
    }
}