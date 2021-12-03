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

        private RestApiClientOptions _apiOptions = new RestApiClientOptions("https://ftx.com/api");
        /// <summary>
        /// Api options
        /// </summary>
        public RestApiClientOptions ApiOptions
        {
            get => _apiOptions;
            set => _apiOptions.Copy(_apiOptions, value);
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public FTXClientOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }

        /// <summary>
        /// Copy the values of the def to the input
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="def"></param>
        public new void Copy<T>(T input, T def) where T : FTXClientOptions
        {
            base.Copy(input, def);

            input.AffiliateCode = def.AffiliateCode;
            input.AutoTimestamp = def.AutoTimestamp;
            input.AutoTimestampRecalculationInterval = def.AutoTimestampRecalculationInterval;
            input.SubaccountName = def.SubaccountName;
            input.ApiOptions = new RestApiClientOptions(def.ApiOptions);
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

        private ApiClientOptions _streamOptions = new RestApiClientOptions("wss://ftx.com/ws/");
        /// <summary>
        /// Stream options
        /// </summary>
        public ApiClientOptions StreamOptions
        {
            get => _streamOptions;
            set => _streamOptions.Copy(_streamOptions, value);
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public FTXSocketClientOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }

        /// <summary>
        /// Copy the values of the def to the input
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="def"></param>
        public new void Copy<T>(T input, T def) where T : FTXSocketClientOptions
        {
            base.Copy(input, def);

            input.SubaccountName = def.SubaccountName;
            input.StreamOptions = new ApiClientOptions(def.StreamOptions);
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
        public FTXSymbolOrderBookOptions(IFTXSocketClient? client = null, int? grouping = null)
        {
            Client = client;
            Grouping = grouping;
        }
    }
}
