using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net
{
    public class FTXClientOptions : RestClientOptions
    {
        public FTXClientOptions(): base("https://ftx.com/api")
        {

        }
    }

    public class FTXSocketClientOptions: SocketClientOptions
    {
        public FTXSocketClientOptions(): base("wss://ftx.com/ws/")
        {
            SocketSubscriptionsCombineTarget = 10;
        }
    }
}
