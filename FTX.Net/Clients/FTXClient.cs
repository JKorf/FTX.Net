using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using FTX.Net.Clients.GeneralApi;
using FTX.Net.Clients.TradeApi;
using FTX.Net.Interfaces.Clients;
using FTX.Net.Interfaces.Clients.GeneralApi;
using FTX.Net.Interfaces.Clients.TradeApi;
using FTX.Net.Objects;
using FTX.Net.Objects.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FTX.Net.Clients
{
    /// <inheritdoc cref="IFTXClient" />
    public class FTXClient : BaseRestClient, IFTXClient
    {
        private const string SubaccountHeaderName = "FTX-SUBACCOUNT";

        /// <inheritdoc />
        public IFTXClientGeneralApi GeneralApi { get; }
        /// <inheritdoc />
        public IFTXClientTradeApi TradeApi { get; }

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of FTXClient using the default options
        /// </summary>
        public FTXClient() : this(FTXClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of FTXClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public FTXClient(FTXClientOptions options) : base("FTX", options)
        {
            if (options == null)
                throw new ArgumentException("Cant pass null options, use empty constructor for default");

            if (!string.IsNullOrEmpty(options.SubaccountName))
            {
                StandardRequestHeaders = new Dictionary<string, string>
                {
                    { SubaccountHeaderName, WebUtility.UrlEncode(options.SubaccountName) }
                };
            }
            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InBody;

            GeneralApi = new FTXClientGeneralApi(this, options);
            TradeApi = new FTXClientTradeApi(this, options);
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(FTXClientOptions options)
        {
            FTXClientOptions.Default = options;
        }

        #region methods
        internal static Dictionary<string, string>? GetSubaccountHeader(string? subaccountName) => subaccountName == null ? null : new Dictionary<string, string>
            {
                { SubaccountHeaderName, WebUtility.UrlEncode(subaccountName) }
            };

        #region private

        internal async Task<WebCallResult<T>> SendFTXRequest<T>(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null, ArrayParametersSerialization? arraySerialization = null, int credits = 1, JsonSerializer? deserializer = null, Dictionary<string, string>? additionalHeaders = null)
        {
            if (signed)
                await FTXTimestampProvider.UpdateTimeAsync(this, log, (FTXClientOptions)ClientOptions).ConfigureAwait(false);

            var result = await SendRequestAsync<FTXResult<T>>(apiClient, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, credits, deserializer, additionalHeaders).ConfigureAwait(false);
            if (result)
                return result.As(result.Data.Result);

            return WebCallResult<T>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);
        }

        internal async Task<WebCallResult> SendFTXRequest(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null, ArrayParametersSerialization? arraySerialization = null, int credits = 1, JsonSerializer? deserializer = null, Dictionary<string, string>? additionalHeaders = null)
        {
            var result = await SendRequestAsync<FTXResult>(apiClient, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, credits, deserializer, additionalHeaders).ConfigureAwait(false);
            if (result)
                return new WebCallResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return WebCallResult.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);
        }

        internal CallResult<T> DeserializeInternal<T>(string data)
        {
            return Deserialize<T>(data);
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(JToken error)
        {
            if (error["error"] == null)
                return new ServerError(error.ToString());

            return new ServerError(error["error"]!.ToString());
        }

        internal static void AddFilter(Dictionary<string, object> parameters, DateTime? startTime, DateTime? endTime)
        {
            parameters.AddOptionalParameter("start_time", DateTimeConverter.ConvertToSeconds(startTime));
            parameters.AddOptionalParameter("end_time", DateTimeConverter.ConvertToSeconds(endTime));
        }


        #endregion

        /// <inheritdoc />
        public override void Dispose()
        {
            TradeApi.Dispose();
            GeneralApi.Dispose();
            base.Dispose();
        }
        #endregion
    }
}
