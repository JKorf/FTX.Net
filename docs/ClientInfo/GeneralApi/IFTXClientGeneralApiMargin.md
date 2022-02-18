---
title: IFTXClientGeneralApiMargin
has_children: false
parent: IFTXClientGeneralApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`FTXClient > GeneralApi > Margin`  
*FTX margin endpoints*
  

***

## GetBorrowRatesAsync  

[https://docs.ftx.com/#get-borrow-rates](https://docs.ftx.com/#get-borrow-rates)  
<p>

*Get borrow rates*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Margin.GetBorrowRatesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXBorrowRate>>> GetBorrowRatesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetDailyBorrowedAmountAsync  

[https://docs.ftx.com/#get-daily-borrowed-amounts](https://docs.ftx.com/#get-daily-borrowed-amounts)  
<p>

*Get daily borrowed amount*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Margin.GetDailyBorrowedAmountAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXBorrowSummary>>> GetDailyBorrowedAmountAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLendingHistoryAsync  

[https://docs.ftx.com/#get-lending-history](https://docs.ftx.com/#get-lending-history)  
<p>

*Get lending history*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Margin.GetLendingHistoryAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXLend>>> GetLendingHistoryAsync(DateTime? startTime = default, DateTime? endTime = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLendingInfoAsync  

[https://docs.ftx.com/#get-lending-info](https://docs.ftx.com/#get-lending-info)  
<p>

*Get lending info*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Margin.GetLendingInfoAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXLendingInfo>>> GetLendingInfoAsync(string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLendingOffersAsync  

[https://docs.ftx.com/#get-lending-offers](https://docs.ftx.com/#get-lending-offers)  
<p>

*Get lending offers*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Margin.GetLendingOffersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXLendingOffer>>> GetLendingOffersAsync(string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLendingRatesAsync  

[https://docs.ftx.com/#get-lending-rates](https://docs.ftx.com/#get-lending-rates)  
<p>

*Get lending rates*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Margin.GetLendingRatesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXBorrowRate>>> GetLendingRatesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSymbolSummaryAsync  

[https://docs.ftx.com/#get-market-info](https://docs.ftx.com/#get-market-info)  
<p>

*Get symbol info*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Margin.GetSymbolSummaryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXMarginMarketInfo>>> GetSymbolSummaryAsync(string symbol, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to get info on|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUserBorrowHistoryAsync  

[https://docs.ftx.com/#get-my-borrow-history](https://docs.ftx.com/#get-my-borrow-history)  
<p>

*Get user borrow history*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Margin.GetUserBorrowHistoryAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXUserLend>>> GetUserBorrowHistoryAsync(DateTime? startTime = default, DateTime? endTime = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUserLendingHistoryAsync  

[https://docs.ftx.com/#get-my-lending-history](https://docs.ftx.com/#get-my-lending-history)  
<p>

*Get user lending history*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Margin.GetUserLendingHistoryAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXUserLend>>> GetUserLendingHistoryAsync(DateTime? startTime = default, DateTime? endTime = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceLendingOfferAsync  

[https://docs.ftx.com/#submit-lending-offer](https://docs.ftx.com/#submit-lending-offer)  
<p>

*Submit a lending offer*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Margin.PlaceLendingOfferAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult> PlaceLendingOfferAsync(string asset, decimal quantity, decimal rate, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|quantity|Quantity|
|rate|Rate|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>
