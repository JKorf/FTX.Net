---
title: IFTXClientTradeApiExchangeData
has_children: false
parent: IFTXClientTradeApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`FTXClient > TradeApi > ExchangeData`  
*FTX exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.*
  

***

## GetAssetsAsync  

[https://docs.ftx.com/#get-coins](https://docs.ftx.com/#get-coins)  
<p>

*Get the list of assets*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetAssetsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXAsset>>> GetAssetsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetETFRebalanceInfoAsync  

[https://docs.ftx.com/#request-etf-rebalance-info](https://docs.ftx.com/#request-etf-rebalance-info)  
<p>

*Provides information about the most recent rebalance of each ETF.*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetETFRebalanceInfoAsync();  
```  

```csharp  
Task<WebCallResult<Dictionary<string, FTXETFRebalanceEntry>>> GetETFRebalanceInfoAsync(string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetExpiredFuturesAsync  

[https://docs.ftx.com/#get-expired-futures](https://docs.ftx.com/#get-expired-futures)  
<p>

*Get the list of expired futures*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetExpiredFuturesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXFuture>>> GetExpiredFuturesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFundingRatesAsync  

[https://docs.ftx.com/#get-funding-rates](https://docs.ftx.com/#get-funding-rates)  
<p>

*Get funding rates*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetFundingRatesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXFundingRate>>> GetFundingRatesAsync(string? future = default, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ future|Future name|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFutureAsync  

[https://docs.ftx.com/#get-future](https://docs.ftx.com/#get-future)  
<p>

*Get info on a future*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetFutureAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXFuture>> GetFutureAsync(string future, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|future|Future name|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFuturesAsync  

[https://docs.ftx.com/#list-all-futures](https://docs.ftx.com/#list-all-futures)  
<p>

*Get the list of supported futures*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetFuturesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXFuture>>> GetFuturesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFutureStatsAsync  

[https://docs.ftx.com/#get-future-stats](https://docs.ftx.com/#get-future-stats)  
<p>

*Get stats on a future*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetFutureStatsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXFutureStat>> GetFutureStatsAsync(string future, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|future|Future name|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIndexKlinesAsync  

[https://docs.ftx.com/#get-historical-index](https://docs.ftx.com/#get-historical-index)  
<p>

*Get index klines*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetIndexKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXKline>>> GetIndexKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to get trades for|
|interval|Kline interval|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIndexWeightsAsync  

[https://docs.ftx.com/#get-index-weights](https://docs.ftx.com/#get-index-weights)  
<p>

*Get index weights*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetIndexWeightsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<Dictionary<string, decimal>>> GetIndexWeightsAsync(string index, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|index|Index name|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetKlinesAsync  

[https://docs.ftx.com/#get-historical-prices](https://docs.ftx.com/#get-historical-prices)  
<p>

*Get klines for a symbol*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to get trades for|
|interval|Kline interval|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLeveragedTokenAsync  

[https://docs.ftx.com/#get-token-info](https://docs.ftx.com/#get-token-info)  
<p>

*Get info on a token*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetLeveragedTokenAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXLeveragedToken>> GetLeveragedTokenAsync(string tokenName, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|tokenName|Name of the token|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLeveragedTokensAsync  

[https://docs.ftx.com/#list-leveraged-tokens](https://docs.ftx.com/#list-leveraged-tokens)  
<p>

*Get list of leveraged tokens*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetLeveragedTokensAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXLeveragedToken>>> GetLeveragedTokensAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOptionsHistoricalOpenInterestAsync  

[https://docs.ftx.com/#get-option-open-interest-3](https://docs.ftx.com/#get-option-open-interest-3)  
<p>

*Get open interest history*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetOptionsHistoricalOpenInterestAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXOptionHistoricalOpenInterest>>> GetOptionsHistoricalOpenInterestAsync(DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOptionsHistoricalVolumeAsync  

[https://docs.ftx.com/#get-option-open-interest](https://docs.ftx.com/#get-option-open-interest)  
<p>

*Get historical option volume*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetOptionsHistoricalVolumeAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXOptionsHistoricalVolume>>> GetOptionsHistoricalVolumeAsync(DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOptionsOpenInterestAsync  

[https://docs.ftx.com/#get-option-open-interest](https://docs.ftx.com/#get-option-open-interest)  
<p>

*Get open interest*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetOptionsOpenInterestAsync();  
```  

```csharp  
Task<WebCallResult<FTXOptionOpenInterest>> GetOptionsOpenInterestAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOptionsQuoteRequestsAsync  

[https://docs.ftx.com/#list-quote-requests](https://docs.ftx.com/#list-quote-requests)  
<p>

*Get list of quote requests*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetOptionsQuoteRequestsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXQuoteRequest>>> GetOptionsQuoteRequestsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOptionsTradesAsync  

[https://docs.ftx.com/#get-public-options-trades](https://docs.ftx.com/#get-public-options-trades)  
<p>

*Get public options positions*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetOptionsTradesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXOptionTrade>>> GetOptionsTradesAsync(DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOptionsVolumeAsync  

[https://docs.ftx.com/#get-24h-option-volume](https://docs.ftx.com/#get-24h-option-volume)  
<p>

*Get 24H option volume*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetOptionsVolumeAsync();  
```  

```csharp  
Task<WebCallResult<FTXOptionsVolume>> GetOptionsVolumeAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderBookAsync  

[https://docs.ftx.com/#get-orderbook](https://docs.ftx.com/#get-orderbook)  
<p>

*Get the orderbook for a symbol*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetOrderBookAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXOrderbook>> GetOrderBookAsync(string symbol, int depth, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to get the book for|
|depth|Depth of the book|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetServerTimeAsync  

[https://blog.ftx.com/blog/api-authentication/](https://blog.ftx.com/blog/api-authentication/)  
<p>

*Get the server time*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetServerTimeAsync();  
```  

```csharp  
Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSymbolAsync  

[https://docs.ftx.com/#get-single-market](https://docs.ftx.com/#get-single-market)  
<p>

*Get symbol info*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetSymbolAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol name|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSymbolsAsync  

[https://docs.ftx.com/#get-markets](https://docs.ftx.com/#get-markets)  
<p>

*Get the list of supported symbols*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetSymbolsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXSymbol>>> GetSymbolsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTradeHistoryAsync  

[https://docs.ftx.com/#get-trades](https://docs.ftx.com/#get-trades)  
<p>

*Get trades for a symbol*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.ExchangeData.GetTradeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXTrade>>> GetTradeHistoryAsync(string symbol, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to get trades for|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ ct|Cancellation token|

</p>
