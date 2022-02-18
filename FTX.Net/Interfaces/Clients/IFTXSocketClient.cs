using CryptoExchange.Net.Interfaces;
using FTX.Net.Interfaces.Clients.TradeApi;

namespace FTX.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the FTX websocket API
    /// </summary>
    public interface IFTXSocketClient : ISocketClient
    {
        /// <summary>
        /// Streams
        /// </summary>
        public IFTXSocketClientStreams Streams { get; }
    }
}