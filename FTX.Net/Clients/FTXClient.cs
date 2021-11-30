using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;
using FTX.Net.Clients.General;
using FTX.Net.Clients.Market;
using FTX.Net.Enums;
using FTX.Net.Interfaces.Clients.General;
using FTX.Net.Interfaces.Clients.Market;
using FTX.Net.Interfaces.Clients.Rest;
using FTX.Net.Objects;
using FTX.Net.Objects.Internal;
using FTX.Net.Objects.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FTX.Net.Clients.Rest
{
    /// <summary>
    /// Client for interacting with the FTX API
    /// </summary>
    public class FTXClient : RestClient, IFTXClient
    {
        private const string SubaccountHeaderName = "FTX-SUBACCOUNT";

        public IFTXClientGeneral General { get; }
        public IFTXClientMarket Market { get; }

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of FTXClient using the default options
        /// </summary>
        public FTXClient() : this(Objects.FTXClientOptions.Default)
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

            General = new FTXClientGeneral(this, options);
            Market = new FTXClientMarket(this, options);
        }
        #endregion

        #region methods
        internal static Dictionary<string, string>? GetSubaccountHeader(string? subaccountName) => subaccountName == null ? null : new Dictionary<string, string>
            {
                { SubaccountHeaderName, WebUtility.UrlEncode(subaccountName) }
            };

        #region private

        internal async Task<WebCallResult<T>> SendFTXRequest<T>(RestSubClient subClient, Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null, ArrayParametersSerialization? arraySerialization = null, int credits = 1, JsonSerializer? deserializer = null, Dictionary<string, string>? additionalHeaders = null)
        {
            if (signed)
                await FTXTimestampProvider.UpdateTimeAsync(this, log, (FTXClientOptions)ClientOptions).ConfigureAwait(false);

            var result = await SendRequestAsync<FTXResult<T>>(subClient, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, credits, deserializer, additionalHeaders).ConfigureAwait(false);
            if (result)
                return result.As(result.Data.Result);

            return WebCallResult<T>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);
        }

        internal async Task<WebCallResult> SendFTXRequest(RestSubClient subClient, Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null, ArrayParametersSerialization? arraySerialization = null, int credits = 1, JsonSerializer? deserializer = null, Dictionary<string, string>? additionalHeaders = null)
        {
            var result = await SendRequestAsync<FTXResult>(subClient, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, credits, deserializer, additionalHeaders).ConfigureAwait(false);
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

        public override void Dispose()
        {
            Market.Dispose();
            General.Dispose();
            base.Dispose();
        }
        #endregion
    }
}
