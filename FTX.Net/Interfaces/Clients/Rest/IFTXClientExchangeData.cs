using CryptoExchange.Net.Objects;
using FTX.Net.Enums;
using FTX.Net.Objects;
using FTX.Net.Objects.Futures;
using FTX.Net.Objects.Spot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FTX.Net.Interfaces.Clients.Rest
{
    public interface IFTXClientExchangeData
    {
        /// <summary>
        /// Get the server time
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the list of assets
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXAsset>>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the list of supported symbols
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXSymbol>>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get symbol info
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the orderbook for a symbol
        /// </summary>
        /// <param name="symbol">Symbol to get the book for</param>
        /// <param name="depth">Depth of the book</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOrderbook>> GetOrderBookAsync(string symbol, int depth, CancellationToken ct = default);

        /// <summary>
        /// Get trades for a symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXTrade>>> GetTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get klines for a symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get the list of supported futures
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXFuture>>> GetFuturesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get info on a future
        /// </summary>
        /// <param name="future">Future name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXFuture>> GetFutureAsync(string future, CancellationToken ct = default);

        /// <summary>
        /// Get stats on a future
        /// </summary>
        /// <param name="future">Future name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXFutureStat>> GetFutureStatsAsync(string future, CancellationToken ct = default);

        /// <summary>
        /// Get funding rates
        /// </summary>
        /// <param name="future">Future name</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXFundingRate>>> GetFundingRatesAsync(string? future = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get index weights
        /// </summary>
        /// <param name="index">Index name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, decimal>>> GetIndexWeightsAsync(string index, CancellationToken ct = default);

        /// <summary>
        /// Get the list of expired futures
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXFuture>>> GetExpiredFuturesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get index klines
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXKline>>> GetIndexKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

    }
}
