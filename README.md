# FTX.Net
[![.NET](https://github.com/JKorf/FTX.Net/actions/workflows/dotnet.yml/badge.svg)](https://github.com/JKorf/FTX.Net/actions/workflows/dotnet.yml) ![Nuget version](https://img.shields.io/nuget/v/FTX.net.svg)  ![Nuget downloads](https://img.shields.io/nuget/dt/FTX.Net.svg)
 
FTX.Net is a wrapper around the FTX API as described on [FTX](https://docs.ftx.com/), including all features the API provides using clear and readable objects, both for the REST  as the websocket API's.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/JKorf/FTX.Net/issues)**

[Documentation](https://jkorf.github.io/FTX.Net/)

## Donate / Sponsor
I develop and maintain this package on my own for free in my spare time. Donations are greatly appreciated. If you prefer to donate any other currency please contact me.

**Btc**:  12KwZk3r2Y3JZ2uMULcjqqBvXmpDwjhhQS  
**Eth**:  0x069176ca1a4b1d6e0b7901a6bc0dbf3bb0bf5cc2  
**Nano**: xrb_1ocs3hbp561ef76eoctjwg85w5ugr8wgimkj8mfhoyqbx4s1pbc74zggw7gs  

Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf)  

## Discord
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). Feel free to join for discussion and/or questions around the CryptoExchange.Net and implementation libraries.

## Release notes
* Version 1.0.12 - 12 Jun 2022
    * Fixed subaccount header not properly set for US api
    * Updated CrytpoExchange.Net

* Version 1.0.11 - 24 May 2022
    * Updated CryptoExchange.Net

* Version 1.0.10 - 22 May 2022
    * Updated CryptoExchange.Net

* Version 1.0.9 - 08 May 2022
    * Updated CryptoExchange.Net

* Version 1.0.8 - 01 May 2022
    * Updated CryptoExchange.Net which fixed an timing related issue in the websocket reconnection logic
    * Added seconds representation to KlineInterval enum
    * Fixed Deposit deserialization when confirmationTime is null

* Version 1.0.7 - 14 Apr 2022
    * Updated CryptoExchange.Net

* Version 1.0.6 - 10 Mar 2022
    * Updated CryptoExchange.Net

* Version 1.0.5 - 08 Mar 2022
    * Fixed Convert.AcceptQuoteAsync endpoint
    * Updated CryptoExchange.Net

* Version 1.0.4 - 01 Mar 2022
    * Updated CryptoExchange.Net improving the websocket reconnection robustness

* Version 1.0.3 - 27 Feb 2022
    * Updated CryptoExchange.Net to fix timestamping issue when request is ratelimiter

* Version 1.0.2 - 24 Feb 2022
    * Added missing Completed deposit status
    * Fixed deserialization error on FTXPosition if cumulative properties are null
    * Updated CryptoExchange.Net

* Version 1.0.1 - 21 Feb 2022
    * Added network parameter for GetWithdrawalFeesAsync endpoint

* Version 1.0.0 - 18 Feb 2022
	* Added Github.io page for documentation: https://jkorf.github.io/FTX.Net/
	* Added unit tests for parsing the returned JSON for each endpoint and subscription
	* Added AddFTX extension method on IServiceCollection for easy dependency injection
	* Added URL reference to API endpoint documentation for each endpoint

	* Refactored client structure to be consistent across exchange implementations
	* Renamed various properties to be consistent across exchange implementations

	* Cleaned up project structure
	* Fixed various models

	* Updated CryptoExchange.Net, see https://github.com/JKorf/CryptoExchange.Net#release-notes
	* See https://jkorf.github.io/FTX.Net/MigrationGuide.html for additional notes for updating from V0 to V1

* Version 0.1.8 - 03 Nov 2021
    * Fix for ftx.us authentication
    * Added AutoTimestamp option to prevent authentication issues because of clock drift
    * Fixed GetBorrowRatesAsync endpoint

* Version 0.1.7 - 11 Oct 2021
    * Fixed nullable properties on FTXPosition model

* Version 0.1.6 - 11 Oct 2021
    * Fixed deserialization trigger order type

* Version 0.1.5 - 08 Oct 2021
    * Updated CryptoExchange.Net to fix some socket issues

* Version 0.1.4 - 06 Oct 2021
    * Updated CryptoExchange.Net, fixing socket issue when calling from .Net Framework
    * Fix for order book syncing

* Version 0.1.3 - 05 Oct 2021
    * Fixed incorrect Subscription response check
    * Fixed GetFundingRatesAsync interface
    * Added missing properties on FTXPosition model
    * Fixed deserialization issue on FTXMarginMarketInfo model

* Version 0.1.2 - 29 Sep 2021
    * Updated CryptoExchange.Net

* Version 0.1.1 - 27 Sep 2021
    * Fixed start/endtime filters
    * Added futures endpoints

* Version 0.1.0 - 20 Sep 2021
    * Added missing SetApiCredentials methods
    * Updated CryptoExchange.Net

* Version 0.0.5 - 17 Sep 2021
    * Fix deserialization issue GetSymbolsAsync because Last property returning null

* Version 0.0.4 - 15 Sep 2021
    * Added support for subaccount requests/subscriptions
    * Updated CryptoExchange.Net
    * Fixed checksum issue FTXSymbolOrderBook

* Version 0.0.3 - 09 Sep 2021
    * Updated stream ticker model
    * Added Topic for socket subscriptions

* Version 0.0.2 - 05 Sep 2021
    * Fixed multiple calls not being passed the input parameters

* Version 0.0.1 - 03 Sep 2021
    * Fixed placing market order

* Version 0.0.1-beta9 - 02 Sep 2021
    * Fix for position deserialization

* Version 0.0.1-beta8 - 02 Sep 2021
    * Fix for disposing order book closing socket even if there are other connections

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