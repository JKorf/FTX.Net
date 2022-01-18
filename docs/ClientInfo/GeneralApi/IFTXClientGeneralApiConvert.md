---
title: IFTXClientGeneralApiConvert
has_children: false
parent: IFTXClientGeneralApi
grand_parent: IFTXClient
---
*[generated documentation]*  
`FTXClient > GeneralApi > Convert`  
*FTX convert endpoints*
  

***

## AcceptQuoteAsync  

[https://docs.ftx.com/#accept-quote](https://docs.ftx.com/#accept-quote)  
<p>

*Accept a convert quote*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Convert.AcceptQuoteAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult> AcceptQuoteAsync(long quoteId, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|quoteId|Id of quote to accept|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CreateQuoteRequestAsync  

[https://docs.ftx.com/#request-quote](https://docs.ftx.com/#request-quote)  
<p>

*Create a new quote request*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Convert.CreateQuoteRequestAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXConvertQuoteResult>> CreateQuoteRequestAsync(string fromAsset, string toAsset, decimal quantity, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|fromAsset|From asset|
|toAsset|To asset|
|quantity|Quantity|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetQuoteStatusAsync  

[https://docs.ftx.com/#get-quote-status](https://docs.ftx.com/#get-quote-status)  
<p>

*Get quote status*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Convert.GetQuoteStatusAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXConvertQuote>>> GetQuoteStatusAsync(long quoteId, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|quoteId|Quote id|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>
