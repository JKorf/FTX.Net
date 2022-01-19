---
title: IFTXSocketClientStreams
has_children: true
parent: IFTXSocketClientTradeStreams
grand_parent: Socket API documentation
---
*[generated documentation]*  
`FTXSocketClient > TradeStreams > Streams`  
*FTX streams*
  

***

## SubscribeToFTXPayUpdatesAsync  

[https://docs.ftx.com/#ftx-pay](https://docs.ftx.com/#ftx-pay)  
<p>

*Subscribes to FTX-pay updates*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeStreams.Streams.SubscribeToFTXPayUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToFTXPayUpdatesAsync(Action<DataEvent<FTXUserTrade>> handler, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|handler|The handler for the data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToGroupedOrderBookUpdatesAsync  

[https://docs.ftx.com/#grouped-orderbooks](https://docs.ftx.com/#grouped-orderbooks)  
<p>

*Subscribes to order book updates for a symbol*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeStreams.Streams.SubscribeToGroupedOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToGroupedOrderBookUpdatesAsync(string symbol, int grouping, Action<DataEvent<FTXStreamOrderBook>> handler, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol for the order book|
|grouping|Grouping of the data|
|handler|The handler for the data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToOrderBookUpdatesAsync  

[https://docs.ftx.com/#orderbooks](https://docs.ftx.com/#orderbooks)  
<p>

*Subscribes to order book updates for a symbol*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeStreams.Streams.SubscribeToOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<FTXStreamOrderBook>> handler, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe to|
|handler|The handler for the data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToOrderUpdatesAsync  

[https://docs.ftx.com/#orders-2](https://docs.ftx.com/#orders-2)  
<p>

*Subscribes to order updates*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeStreams.Streams.SubscribeToOrderUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<FTXOrder>> handler, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|handler|The handler for the data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToSymbolsUpdatesAsync  

[https://docs.ftx.com/#markets-2](https://docs.ftx.com/#markets-2)  
<p>

*Subscribes to symbol updates*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeStreams.Streams.SubscribeToSymbolsUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToSymbolsUpdatesAsync(Action<DataEvent<Dictionary<string,FTXStreamSymbol>>> handler, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|handler|The handler for the data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToTickerUpdatesAsync  

[https://docs.ftx.com/#ticker](https://docs.ftx.com/#ticker)  
<p>

*Subscribes to ticker updates for a symbol*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeStreams.Streams.SubscribeToTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<FTXStreamTicker>> handler, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe to|
|handler|The handler for the data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToTradeUpdatesAsync  

[https://docs.ftx.com/#trades](https://docs.ftx.com/#trades)  
<p>

*Subscribes to trade updates for a symbol*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeStreams.Streams.SubscribeToTradeUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<FTXTrade>>> handler, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe to|
|handler|The handler for the data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToUserTradeUpdatesAsync  

[https://docs.ftx.com/#fills-2](https://docs.ftx.com/#fills-2)  
<p>

*Subscribes to trade updates*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeStreams.Streams.SubscribeToUserTradeUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<FTXUserTrade>> handler, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|handler|The handler for the data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>
