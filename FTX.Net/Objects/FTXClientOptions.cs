using CryptoExchange.Net.Objects;
using System;
using FTX.Net.Interfaces.Clients;

namespace FTX.Net.Objects
{
    /// <summary>
    /// Options for the FTX client
    /// </summary>
    public class FTXClientOptions : BaseRestClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static FTXClientOptions Default { get; set; } = new FTXClientOptions();

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

        private RestApiClientOptions _apiOptions = new RestApiClientOptions(FTXApiAddresses.Default.RestClientAddress);
        /// <summary>
        /// Api options
        /// </summary>
        public RestApiClientOptions ApiOptions
        {
            get => _apiOptions;
            set => _apiOptions = new RestApiClientOptions(_apiOptions, value);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public FTXClientOptions() : this(Default)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn">Base the new options on other options</param>
        internal FTXClientOptions(FTXClientOptions baseOn) : base(baseOn)
        {
            if (baseOn == null)
                return;

            AffiliateCode = baseOn.AffiliateCode;
            AutoTimestamp = baseOn.AutoTimestamp;
            AutoTimestampRecalculationInterval = baseOn.AutoTimestampRecalculationInterval;
            SubaccountName = baseOn.SubaccountName;
            _apiOptions = new RestApiClientOptions(baseOn.ApiOptions, null);
        }
    }

    /// <summary>
    /// Options for the FTX socket client
    /// </summary>
    public class FTXSocketClientOptions: BaseSocketClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static FTXSocketClientOptions Default { get; set; } = new FTXSocketClientOptions()
        {
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// Bind this client to a subaccount. All private subscriptions (orders/fills etc) will be for the bound subaccount instead of the main account.
        /// </summary>
        public string? SubaccountName { get; set; }

        private ApiClientOptions _streamOptions = new RestApiClientOptions(FTXApiAddresses.Default.SocketClientAddress);
        /// <summary>
        /// Stream options
        /// </summary>
        public ApiClientOptions StreamOptions
        {
            get => _streamOptions;
            set => _streamOptions = new ApiClientOptions(_streamOptions, value);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public FTXSocketClientOptions() : this(Default)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn">Base the new options on other options</param>
        internal FTXSocketClientOptions(FTXSocketClientOptions baseOn) : base(baseOn)
        {
            if (baseOn == null)
                return;

            SubaccountName = baseOn.SubaccountName;
            _streamOptions = new ApiClientOptions(baseOn.StreamOptions, null);
        }
    }

    /// <summary>
    /// Options the FTX symbol order book
    /// </summary>
    public class FTXSymbolOrderBookOptions: OrderBookOptions
    {

        /// <summary>
        /// After how much time we should consider the connection dropped if no data is received for this time after the initial subscriptions
        /// </summary>
        public TimeSpan? InitialDataTimeout { get; set; }

        /// <summary>
        /// Grouping of the order book entries
        /// </summary>
        public int? Grouping { get; set; }

        /// <summary>
        /// Client to use for connecting
        /// </summary>
        public IFTXSocketClient? Client { get; set; }
    }
}
