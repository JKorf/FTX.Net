---
title: IFTXClientGeneralApiNft
has_children: false
parent: IFTXClientGeneralApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`FTXClient > GeneralApi > Nft`  
*FTX NFT endpoints*
  

***

## BuyNftAsync  

[https://docs.ftx.com/#buy-nft](https://docs.ftx.com/#buy-nft)  
<p>

*Buy a NFT*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.BuyNftAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXNft>> BuyNftAsync(long nftId, decimal price, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|nftId|NFT id|
|price|Price|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelAuctionAsync  

[https://docs.ftx.com/#cancel-auction](https://docs.ftx.com/#cancel-auction)  
<p>

*Cancel an auction*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.CancelAuctionAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXNft>> CancelAuctionAsync(long nftId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|nftId|NFT id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CreateAuctionAsync  

[https://docs.ftx.com/#create-auction](https://docs.ftx.com/#create-auction)  
<p>

*Create a new auction*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.CreateAuctionAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXNft>> CreateAuctionAsync(long nftId, decimal initialPrice, decimal reservationPrice, TimeSpan duration, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|nftId|NFT id|
|initialPrice|Initial price|
|reservationPrice|Reservation price|
|duration|Duration of the auction|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CreateNftOfferAsync  

[https://docs.ftx.com/#make-nft-offer](https://docs.ftx.com/#make-nft-offer)  
<p>

*Create an offer of an NFT*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.CreateNftOfferAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXNft>> CreateNftOfferAsync(long nftId, decimal price, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|nftId|NFT id|
|price|Price|
|_[Optional]_ ct|Cancellation token|

</p>

***

## EditAuctionAsync  

[https://docs.ftx.com/#edit-auction](https://docs.ftx.com/#edit-auction)  
<p>

*Edit an auction*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.EditAuctionAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXNft>> EditAuctionAsync(long nftId, decimal reservationPrice, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|nftId|NFT id|
|reservationPrice|Reservation price|
|_[Optional]_ ct|Cancellation token|

</p>

***

## EditGallerySettingsAsync  

[https://docs.ftx.com/#edit-gallery-settings](https://docs.ftx.com/#edit-gallery-settings)  
<p>

*Edit NFT gallery settings*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.EditGallerySettingsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult> EditGallerySettingsAsync(bool isPublic, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|isPublic|Gallery is public or not|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBidsAsync  

[https://docs.ftx.com/#get-bids](https://docs.ftx.com/#get-bids)  
<p>

*Get bids*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.GetBidsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXNft>>> GetBidsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetGallerySettingsAsync  

[https://docs.ftx.com/#get-gallery-settings](https://docs.ftx.com/#get-gallery-settings)  
<p>

*Get NFT gallery settings*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.GetGallerySettingsAsync();  
```  

```csharp  
Task<WebCallResult<FTXNftGallerySettings>> GetGallerySettingsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetNftAllTradesAsync  

[https://docs.ftx.com/#get-all-nft-trades](https://docs.ftx.com/#get-all-nft-trades)  
<p>

*Get all NFT trades*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.GetNftAllTradesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXNftTradeAll>>> GetNftAllTradesAsync(DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetNftAsync  

[https://docs.ftx.com/#get-nft-info](https://docs.ftx.com/#get-nft-info)  
<p>

*Get info on a NFT*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.GetNftAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXNft>> GetNftAsync(long nftId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|nftId|Id of the NFT|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetNftBalancesAsync  

[https://docs.ftx.com/#get-nft-balances](https://docs.ftx.com/#get-nft-balances)  
<p>

*Get user balances*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.GetNftBalancesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXNft>>> GetNftBalancesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetNftCollectionsAsync  

[https://docs.ftx.com/#get-all-nft-collections](https://docs.ftx.com/#get-all-nft-collections)  
<p>

*Get all collections*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.GetNftCollectionsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXNftCollection>>> GetNftCollectionsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetNftDepositsAsync  

[https://docs.ftx.com/#get-nft-deposits](https://docs.ftx.com/#get-nft-deposits)  
<p>

*Get NFT deposits*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.GetNftDepositsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXNftDeposit>>> GetNftDepositsAsync(DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetNftGalleryAsync  

[https://docs.ftx.com/#get-nft-gallery](https://docs.ftx.com/#get-nft-gallery)  
<p>

*Get NFT gallery*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.GetNftGalleryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXNftGallery>> GetNftGalleryAsync(long galleryId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|galleryId|Id of the gallery|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetNftsAsync  

[https://docs.ftx.com/#list-nfts](https://docs.ftx.com/#list-nfts)  
<p>

*Get list of NFTs*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.GetNftsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXNft>>> GetNftsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetNftTradesAsync  

[https://docs.ftx.com/#get-nft-trades](https://docs.ftx.com/#get-nft-trades)  
<p>

*Get info on the trades of a NFT*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.GetNftTradesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXNftTrade>>> GetNftTradesAsync(long nftId, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|nftId|Id of the NFT|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetNftUserInfoAsync  

[https://docs.ftx.com/#get-nft-account-info](https://docs.ftx.com/#get-nft-account-info)  
<p>

*Get details on a NFT for the user*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.GetNftUserInfoAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<NFTUserInfo>> GetNftUserInfoAsync(long nftId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|nftId|NFT id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetNftUserTradesAsync  

[https://docs.ftx.com/#get-nft-fills](https://docs.ftx.com/#get-nft-fills)  
<p>

*Get NFT trades*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.GetNftUserTradesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXNftUserTrade>>> GetNftUserTradesAsync(DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetNftWithdrawalsAsync  

[https://docs.ftx.com/#get-nft-withdrawals](https://docs.ftx.com/#get-nft-withdrawals)  
<p>

*Get NFT withdrawals*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.GetNftWithdrawalsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<FTXNftWithdrawal>>> GetNftWithdrawalsAsync(DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceBidAsync  

[https://docs.ftx.com/#place-bid](https://docs.ftx.com/#place-bid)  
<p>

*Place a bid on an NFT auction*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.PlaceBidAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXNft>> PlaceBidAsync(long nftId, decimal price, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|nftId|NFT id|
|price|Bid price|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RedeemNftAsync  

[https://docs.ftx.com/#redeem-nft](https://docs.ftx.com/#redeem-nft)  
<p>

*Redeem a NFT*  

```csharp  
var client = new FTXClient();  
var result = await client.GeneralApi.Nft.RedeemNftAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<FTXNftRedeem>> RedeemNftAsync(long nftId, string address, string? notes = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|nftId|NFT id to redeem|
|address|Address to redeem to|
|_[Optional]_ notes|Notes|
|_[Optional]_ ct|Cancellation token|

</p>
