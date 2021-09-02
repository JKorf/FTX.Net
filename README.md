# FTX.Net
![Build status](https://app.travis-ci.com/JKorf/FTX.Net.svg?token=2PbyJrjvDDpys8nuT4Xb&branch=main) ![Nuget version](https://img.shields.io/nuget/v/FTX.net.svg)  ![Nuget downloads](https://img.shields.io/nuget/dt/FTX.Net.svg)
 
FTX.Net is a wrapper around the FTX API as described on [FTX](https://docs.ftx.com/), including all features the API provides using clear and readable objects, both for the REST  as the websocket API's.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/JKorf/FTX.Net/issues)**

## CryptoExchange.Net
This library is build upon the CryptoExchange.Net library, make sure to check out the documentation on that for basic usage: [docs](https://github.com/JKorf/CryptoExchange.Net)

## Donations
I develop and maintain this package on my own for free in my spare time. Donations are greatly appreciated. If you prefer to donate any other currency please contact me.

**Btc**:  12KwZk3r2Y3JZ2uMULcjqqBvXmpDwjhhQS  
**Eth**:  0x069176ca1a4b1d6e0b7901a6bc0dbf3bb0bf5cc2  
**Nano**: xrb_1ocs3hbp561ef76eoctjwg85w5ugr8wgimkj8mfhoyqbx4s1pbc74zggw7gs  

## Discord
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). Feel free to join for discussion and/or questions around the CryptoExchange.Net and implementation libraries.

## Getting started
Make sure you have installed the FTX.Net [Nuget](https://www.nuget.org/packages/FTX.Net/) package and add `using FTX.Net` to your usings.  You now have access to 2 clients:  
**FTXClient**  
The client to interact with the FTX REST API. Getting prices:
````C#
var client = new FTXClient(new FTXClientOptions(){
 // Specify options for the client
});
var callResult = await client.GetSymbolsAsync();
// Make sure to check if the call was successful
if(!callResult.Success)
{
  // Call failed, check callResult.Error for more info
}
else
{
  // Call succeeded, callResult.Data will have the resulting data
}
````

Placing an order:
````C#
var client = new FTXClient(new FTXClientOptions(){
 // Specify options for the client
 ApiCredentials = new ApiCredentials("Key", "Secret")
});
var callResult = await client.PlaceOrderAsync("ETH/USDT", OrderSide.Buy, OrderType.Limit, quantity:10, price: 50);
// Make sure to check if the call was successful
if(!callResult.Success)
{
  // Call failed, check callResult.Error for more info
}
else
{
  // Call succeeded, callResult.Data will have the resulting data
}
````

The FTXClient has been split in basic usage and some sub-clients for specific parts of the API:
`client.Convert`: General account endpoints  
`client.Options`: Options endpoints  
`client.LeveragedTokens`: Leveraged token endpoints  
`client.Staking`: Staking endpoints  
`client.Margin`: Spot margin endpoints  
`client.NFT`: NFT endpoints  
`client.FTXPay`: FTXPay endpoints  
Other endpoints are available in the FTXClient directly.

**FTXSocketClient**  
The client to interact with the FTX websocket API. Basic usage:
````C#
var client = new FTXSocketClient(new FTXSocketClientOptions()
{
  // Specify options for the client
});
var subscribeResult = client.SubscribeToTickerUpdatesAsync("ETH/USDT", data => {
  // Handle data when it is received
});
// Make sure to check if the subscription was successful
if(!subscribeResult.Success)
{
  // Subscription failed, check callResult.Error for more info
}
else
{
  // Subscription succeeded, the handler will start receiving data when it is available
}
````

## Client options
For the basic client options see also the CryptoExchange.Net [docs](https://github.com/JKorf/CryptoExchange.Net#client-options). 

**FTXClientOptions**
| Property | Description | Default |
| ----------- | ----------- | ---------|
|`AffiliateCode`|Affiliate code which will be sent when placing orders|`jkorf-net`

## Release notes
* Version 0.0.1-beta7 - 02 Sep 2021
    * Added shared base class for FTXOrder and FTXTriggerOrder

* Version 0.0.1-beta6 - 30 Aug 2021
    * Fixed trigger order deserialization

* Version 0.0.1-beta5 - 30 Aug 2021
    * Fixed GetSymbolsAsync deserialization

* Version 0.0.1-beta4 - 27 Aug 2021
    * Fixed market order deserialization

* Version 0.0.1-beta3 - 27 Aug 2021
    * Fixed order book parsing
    * Updated Symbol model

* Version 0.0.1-beta2 - 27 Aug 2021
    * Fixed request signing
    * Added FTXSymbolOrderBook checksum validation


* Version 0.0.1-beta1 - 26 Aug 2021
    * Initial release