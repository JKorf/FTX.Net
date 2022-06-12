using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using FTX.Net.Clients.TradeApi;
using FTX.Net.Interfaces.Clients.GeneralApi;
using FTX.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FTX.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    public class FTXClientGeneralApi : RestApiClient, IFTXClientGeneralApi
    {
        private readonly FTXClient _baseClient;
        private readonly FTXClientOptions _options;
        private readonly Log _log;

        /// <inheritdoc />
        public IFTXClientGeneralApiConvert Convert { get; }
        /// <inheritdoc />
        public IFTXClientGeneralApiStaking Staking { get; }
        /// <inheritdoc />
        public IFTXClientGeneralApiMargin Margin { get; }
        /// <inheritdoc />
        public IFTXClientGeneralApiNft NFT { get; }
        /// <inheritdoc />
        public IFTXClientGeneralApiPay FTXPay { get; }
        /// <inheritdoc />
        public IFTXClientGeneralApiSubaccounts Subaccounts { get; }

        internal FTXClientGeneralApi(Log log, FTXClient baseClient, FTXClientOptions options) :
            base(options, options.ApiOptions)
        {
            _baseClient = baseClient;
            _options = options;
            _log = log;

            Convert = new FTXClientGeneralApiConvert(this);
            Margin = new FTXClientGeneralApiMargin(this);
            Staking = new FTXClientGeneralApiStaking(this);
            NFT = new FTXClientGeneralApiNFT(this);
            FTXPay = new FTXClientGeneralApiPay(this);
            Subaccounts = new FTXClientGeneralApiSubaccounts(this);

            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InBody;
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new FTXAuthenticationProvider(credentials);

        internal Task<WebCallResult<T>> SendFTXRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null, ArrayParametersSerialization? arraySerialization = null, int credits = 1, JsonSerializer? deserializer = null, Dictionary<string, string>? additionalHeaders = null)
         => _baseClient.SendFTXRequest<T>(this, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, credits, deserializer, additionalHeaders);

        internal Task<WebCallResult> SendFTXRequest(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null, ArrayParametersSerialization? arraySerialization = null, int credits = 1, JsonSerializer? deserializer = null, Dictionary<string, string>? additionalHeaders = null)
         => _baseClient.SendFTXRequest(this, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, credits, deserializer, additionalHeaders);

        internal CallResult<T> DeserializeInternal<T>(string data)
        {
            return _baseClient.DeserializeInternal<T>(data);
        }

        internal Uri GetUri(string path)
        {
            return new Uri(BaseAddress.AppendPath(path));
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => _baseClient.TradeApi.ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, _options.ApiOptions.AutoTimestamp, _options.ApiOptions.TimestampRecalculationInterval, FTXClientTradeApi.TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan GetTimeOffset()
            => FTXClientTradeApi.TimeSyncState.TimeOffset;
    }
}
