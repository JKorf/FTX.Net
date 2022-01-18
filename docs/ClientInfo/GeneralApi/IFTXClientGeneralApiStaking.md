---
title: IFTXClientGeneralApiStaking
has_children: false
parent: IFTXClientGeneralApi
grand_parent: IFTXClient
---
*[generated documentation]*  
`FTXClient > GeneralApi > Staking`  
*FTX staking endpoints*
  

***

## CancelUnstakeRequestAsync  

[https://docs.ftx.com/#cancel-unstake-request](https://docs.ftx.com/#cancel-unstake-request)  
<p>

*Cancel an unstake request*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Staking.CancelUnstakeRequestAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<string[]>> CancelUnstakeRequestAsync(long requestId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|requestId|Id of request to unstake|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetStakeBalancesAsync  

[https://docs.ftx.com/#get-stake-balances](https://docs.ftx.com/#get-stake-balances)  
<p>

*Get list of stake balances*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Staking.GetStakeBalancesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXStakeBalance>>> GetStakeBalancesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetStakesAsync  

[https://docs.ftx.com/#get-stakes](https://docs.ftx.com/#get-stakes)  
<p>

*Get list of stakes for the user*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Staking.GetStakesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXStake>>> GetStakesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetStakingRewardsAsync  

[https://docs.ftx.com/#get-staking-rewards](https://docs.ftx.com/#get-staking-rewards)  
<p>

*Get list of staking rewards*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Staking.GetStakingRewardsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXStakeReward>>> GetStakingRewardsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUnstakeRequestsAsync  

[https://docs.ftx.com/#unstake-request](https://docs.ftx.com/#unstake-request)  
<p>

*Get list of unstake requests for the user*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Staking.GetUnstakeRequestsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXUnstakeRequest>>> GetUnstakeRequestsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RequestUnstakeAsync  

[https://docs.ftx.com/#unstake-request-2](https://docs.ftx.com/#unstake-request-2)  
<p>

*Create a new unstake request*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Staking.RequestUnstakeAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXUnstakeRequest>> RequestUnstakeAsync(string asset, decimal quantity, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|quantity|Quantity to unstake|
|_[Optional]_ ct|Cancellation token|

</p>

***

## StakeAsync  

[https://docs.ftx.com/#stake-request](https://docs.ftx.com/#stake-request)  
<p>

*Create a new stake request*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Staking.StakeAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXStake>> StakeAsync(string asset, decimal quantity, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset to stake|
|quantity|Quantity to stake|
|_[Optional]_ ct|Cancellation token|

</p>
