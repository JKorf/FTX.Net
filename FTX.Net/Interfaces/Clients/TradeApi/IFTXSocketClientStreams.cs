using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using FTX.Net.Objects.Models;
using FTX.Net.Objects.Models.Socket;

namespace FTX.Net.Interfaces.Clients.TradeApi
{
    /// <summary>
    /// FTX streams
    /// </summary>
    public interface IFTXSocketClientStreams : IDisposable
    {
        /// <summary>
        /// Subscribes to ticker updates for a symbol
        /// <para><a href="https://docs.ftx.com/#ticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="handler">The handler for the data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<FTXStreamTicker>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to symbol updates
        /// <para><a href="https://docs.ftx.com/#markets-2" /></para>
        /// </summary>
        /// <param name="handler">The handler for the data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolsUpdatesAsync(Action<DataEvent<Dictionary<string, FTXStreamSymbol>>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to trade updates for a symbol
        /// <para><a href="https://docs.ftx.com/#trades" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="handler">The handler for the data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<FTXTrade>>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// <para><a href="https://docs.ftx.com/#orderbooks" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="handler">The handler for the data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<FTXStreamOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// <para><a href="https://docs.ftx.com/#grouped-orderbooks" /></para>
        /// </summary>
        /// <param name="symbol">Symbol for the order book</param>
        /// <param name="grouping">Grouping of the data</param>
        /// <param name="handler">The handler for the data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToGroupedOrderBookUpdatesAsync(string symbol, int grouping, Action<DataEvent<FTXStreamOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to order updates
        /// <para><a href="https://docs.ftx.com/#orders-2" /></para>
        /// </summary>
        /// <param name="handler">The handler for the data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<FTXOrder>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to trade updates
        /// <para><a href="https://docs.ftx.com/#fills-2" /></para>
        /// </summary>
        /// <param name="handler">The handler for the data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<FTXUserTrade>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to FTX-pay updates
        /// <para><a href="https://docs.ftx.com/#ftx-pay" /></para>
        /// </summary>
        /// <param name="handler">The handler for the data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFTXPayUpdatesAsync(Action<DataEvent<FTXUserTrade>> handler, CancellationToken ct = default);
    }
}