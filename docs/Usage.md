---
title: Getting started
nav_order: 2
---

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
More info on the specific options can be found in the [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Options.html)

### Dependency injection
See [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Clients.html#dependency-injection)