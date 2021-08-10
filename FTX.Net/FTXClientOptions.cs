using CryptoExchange.Net.Objects;

namespace FTX.Net
{
    public class FTXClientOptions : RestClientOptions
    {
        public string AffiliateCode { get; set; } = "jkorf-net";

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
