using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FTX.Net
{
    internal class FTXAuthenticationProvider : AuthenticationProvider
    {
        public FTXAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, Dictionary<string, object> providedParameters, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, out SortedDictionary<string, object> uriParameters, out SortedDictionary<string, object> bodyParameters, out Dictionary<string, string> headers)
        {
            uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            headers = new Dictionary<string, string>();

            if (!auth)
                return;

            uri = uri.SetParameters(uriParameters, arraySerialization);
            var ftxPrefix = GetFTXHeaderPrefix(uri);
            var timestamp = GetMillisecondTimestamp(apiClient);

            headers.Add($"{ftxPrefix}-KEY", Credentials.Key!.GetString());
            headers.Add($"{ftxPrefix}-TS", timestamp);
            var toSign = timestamp + method + uri.PathAndQuery + (parameterPosition == HttpMethodParameterPosition.InBody ? JsonConvert.SerializeObject(bodyParameters) : "");
            headers.Add($"{ftxPrefix}-SIGN", SignHMACSHA256(toSign).ToLowerInvariant());
        }

        public override string Sign(string toSign) => SignHMACSHA256(toSign).ToLowerInvariant();

        private static string GetFTXHeaderPrefix(Uri requestUri)
            => requestUri.Host.EndsWith(".us", StringComparison.OrdinalIgnoreCase) ? "FTXUS" : "FTX";
    }
}
