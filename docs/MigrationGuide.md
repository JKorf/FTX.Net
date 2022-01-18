There are a decent amount of breaking changes when moving from version 6.x.x to version 7.x.x. Although the interface has changed, the available endpoints/information have not, so there should be no need to completely rewrite your program.
Most endpoints are now available under a slightly different name or path, and most data models have remained the same, barring a few renames.
In this document most changes will be described. If you have any other questions or issues when updating, feel free to open an issue.

Changes related to `IExchangeClient`, options and client structure are also (partially) covered in the [CryptoExchange.Net Migration Guide](https://github.com/JKorf/CryptoExchange.Net/wiki/Migration-Guide)

### Namespaces
There are a few namespace changes:  
|Type|Old|New|
|----|---|---|
|Clients|`FTX.Net`|`FTX.Net.Clients`  |
|Client interfaces|`FTX.Net.Interfaces`|`FTX.Net.Interfaces.Clients`  |
|Objects|`FTX.Net.Objects`|`FTX.Net.Objects.Models`  |

### Client options
The `BaseAddress` and rate limiting options are now under the `SpotApiOptions`.  
*V0*
````C#
var ftxClient = new FTXClient(new FTXClientOptions
{
	ApiCredentials = new ApiCredentials("API-KEY", "API-SECRET"),
	BaseAddress = "ADDRESS",
	RateLimitingBehaviour = RateLimitingBehaviour.Fail
});
````

*V1*
````C#
var ftxClient = new FTXClient(new FTXClientOptions
{
	ApiCredentials = new ApiCredentials("API-KEY", "API-SECRET"),
	SpotApiOptions = new RestApiClientOptions
	{
		BaseAddress = "ADDRESS",
		RateLimitingBehaviour = RateLimitingBehaviour.Fail
	}
});
````

### Client structure
Version 1 adds the `TradeApi` and `GeneralApi` Api clients under the `FTXClient`, and a topic underneath that. This is done to keep the same client structure as other exchange implementations, more info on this [here](https://github.com/Jkorf/CryptoExchange.Net/wiki/Clients).
In the `FTXSocketClient` a `Streams` Api client is added. This means all calls will have changed, though most will only need to add `TradeApi.[Topic].XXX`/`Streams.XXX`:

*V0*
````C#
var balances = await ftxClient.GetBalancesAsync();
var withdrawals = await ftxClient.GetWithdrawalHistoryAsync();

var tradeHistory = await ftxClient.GetTradeHistoryAsync("BTC/USD");
var symbols = await ftxClient.GetSymbolsAsync();

var order = await ftxClient.PlaceOrderAsync();
var trades = await ftxClient.GetUserTradesAsync();

var sub = ftxSocketClient.SubscribeToTickerUpdatesAsync("BTC/USD", DataHandler);
````

*V1*  
````C#
var balances = await ftxClient.TradeApi.Account.GetBalancesAsync();
var withdrawals = await ftxClient.TradeApi.Account.GetWithdrawalHistoryAsync();

var tradeHistory = await ftxClient.TradeApi.ExchangeData.GetTradeHistoryAsync("BTC/USD");
var symbols = await ftxClient.TradeApi.ExchangeData.GetSymbolsAsync();

var order = await ftxClient.TradeApi.Trading.PlaceOrderAsync();
var trades = await ftxClient.TradeApi.Trading.GetUserTradesAsync();

var sub = ftxSocketClient.Streams.SubscribeToTickerUpdatesAsync("BTC/USD", DataHandler);
````

### Definitions
Some names have been changed to a common definition. This includes where the name is part of a bigger name  
|Old|New||
|----|---|---|
|`asset.Id`|`asset.Name`||
|`StartTime/Open/High/Low/Close`|`OpenTime/OpenPrice/HighPrice/LowPrice/ClosePrice`||
|`BestAsk/BestBid`|`BestAskPrice/BestBidPrice`||
|`Cancelled`|`Canceled`||
|`Coin`|`Asset`||

Some names have slightly changed to be consistent across different libraries   
`FilledQuantity` -> `QuantityFilled`  
`balance.Free` -> `balance.Available`  
