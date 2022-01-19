---
title: IFTXClientTradeApiTrading
has_children: false
parent: IFTXClientTradeApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`FTXClient > TradeApi > Trading`  
*FTX trading endpoints, placing and mananging orders.*
  

***

## AcceptOptionsQuoteAsync  

[https://docs.ftx.com/#accept-options-quote](https://docs.ftx.com/#accept-options-quote)  
<p>

*Accept options quote*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.AcceptOptionsQuoteAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXQuoteRequestQuote>> AcceptOptionsQuoteAsync(long quoteId, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|quoteId|Quote id|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelAllOrdersAsync  

[https://docs.ftx.com/#cancel-all-orders](https://docs.ftx.com/#cancel-all-orders)  
<p>

*Cancel all orders matching the parameters*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.CancelAllOrdersAsync();  
```  

```csharp  
Task<WebCallResult<string>> CancelAllOrdersAsync(string? symbol = default, OrderSide? side = default, bool? conditionalOrdersOnly = default, bool? limitOrdersOnly = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter by symbol|
|_[Optional]_ side|Filter by side|
|_[Optional]_ conditionalOrdersOnly|Only cancel conditional orders|
|_[Optional]_ limitOrdersOnly|Only cancel limit orders|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelOptionsQuoteAsync  

[https://docs.ftx.com/#cancel-quote](https://docs.ftx.com/#cancel-quote)  
<p>

*Cancel a quote*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.CancelOptionsQuoteAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXQuoteRequestQuote>> CancelOptionsQuoteAsync(long quoteId, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|quoteId|Quote id|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelOptionsQuoteRequestAsync  

[https://docs.ftx.com/#cancel-quote-request](https://docs.ftx.com/#cancel-quote-request)  
<p>

*Cancel a quote request*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.CancelOptionsQuoteRequestAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXUserQuoteRequest>> CancelOptionsQuoteRequestAsync(long requestId, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|requestId|Request id to cancel|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelOrderAsync  

[https://docs.ftx.com/#cancel-order](https://docs.ftx.com/#cancel-order)  
<p>

*Cancel an order*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.CancelOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<string>> CancelOrderAsync(long orderId, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|Id of the order|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelOrderByClientIdAsync  

[https://docs.ftx.com/#cancel-order-by-client-id](https://docs.ftx.com/#cancel-order-by-client-id)  
<p>

*Cancel an order*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.CancelOrderByClientIdAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<string>> CancelOrderByClientIdAsync(string clientOrderId, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|clientOrderId|Client order id of the order|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelTriggerOrderAsync  

[https://docs.ftx.com/#cancel-open-trigger-order](https://docs.ftx.com/#cancel-open-trigger-order)  
<p>

*Cancel a trigger order*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.CancelTriggerOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<string>> CancelTriggerOrderAsync(long orderId, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|Id of the order|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CreateOptionsQuoteAsync  

[https://docs.ftx.com/#create-quote](https://docs.ftx.com/#create-quote)  
<p>

*Create quote*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.CreateOptionsQuoteAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXUserQuoteRequest>> CreateOptionsQuoteAsync(long requestId, decimal price, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|requestId|Request id|
|price|Price of the quote|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CreateOptionsQuoteRequestAsync  

[https://docs.ftx.com/#create-quote-request](https://docs.ftx.com/#create-quote-request)  
<p>

*Create quote request*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.CreateOptionsQuoteRequestAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXQuoteRequest>> CreateOptionsQuoteRequestAsync(string underlying, OptionType type, decimal strike, DateTime expiry, OrderSide side, decimal size, decimal? limitPrice = default, bool? hideLimitPrice = default, DateTime? requestExpiry = default, long? counterPartyId = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|underlying|Underlying|
|type|Type|
|strike|Strike|
|expiry|Must be in the future and at 03:00 UTC.|
|side|Side|
|size|Size|
|_[Optional]_ limitPrice|Limit price|
|_[Optional]_ hideLimitPrice|Whether or not to hide your limit price from potential quoters, default true|
|_[Optional]_ requestExpiry|Request expiry|
|_[Optional]_ counterPartyId|When specified, makes the request private to the specified counterparty|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLeveragedTokenCreationRequestsAsync  

[https://docs.ftx.com/#list-leveraged-token-creation-requests](https://docs.ftx.com/#list-leveraged-token-creation-requests)  
<p>

*Get creation requests*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.GetLeveragedTokenCreationRequestsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXLeveragedTokenCreationRequest>>> GetLeveragedTokenCreationRequestsAsync(string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLeveragedTokenRedemptionRequestsAsync  

[https://docs.ftx.com/#list-leveraged-token-redemption-requests](https://docs.ftx.com/#list-leveraged-token-redemption-requests)  
<p>

*Get redemption requests*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.GetLeveragedTokenRedemptionRequestsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXLeveragedTokenRedemption>>> GetLeveragedTokenRedemptionRequestsAsync(string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenOrdersAsync  

[https://docs.ftx.com/#get-open-orders](https://docs.ftx.com/#get-open-orders)  
<p>

*Get a list of open orders*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.GetOpenOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXOrder>>> GetOpenOrdersAsync(string? symbol = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter by symbol|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenTriggerOrdersAsync  

[https://docs.ftx.com/#get-open-trigger-orders](https://docs.ftx.com/#get-open-trigger-orders)  
<p>

*Get a list of open trigger orders*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.GetOpenTriggerOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXTriggerOrder>>> GetOpenTriggerOrdersAsync(string? symbol = default, TriggerOrderType? type = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter by symbol|
|_[Optional]_ type|Filter by type|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOptionsQuotesForQuoteRequestAsync  

[https://docs.ftx.com/#get-quotes-for-your-quote-request](https://docs.ftx.com/#get-quotes-for-your-quote-request)  
<p>

*Get quotes for your quote request*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.GetOptionsQuotesForQuoteRequestAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXQuoteRequestQuote>>> GetOptionsQuotesForQuoteRequestAsync(long requestId, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|requestId|Request id|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOptionsUserQuoteRequestsAsync  

[https://docs.ftx.com/#your-quote-requests](https://docs.ftx.com/#your-quote-requests)  
<p>

*Get list of quote requests for the user*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.GetOptionsUserQuoteRequestsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXUserQuoteRequest>>> GetOptionsUserQuoteRequestsAsync(string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOptionsUserQuotesAsync  

[https://docs.ftx.com/#get-my-quotes](https://docs.ftx.com/#get-my-quotes)  
<p>

*Get quotes for user*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.GetOptionsUserQuotesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXQuoteRequestQuote>>> GetOptionsUserQuotesAsync(string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOptionsUserTradesAsync  

[https://docs.ftx.com/#get-options-fills](https://docs.ftx.com/#get-options-fills)  
<p>

*Get options fills*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.GetOptionsUserTradesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXUserOptionTrade>>> GetOptionsUserTradesAsync(DateTime? startTime = default, DateTime? endTime = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderAsync  

[https://docs.ftx.com/#get-order-status](https://docs.ftx.com/#get-order-status)  
<p>

*Get the status of an order*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.GetOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXOrder>> GetOrderAsync(long orderId, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|Id of the order|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderByClientOrderIdAsync  

[https://docs.ftx.com/#get-order-status-by-client-id](https://docs.ftx.com/#get-order-status-by-client-id)  
<p>

*Get the status of an order*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.GetOrderByClientOrderIdAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|clientOrderId|Client order id of the order|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrdersAsync  

[https://docs.ftx.com/#get-order-history](https://docs.ftx.com/#get-order-history)  
<p>

*Get list of orders*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.GetOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXOrder>>> GetOrdersAsync(string? symbol = default, DateTime? startTime = default, DateTime? endTime = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter by symbol|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTriggerOrdersAsync  

[https://docs.ftx.com/#get-trigger-order-history](https://docs.ftx.com/#get-trigger-order-history)  
<p>

*Get list of trigger orders*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.GetTriggerOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXTriggerOrder>>> GetTriggerOrdersAsync(string? symbol = default, DateTime? startTime = default, DateTime? endTime = default, OrderSide? side = default, TriggerOrderType? type = default, OrderType? orderType = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter by symbol|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ side|Filter by side|
|_[Optional]_ type|Filter by trigger type|
|_[Optional]_ orderType|Filter by order type|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTriggerOrderTriggersAsync  

[https://docs.ftx.com/#get-trigger-order-triggers](https://docs.ftx.com/#get-trigger-order-triggers)  
<p>

*Get a list triggers for a trigger order*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.GetTriggerOrderTriggersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXTriggerOrderTrigger>>> GetTriggerOrderTriggersAsync(long orderId, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|Id of the trigger order|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUserTradesAsync  

[https://docs.ftx.com/#fills](https://docs.ftx.com/#fills)  
<p>

*Get list of trades based on the input parameters*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.GetUserTradesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXUserTrade>>> GetUserTradesAsync(string? symbol = default, long? orderId = default, bool? ascendingOrder = default, DateTime? startTime = default, DateTime? endTime = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter by symbol|
|_[Optional]_ orderId|Filter by order id|
|_[Optional]_ ascendingOrder|Return results in ascending order in time|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ModifyOrderAsync  

[https://docs.ftx.com/#modify-order](https://docs.ftx.com/#modify-order)  
<p>

*Modify an order. Will internally cancel the original order and place a new order with the new price/quantity. The new order will have a new order id. Note: there's a chance that the order meant to be canceled gets filled and its replacement still gets placed.*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.ModifyOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXOrder>> ModifyOrderAsync(long orderId, decimal? price = default, decimal? quantity = default, string? clientOrderId = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|Id of order to modify|
|_[Optional]_ price|New price of the order|
|_[Optional]_ quantity|New quantity of the order|
|_[Optional]_ clientOrderId|New client order id|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ModifyOrderByClientOrderIdAsync  

[https://docs.ftx.com/#modify-order-by-client-id](https://docs.ftx.com/#modify-order-by-client-id)  
<p>

*Modify an order. Will internally cancel the original order and place a new order with the new price/quantity. The new order will have a new order id. Note: there's a chance that the order meant to be canceled gets filled and its replacement still gets placed.*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.ModifyOrderByClientOrderIdAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXOrder>> ModifyOrderByClientOrderIdAsync(long clientOrderId, decimal? price = default, decimal? quantity = default, string? newClientOrderId = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|clientOrderId|Client order id of order to modify|
|_[Optional]_ price|New price of the order|
|_[Optional]_ quantity|New quantity of the order|
|_[Optional]_ newClientOrderId|New client order id|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ModifyTriggerOrderAsync  

[https://docs.ftx.com/#modify-trigger-order](https://docs.ftx.com/#modify-trigger-order)  
<p>

*Modify a trigger order. Will internally cancel the original order and place a new order with the new price/quantity. The new order will have a new order id. Note: there's a chance that the order meant to be canceled gets filled and its replacement still gets placed.*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.ModifyTriggerOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXTriggerOrder>> ModifyTriggerOrderAsync(long orderId, decimal? quantity = default, decimal? triggerPrice = default, decimal? orderPrice = default, decimal? trailingValue = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|Order id|
|_[Optional]_ quantity|New quantity|
|_[Optional]_ triggerPrice|New trigger price|
|_[Optional]_ orderPrice|New order price|
|_[Optional]_ trailingValue|New trailing value|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceOrderAsync  

[https://docs.ftx.com/#place-order](https://docs.ftx.com/#place-order)  
<p>

*Place a new order*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.PlaceOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXOrder>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal quantity, decimal? price = default, bool? reduceOnly = default, bool? immediateOrCancel = default, bool? postOnly = default, string? clientOrderId = default, bool? rejectOnPriceBand = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to place on|
|side|The side of the order|
|type|The type of order|
|quantity|The quantity to buy or sell|
|_[Optional]_ price|The price of the order (null for market orders)|
|_[Optional]_ reduceOnly|Reduce only|
|_[Optional]_ immediateOrCancel|Immediate or cancel|
|_[Optional]_ postOnly|Post only|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ rejectOnPriceBand|If the order should be rejected if its price would instead be adjusted due to price bands|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceTriggerOrderAsync  

[https://docs.ftx.com/#place-trigger-order](https://docs.ftx.com/#place-trigger-order)  
<p>

*Place a new trigger order*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.PlaceTriggerOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXTriggerOrder>> PlaceTriggerOrderAsync(string symbol, OrderSide side, TriggerOrderType type, decimal quantity, bool? reduceOnly = default, bool? retryUntilFilled = default, decimal? triggerPrice = default, decimal? orderPrice = default, decimal? trailValue = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to place on|
|side|The side of the order|
|type|The trigger type|
|quantity|The quantity to buy or sell|
|_[Optional]_ reduceOnly|Reduce only|
|_[Optional]_ retryUntilFilled|Whether or not to keep re-triggering until filled. optional, default true for market orders|
|_[Optional]_ triggerPrice|Trigger price for stop loss/take profit|
|_[Optional]_ orderPrice|Order price, specifying this makes the order a limit order|
|_[Optional]_ trailValue|Tailing value for trailing stop orders, negative for sell, positive for buy|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RequestLeveragedTokenCreationAsync  

[https://docs.ftx.com/#request-leveraged-token-creation](https://docs.ftx.com/#request-leveraged-token-creation)  
<p>

*Request leveraged token creation*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.RequestLeveragedTokenCreationAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXLeveragedTokenCreationRequest>> RequestLeveragedTokenCreationAsync(string tokenName, decimal size, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|tokenName|Name of the token|
|size|Number of tokens to create|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RequestLeveragedTokenRedemptionAsync  

[https://docs.ftx.com/#request-leveraged-token-redemption](https://docs.ftx.com/#request-leveraged-token-redemption)  
<p>

*Request leveraged token redemption*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Trading.RequestLeveragedTokenRedemptionAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXLeveragedTokenRedeemRequest>> RequestLeveragedTokenRedemptionAsync(string tokenName, decimal size, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|tokenName|Name of the token|
|size|Number of tokens to create|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>
