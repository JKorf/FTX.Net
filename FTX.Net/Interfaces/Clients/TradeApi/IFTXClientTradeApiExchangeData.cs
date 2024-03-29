﻿using CryptoExchange.Net.Objects;
using FTX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Objects.Models;
using FTX.Net.Objects.Models.Options;
using FTX.Net.Objects.Models.LeveragedTokens;

namespace FTX.Net.Interfaces.Clients.TradeApi
{
    /// <summary>
    /// FTX exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IFTXClientTradeApiExchangeData
    {
        /// <summary>
        /// Get the server time
        /// <para><a href="https://blog.ftx.com/blog/api-authentication/" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the list of assets
        /// <para><a href="https://docs.ftx.com/#get-coins" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXAsset>>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the list of supported symbols
        /// <para><a href="https://docs.ftx.com/#get-markets" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXSymbol>>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get symbol info
        /// <para><a href="https://docs.ftx.com/#get-single-market" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the orderbook for a symbol
        /// <para><a href="https://docs.ftx.com/#get-orderbook" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to get the book for</param>
        /// <param name="depth">Depth of the book</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOrderbook>> GetOrderBookAsync(string symbol, int depth, CancellationToken ct = default);

        /// <summary>
        /// Get trades for a symbol
        /// <para><a href="https://docs.ftx.com/#get-trades" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXTrade>>> GetTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get klines for a symbol
        /// <para><a href="https://docs.ftx.com/#get-historical-prices" /></para>
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
        /// <para><a href="https://docs.ftx.com/#list-all-futures" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXFuture>>> GetFuturesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get info on a future
        /// <para><a href="https://docs.ftx.com/#get-future" /></para>
        /// </summary>
        /// <param name="future">Future name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXFuture>> GetFutureAsync(string future, CancellationToken ct = default);

        /// <summary>
        /// Get stats on a future
        /// <para><a href="https://docs.ftx.com/#get-future-stats" /></para>
        /// </summary>
        /// <param name="future">Future name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXFutureStat>> GetFutureStatsAsync(string future, CancellationToken ct = default);

        /// <summary>
        /// Get funding rates
        /// <para><a href="https://docs.ftx.com/#get-funding-rates" /></para>
        /// </summary>
        /// <param name="future">Future name</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXFundingRate>>> GetFundingRatesAsync(string? future = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get index weights
        /// <para><a href="https://docs.ftx.com/#get-index-weights" /></para>
        /// </summary>
        /// <param name="index">Index name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, decimal>>> GetIndexWeightsAsync(string index, CancellationToken ct = default);

        /// <summary>
        /// Get the list of expired futures
        /// <para><a href="https://docs.ftx.com/#get-expired-futures" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXFuture>>> GetExpiredFuturesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get index klines
        /// <para><a href="https://docs.ftx.com/#get-historical-index" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXKline>>> GetIndexKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of leveraged tokens
        /// <para><a href="https://docs.ftx.com/#list-leveraged-tokens" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXLeveragedToken>>> GetLeveragedTokensAsync(CancellationToken ct = default);

        /// <summary>
        /// Get info on a token
        /// <para><a href="https://docs.ftx.com/#get-token-info" /></para>
        /// </summary>
        /// <param name="tokenName">Name of the token</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXLeveragedToken>> GetLeveragedTokenAsync(string tokenName, CancellationToken ct = default);

        /// <summary>
        /// Provides information about the most recent rebalance of each ETF.
        /// <para><a href="https://docs.ftx.com/#request-etf-rebalance-info" /></para>
        /// </summary>
        /// <param name="subaccountName">Subaccount name to execute this request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, FTXETFRebalanceEntry>>> GetETFRebalanceInfoAsync(string? subaccountName = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of quote requests
        /// <para><a href="https://docs.ftx.com/#list-quote-requests" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXQuoteRequest>>> GetOptionsQuoteRequestsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get public options positions
        /// <para><a href="https://docs.ftx.com/#get-public-options-trades" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXOptionTrade>>> GetOptionsTradesAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get 24H option volume
        /// <para><a href="https://docs.ftx.com/#get-24h-option-volume" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOptionsVolume>> GetOptionsVolumeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get historical option volume
        /// <para><a href="https://docs.ftx.com/#get-option-open-interest" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXOptionsHistoricalVolume>>> GetOptionsHistoricalVolumeAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get open interest
        /// <para><a href="https://docs.ftx.com/#get-option-open-interest" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXOptionOpenInterest>> GetOptionsOpenInterestAsync(CancellationToken ct = default);

        /// <summary>
        /// Get open interest history
        /// <para><a href="https://docs.ftx.com/#get-option-open-interest-3" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXOptionHistoricalOpenInterest>>> GetOptionsHistoricalOpenInterestAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);
    }
}
