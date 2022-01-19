---
title: Examples
nav_order: 3
---

## Basic operations
Make sure to read the [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Clients.html#processing-request-responses) on processing responses.

### Get market data
```csharp
// Getting info on all symbols
var symbolData = await ftxClient.TradeApi.ExchangeData.GetSymbolsAsync();

// Getting the order book of a symbol
var orderBookData = await ftxClient.TradeApi.ExchangeData.GetOrderBookAsync("BTC/USD", 25);

// Getting recent trades of a symbol
var tradeHistoryData = await ftxClient.TradeApi.ExchangeData.GetTradeHistoryAsync("BTC/USD");
```

### Requesting balances
```csharp
var accountData = await ftxClient.TradeApi.Account.GetBalancesAsync();
```
### Placing order
```csharp
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
```

### Requesting a specific order
```csharp
// Request info on order with id `1234`
var orderData = await ftxClient.TradeApi.Trading.GetOrderAsync(1234);
```

### Requesting order history
```csharp
// Get all orders conform the parameters
 var ordersData = await ftxClient.TradeApi.Trading.GetOrdersAsync();
```

### Cancel order
```csharp
// Cancel order with id `1234`
var orderData = await ftxClient.TradeApi.Trading.CancelOrderAsync(1234);
```

### Get user trades
```csharp
var userTradesResult = await ftxClient.TradeApi.Trading.GetUserTradesAsync();
```

### Subscribing to market data updates
```csharp
var subscribeResult = await ftxSocketClient.Streams.SubscribeToTickerUpdatesAsync("BTC/USD", data => 
{
	// Handle ticker data
});
```

### Subscribing to order updates
```csharp
var subscribeResult = await ftxSocketClient.Streams.SubscribeToOrderUpdatesAsync(data => 
	data =>
	{
	  // Handle order updates
	});
```
