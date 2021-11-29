using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using FTX.Net.Objects.Models;
using FTX.Net.Objects.Models.Socket;

namespace FTX.Net.Interfaces.Clients.Socket
{
    /// <summary>
    /// FTX socket client 
    /// </summary>
    public interface IFTXSocketClient : ISocketClient
    {
        public IFTXSocketClientMarket Market { get; }
    }
}