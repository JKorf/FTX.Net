---
title: IFTXClientTradeApi
has_children: true
parent: Rest API documentation
---
*[generated documentation]*  
`FTXClient > TradeApi`  
*Trade endpoints*
  
***
*Get the ISpotClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.*  
**ISpotClient CommonSpotClient { get; }**  
***
*Get the ISpotClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.*  
**IFuturesClient CommonFuturesClient { get; }**  
***
*Endpoints related to account settings, info or actions*  
**[IFTXClientTradeApiAccount](IFTXClientTradeApiAccount.html) Account { get; }**  
***
*Endpoints related to retrieving market and system data*  
**[IFTXClientTradeApiExchangeData](IFTXClientTradeApiExchangeData.html) ExchangeData { get; }**  
***
*Endpoints related to orders and trades*  
**[IFTXClientTradeApiTrading](IFTXClientTradeApiTrading.html) Trading { get; }**  
