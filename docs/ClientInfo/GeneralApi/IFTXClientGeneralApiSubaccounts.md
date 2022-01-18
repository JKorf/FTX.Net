---
title: IFTXClientGeneralApiSubaccounts
has_children: false
parent: IFTXClientGeneralApi
grand_parent: IFTXClient
---
*[generated documentation]*  
`FTXClient > GeneralApi > Subaccounts`  
*FTX subaccount endpoints*
  

***

## ChangeSubaccountNameAsync  

[https://docs.ftx.com/#change-subaccount-name](https://docs.ftx.com/#change-subaccount-name)  
<p>

*Change the name of a sub account*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Subaccounts.ChangeSubaccountNameAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult> ChangeSubaccountNameAsync(string oldName, string newName, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|oldName|Old name|
|newName|New name|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CreateSubaccountAsync  

[https://docs.ftx.com/#create-subaccount](https://docs.ftx.com/#create-subaccount)  
<p>

*Create a new sub client*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Subaccounts.CreateSubaccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXSubaccount>> CreateSubaccountAsync(string nickname, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|nickname|Name of the subaccount|
|_[Optional]_ ct|Cancellation token|

</p>

***

## DeleteSubaccountAsync  

[https://docs.ftx.com/#delete-subaccount](https://docs.ftx.com/#delete-subaccount)  
<p>

*Delete a subaccount*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Subaccounts.DeleteSubaccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult> DeleteSubaccountAsync(string nickname, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|nickname|Nickname of account to delete|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubaccountBalancesAsync  

[https://docs.ftx.com/#get-subaccount-balances](https://docs.ftx.com/#get-subaccount-balances)  
<p>

*Get subaccount balances*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Subaccounts.GetSubaccountBalancesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXBalance>>> GetSubaccountBalancesAsync(string nickname, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|nickname|Nickname to get|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubaccountsAsync  

[https://docs.ftx.com/#get-all-subaccounts](https://docs.ftx.com/#get-all-subaccounts)  
<p>

*Get list of sub accounts*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Subaccounts.GetSubaccountsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXSubaccount>>> GetSubaccountsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferAsync  

[https://docs.ftx.com/#transfer-between-subaccounts](https://docs.ftx.com/#transfer-between-subaccounts)  
<p>

*Transfer funds between subaccounts*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Subaccounts.TransferAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXSubaccountTransfer>> TransferAsync(string source, string destination, string asset, decimal quantity, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|source|Name of the source subaccount. Use 'main' for the main account|
|destination|Name of the destination subaccount. Use 'main' for the main account|
|asset|Asset to move|
|quantity|Quantity to move|
|_[Optional]_ ct|Cancellation token|

</p>
