using CryptoExchange.Net.Objects;

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
