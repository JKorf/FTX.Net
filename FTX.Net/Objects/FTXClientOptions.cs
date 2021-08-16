using CryptoExchange.Net.Objects;

namespace FTX.Net.Objects
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

    public class FTXSymbolOrderBookOptions: OrderBookOptions
    {
        public int? Grouping { get; set; } = null;

        public FTXSymbolOrderBookOptions(int? grouping = null): base("FTX", false, false)
        {
            if (grouping.HasValue)
                Grouping = grouping.Value;
        }
    }
}
