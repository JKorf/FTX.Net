using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using FTX.Net.Objects;
using FTX.Net.Objects.SocketObjects;
using FTX.Net.Objects.Spot;
using FTX.Net.Objects.Spot.Socket;

namespace FTX.Net.Interfaces
{
    /// <summary>
    /// FTX socket client 
    /// </summary>
    public interface IFTXSocketClient: ISocketClient
    {
        /// <summary>
        /// Subscribes to ticker updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="handler">The handler for the data</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<FTXStreamTicker>> handler);

        /// <summary>
        /// Subscribes to trade updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="handler">The handler for the data</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<FTXTrade>>> handler);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="handler">The handler for the data</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<FTXStreamOrderBook>> handler);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// </summary>
        /// <param name="symbol">Symbol for the order book</param>
        /// <param name="grouping">Grouping of the data</param>
        /// <param name="handler">The handler for the data</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToGroupedOrderBookUpdatesAsync(string symbol, int grouping, Action<DataEvent<FTXStreamOrderBook>> handler);

        /// <summary>
        /// Subscribes to order updates
        /// </summary>
        /// <param name="handler">The handler for the data</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<FTXOrder>> handler);

        /// <summary>
        /// Subscribes to trade updates
        /// </summary>
        /// <param name="handler">The handler for the data</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<FTXUserTrade>> handler);

        /// <summary>
        /// Subscribes to FTX-pay updates
        /// </summary>
        /// <param name="handler">The handler for the data</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFTXPayUpdatesAsync(Action<DataEvent<FTXUserTrade>> handler);
    }
}