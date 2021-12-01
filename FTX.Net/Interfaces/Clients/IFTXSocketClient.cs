using CryptoExchange.Net.Interfaces;
using FTX.Net.Interfaces.Clients.TradeApi;

namespace FTX.Net.Interfaces.Clients
{
    /// <summary>
    /// FTX socket client 
    /// </summary>
    public interface IFTXSocketClient : ISocketClient
    {
        public IFTXSocketClientStreams Streams { get; }
    }
}