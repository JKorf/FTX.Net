---
title: IFTXClientGeneralApiPay
has_children: false
parent: IFTXClientGeneralApi
grand_parent: IFTXClient
---
*[generated documentation]*  
`FTXClient > GeneralApi > Pay`  
*FTX pay endpoints*
  

***

## CancelOrderAsync  

<p>

*Cancels an order, preventing it from being filled by a future FTX Pay payment. Can only be used on orders that have not already been filled or canceled.*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Pay.CancelOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult> CancelOrderAsync(long appId, long orderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|appId|App id|
|orderId|Order id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CreateOrderAsync  

<p>

*you can pre-register an order, specifying its size and currency, and track its status. When you supply an ID identifying the order to an FTX Pay popup, completion of the payment will also update the status of the order.*  
*To supply an ID, the link you should send payers to(or spawn in a popup for them) is: https://ftx.com/pay/request?id=APP_ID&orderId=ORDER_ID or https://ftx.com/pay/request?id=APP_ID&clientOrderId=CLIENT_ORDER_ID*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Pay.CreateOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXAppOrder>> CreateOrderAsync(long appId, string asset, decimal quantity, bool allowTips, string? notes = default, string? clientOrderId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|appId|Application id|
|asset|The asset of the payment|
|quantity|Size of the desired payment|
|allowTips|Whether or not tips are allowed for the payment|
|_[Optional]_ notes|Notes about this order that are private to the merchant|
|_[Optional]_ clientOrderId|ID for you to track the order with (must be unique to your FTX Pay app)|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAppAndPaymentsAsync  

<p>

*Get the details of an FTXPay app, along with a list of payments to that app. Note that UserId is the id of this app specific to your account as a merchant.*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Pay.GetAppAndPaymentsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXAppDetails>> GetAppAndPaymentsAsync(long appId, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|appId|App id|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|Maximum results|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrdersAsync  

<p>

*Get orders for an app*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Pay.GetOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXAppOrder>>> GetOrdersAsync(long appId, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|appId|App id|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ReturnPaymentAsync  

<p>

*You can return a payment by specifying your app ID and the payment ID. The quantity paid to you (includig the tip, but without the fee that was already applied) will be returned to the payer.*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Pay.ReturnPaymentAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult> ReturnPaymentAsync(long appId, long paymentId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|appId|App id|
|paymentId|Payment id|
|_[Optional]_ ct|Cancellation token|

</p>
