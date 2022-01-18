## Creating client
There are 2 clients available to interact with the FTX API, the `FTXClient` and `FTXSocketClient`.

*Create a new rest client*
````C#
var ftxClient = new FTXClient(new FTXClientOptions()
{
	// Set options here for this client
});
````

*Create a new socket client*
````C#
var ftxSocketClient = new FTXSocketClient(new FTXSocketClientOptions()
{
	// Set options here for this client
});
````

Different options are available to set on the clients, see this example
````C#
var ftxClient = new FTXClient(new FTXClientOptions()
{
	ApiCredentials = new ApiCredentials("API-KEY", "API-SECRET"),
	LogLevel = LogLevel.Trace,
	RequestTimeout = TimeSpan.FromSeconds(60)
});
````
Alternatively, options can be provided before creating clients by using `SetDefaultOptions`:
````C#
FTXClient.SetDefaultOptions(new FTXClientOptions{
	// Set options here for all new clients
});
var ftxClient = new FTXClient();
````
More info on the specific options can be found on the [CryptoExchange.Net wiki](https://github.com/JKorf/CryptoExchange.Net/wiki/Options)

### Dependency injection
See [CryptoExchange.Net wiki](https://github.com/JKorf/CryptoExchange.Net/wiki/Clients#dependency-injection)

## Usage
Make sure to read the [CryptoExchange.Net wiki](https://github.com/JKorf/CryptoExchange.Net/wiki/Clients#processing-request-responses) on processing responses.

#### Get market data
````C#
// Getting info on all symbols
var symbolData = await ftxClient.TradeApi.ExchangeData.GetSymbolsAsync();

// Getting the order book of a symbol
var orderBookData = await ftxClient.TradeApi.ExchangeData.GetOrderBookAsync("BTC/USD", 25);

// Getting recent trades of a symbol
var tradeHistoryData = await ftxClient.TradeApi.ExchangeData.GetTradeHistoryAsync("BTC/USD");
````

#### Requesting balances
````C#
var accountData = await ftxClient.TradeApi.Account.GetBalancesAsync();
````
#### Placing order
````C#
// Placing a buy limit order for 0.001 BTC at a price of 50000USDT each
var orderData = await ftxClient.TradeApi.Trading.PlaceOrderAsync(
                "BTC/USD",
                OrderSide.Buy,
                OrderType.Limit,
                0.001m,
                50000);
		
// Placing a market sell order of 0.001 BTC
var orderData = await ftxClient.TradeApi.Trading.PlaceOrderAsync(
                "BTC/USD",
                OrderSide.Sell,
                OrderType.Market,
                0.001m);		
				
													
// Place a stop loss order, place a limit order of 0.001 BTC at 39000USDT each when the last trade price drops below 40000USDT. When not sending `orderPrice` the order will be executed as a market order
var orderData = await ftxClient.TradeApi.Trading.PlaceTriggerOrderAsync(
                "BTC/USD",
                Enums.OrderSide.Buy,
                Enums.TriggerOrderType.Stop,
                0.001m,
                triggerPrice: 40000,
                orderPrice: 39000);
````

#### Requesting a specific order
````C#
// Request info on order with id `1234`
var orderData = await ftxClient.TradeApi.Trading.GetOrderAsync(1234);
````

#### Requesting order history
````C#
// Get all orders conform the parameters
 var ordersData = await ftxClient.TradeApi.Trading.GetOrdersAsync();
````

#### Cancel order
````C#
// Cancel order with id `1234`
var orderData = await ftxClient.TradeApi.Trading.CancelOrderAsync(1234);
````

#### Get user trades
````C#
var userTradesResult = await ftxClient.TradeApi.Trading.GetUserTradesAsync();
````

#### Subscribing to market data updates
````C#
var subscribeResult = await ftxSocketClient.Streams.SubscribeToTickerUpdatesAsync("BTC/USD", data => 
{
	// Handle ticker data
});
````

#### Subscribing to order updates
````C#
var subscribeResult = await ftxSocketClient.Streams.SubscribeToOrderUpdatesAsync(data => 
	data =>
	{
	  // Handle order updates
	});
````
