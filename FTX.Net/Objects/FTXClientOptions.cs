using CryptoExchange.Net.Objects;
using System.Net.Http;
using FTX.Net.Interfaces;
using System;

namespace FTX.Net.Objects
{
    /// <summary>
    /// Options for the FTX client
    /// </summary>
    public class FTXClientOptions : RestClientOptions
    {
        /// <summary>
        /// Whether or not to automatically sync the local time with the server time
        /// </summary>
        public bool AutoTimestamp { get; set; } = true;

        /// <summary>
        /// Interval for refreshing the auto timestamp calculation
        /// </summary>
        public TimeSpan AutoTimestampRecalculationInterval { get; set; } = TimeSpan.FromHours(3);

        /// <summary>
        /// Affiliate code which will be sent when placing orders
        /// </summary>
        public string AffiliateCode { get; set; } = "jkorf-net";

        /// <summary>
        /// Bind this client to a subaccount. All requests send from this account will be associated with the provided sub account
        /// </summary>
        public string? SubaccountName { get; set; }

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
            copy.SubaccountName = SubaccountName;
            return copy;
        }
    }

    /// <summary>
    /// Options for the FTX socket client
    /// </summary>
    public class FTXSocketClientOptions: SocketClientOptions
    {
        /// <summary>
        /// Bind this client to a subaccount. All private subscriptions (orders/fills etc) will be for the bound subaccount instead of the main account.
        /// </summary>
        public string? SubaccountName { get; set; }

        /// <summary>
        /// Create new client options
        /// </summary>
        public FTXSocketClientOptions() : this(null)
        { 
        }

        /// <summary>
        /// Create new client options
        /// </summary>
        /// <param name="subaccountName">Name of the subaccount to subscribe private endpoints for. Null for master account</param>
        public FTXSocketClientOptions(string? subaccountName = null) : base("wss://ftx.com/ws/")
        {
            SubaccountName = subaccountName;
            SocketSubscriptionsCombineTarget = 10;
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <returns></returns>
        public FTXSocketClientOptions Copy()
        {
            var copy = Copy<FTXSocketClientOptions>();
            copy.SubaccountName = SubaccountName;
            return copy;
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
