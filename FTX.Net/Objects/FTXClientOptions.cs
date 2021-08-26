using CryptoExchange.Net.Objects;
using System.Net.Http;
using FTX.Net.Interfaces;

namespace FTX.Net.Objects
{
    /// <summary>
    /// Options for the FTX client
    /// </summary>
    public class FTXClientOptions : RestClientOptions
    {
        /// <summary>
        /// Affiliate code which will be sent when placing orders
        /// </summary>
        public string AffiliateCode { get; set; } = "jkorf-net";


        /// <summary>
        /// Create new client options
        /// </summary>
        public FTXClientOptions(): base("https://ftx.com/api")
        {
        }

        /// <summary>
        /// Create new client options
        /// </summary>
        /// <param name="httpClient">HttpClient to use for requests from this client</param>
        public FTXClientOptions(HttpClient httpClient) : base(httpClient, "https://ftx.com/api")
        {
        }

        /// <summary>
        /// Create new client options
        /// </summary>
        /// <param name="apiAddress">Custom API address to use</param>
        public FTXClientOptions(string apiAddress) : base(apiAddress)
        {
        }

        /// <summary>
        /// Create new client options
        /// </summary>
        /// <param name="httpClient">HttpClient to use for requests from this client</param>
        /// <param name="apiAddress">Custom API address to use</param>
        public FTXClientOptions(string apiAddress, HttpClient httpClient) : base(httpClient, apiAddress)
        {
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <returns></returns>
        public FTXClientOptions Copy()
        {
            var copy = Copy<FTXClientOptions>();
            copy.AffiliateCode = AffiliateCode;
            return copy;
        }
    }

    /// <summary>
    /// Options for the FTX socket client
    /// </summary>
    public class FTXSocketClientOptions: SocketClientOptions
    {
        /// <summary>
        /// Create new client options
        /// </summary>
        public FTXSocketClientOptions(): base("wss://ftx.com/ws/")
        {
            SocketSubscriptionsCombineTarget = 10;
        }
    }

    /// <summary>
    /// Options the FTX symbol order book
    /// </summary>
    public class FTXSymbolOrderBookOptions: OrderBookOptions
    {
        /// <summary>
        /// Grouping of the order book entries
        /// </summary>
        public int? Grouping { get; set; }

        /// <summary>
        /// Client to use for connecting
        /// </summary>
        public IFTXSocketClient? Client { get; set; }

        /// <summary>
        /// Create new book options
        /// </summary>
        /// <param name="grouping">Grouping of the order book entries</param>
        /// <param name="client">Client to use for connecting</param>
        public FTXSymbolOrderBookOptions(IFTXSocketClient? client = null, int? grouping = null): base("FTX", false, false)
        {
            Client = client;
            Grouping = grouping;
        }
    }
}
