---
title: IFTXClientTradeApiAccount
has_children: false
parent: IFTXClientTradeApi
grand_parent: IFTXClient
---
*[generated documentation]*  
`FTXClient > TradeApi > Account`  
*FTX account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings*
  

***

## ChangeAccountLeverageAsync  

[https://docs.ftx.com/#change-account-leverage](https://docs.ftx.com/#change-account-leverage)  
<p>

*Change account leverage*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.ChangeAccountLeverageAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult> ChangeAccountLeverageAsync(decimal leverage, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|leverage|Desired acccount-wide leverage setting|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CreateSavedAddressAsync  

[https://docs.ftx.com/#create-saved-addresses](https://docs.ftx.com/#create-saved-addresses)  
<p>

*Create a saved address*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.CreateSavedAddressAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXSavedAddress>> CreateSavedAddressAsync(string asset, string address, string addressName, bool isPrimeTrust, string? tag = default, string? code = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset the address is for|
|address|The address|
|addressName|Name of the address|
|isPrimeTrust|Is prime trust|
|_[Optional]_ tag|Address tag|
|_[Optional]_ code|2FA code if needed|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## DeleteSavedAddressAsync  

[https://docs.ftx.com/#delete-saved-addresses](https://docs.ftx.com/#delete-saved-addresses)  
<p>

*Delete a saved address*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.DeleteSavedAddressAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<string>> DeleteSavedAddressAsync(long savedAddressId, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|savedAddressId|Id of the saved address to delete|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAccountInfoAsync  

[https://docs.ftx.com/#get-account-information](https://docs.ftx.com/#get-account-information)  
<p>

*Get account info*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.GetAccountInfoAsync();  
```  

```csharp  
Task<WebCallResult<FTXAccountInfo>> GetAccountInfoAsync(string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAirdropsAsync  

[https://docs.ftx.com/#get-airdrops](https://docs.ftx.com/#get-airdrops)  
<p>

*Get airdrops*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.GetAirdropsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXAirdrop>>> GetAirdropsAsync(DateTime? startTime = default, DateTime? endTime = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAllAccountBalancesAsync  

[https://docs.ftx.com/#get-balances-of-all-accounts](https://docs.ftx.com/#get-balances-of-all-accounts)  
<p>

*Get a list of balances, including master and subaccounts*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.GetAllAccountBalancesAsync();  
```  

```csharp  
Task<WebCallResult<Dictionary<string, IEnumerable<FTXBalance>>>> GetAllAccountBalancesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBalancesAsync  

[https://docs.ftx.com/#get-balances](https://docs.ftx.com/#get-balances)  
<p>

*Get a list of balances*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.GetBalancesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXBalance>>> GetBalancesAsync(string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetDepositAddressAsync  

[https://docs.ftx.com/#get-deposit-address](https://docs.ftx.com/#get-deposit-address)  
<p>

*Get deposit address for an asset*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.GetDepositAddressAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXDepositAddress>> GetDepositAddressAsync(string asset, string? network = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset to get address for|
|_[Optional]_ network|The network to use|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetDepositHistoryAsync  

[https://docs.ftx.com/#get-deposit-history](https://docs.ftx.com/#get-deposit-history)  
<p>

*Get deposit history*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.GetDepositHistoryAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXDeposit>>> GetDepositHistoryAsync(DateTime? startTime = default, DateTime? endTime = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFundingPaymentsAsync  

[https://docs.ftx.com/#funding-payments](https://docs.ftx.com/#funding-payments)  
<p>

*Get list of funding payments*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.GetFundingPaymentsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXFundingPayment>>> GetFundingPaymentsAsync(string? future = default, DateTime? startTime = default, DateTime? endTime = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ future|Filter by future|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLeveragedTokenBalancesAsync  

[https://docs.ftx.com/#get-leveraged-token-balances](https://docs.ftx.com/#get-leveraged-token-balances)  
<p>

*Get token balances*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.GetLeveragedTokenBalancesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXLeveragedTokenBalance>>> GetLeveragedTokenBalancesAsync(string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOptionsAccountInfoAsync  

[https://docs.ftx.com/#get-account-options-info](https://docs.ftx.com/#get-account-options-info)  
<p>

*Get account options info*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.GetOptionsAccountInfoAsync();  
```  

```csharp  
Task<WebCallResult<FTXOptionsAccountInfo>> GetOptionsAccountInfoAsync(string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOptionsPositionsAsync  

[https://docs.ftx.com/#get-options-positions](https://docs.ftx.com/#get-options-positions)  
<p>

*Get options positions*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.GetOptionsPositionsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXOptionsPosition>>> GetOptionsPositionsAsync(string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetPositionsAsync  

[https://docs.ftx.com/#get-positions](https://docs.ftx.com/#get-positions)  
<p>

*Get positions*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.GetPositionsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXPosition>>> GetPositionsAsync(bool? showAveragePrice = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ showAveragePrice||
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSavedAddressesAsync  

[https://docs.ftx.com/#get-saved-addresses](https://docs.ftx.com/#get-saved-addresses)  
<p>

*Get saved addresses*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.GetSavedAddressesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXSavedAddress>>> GetSavedAddressesAsync(string? asset = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetWithdrawalFeesAsync  

[https://docs.ftx.com/#get-withdrawal-fees](https://docs.ftx.com/#get-withdrawal-fees)  
<p>

*Get withdrawal fees*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.GetWithdrawalFeesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXWithdrawalFee>> GetWithdrawalFeesAsync(string asset, decimal quantity, string address, string? tag = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|quantity|Quantity|
|address|Address|
|_[Optional]_ tag|Tag|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetWithdrawalHistoryAsync  

[https://docs.ftx.com/#get-withdrawal-history](https://docs.ftx.com/#get-withdrawal-history)  
<p>

*Get withdrawal history*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.GetWithdrawalHistoryAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXWithdrawal>>> GetWithdrawalHistoryAsync(DateTime? startTime = default, DateTime? endTime = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## WithdrawAsync  

[https://docs.ftx.com/#request-withdrawal](https://docs.ftx.com/#request-withdrawal)  
<p>

*Submit a withdraw request*  

```csharp  
var client = new FTXClient();  
var result = await client.TradeApi.Account.WithdrawAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXWithdrawal>> WithdrawAsync(string asset, decimal quantity, string address, string? tag = default, string? network = default, string? password = default, string? code = default, string? subaccountName = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset to withdraw|
|quantity|Quantity to withdraw|
|address|Address to withdraw to|
|_[Optional]_ tag|Address tag|
|_[Optional]_ network|Network to us|
|_[Optional]_ password|Withdrawal password if required|
|_[Optional]_ code|Two factor authentication code if required|
|_[Optional]_ subaccountName|Subaccount name to execute this request for|
|_[Optional]_ ct|Cancellation token|

</p>
