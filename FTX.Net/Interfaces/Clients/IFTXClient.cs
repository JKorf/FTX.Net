using CryptoExchange.Net.Interfaces;
using FTX.Net.Interfaces.Clients.General;
using FTX.Net.Interfaces.Clients.Market;

namespace FTX.Net.Interfaces.Clients.Rest
{
    /// <summary>
    /// Client for accessing the FTX API. 
    /// </summary>
    public interface IFTXClient : IRestClient
    {
        public IFTXClientGeneral General { get; }
        public IFTXClientMarket Market { get; }
    }
}