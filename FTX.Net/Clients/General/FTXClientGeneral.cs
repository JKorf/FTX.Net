using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using FTX.Net.Clients.Rest;
using FTX.Net.Interfaces.Clients.General;
using FTX.Net.Interfaces.Clients.Rest;
using FTX.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FTX.Net.Clients.General
{
    public class FTXClientGeneral: RestSubClient, IFTXClientGeneral
    {
        private readonly FTXClient _baseClient;

        /// <inheritdoc />
        public IFTXClientGeneralConvert Convert { get; }
        /// <inheritdoc />
        public IFTXClientGeneralStaking Staking { get; }
        /// <inheritdoc />
        public IFTXClientGeneralMargin Margin { get; }
        /// <inheritdoc />
        public IFTXClientGeneralNft NFT { get; }
        /// <inheritdoc />
        public IFTXClientGeneralPay FTXPay { get; }
        /// <inheritdoc />
        public IFTXClientGeneralSubaccounts Subaccounts { get; }

        public FTXClientGeneral(FTXClient baseClient, FTXClientOptions options) :
            base(options.OptionsMarket, options.OptionsMarket.ApiCredentials == null ? null : new FTXAuthenticationProvider(options.OptionsMarket.ApiCredentials))
        {
            _baseClient = baseClient;

            Convert = new FTXClientGeneralConvert(this);
            Margin = new FTXClientGeneralMargin(this);
            Staking = new FTXClientGeneralStaking(this);
            NFT = new FTXClientGeneralNFT(this);
            FTXPay = new FTXClientGeneralPay(this);
            Subaccounts = new FTXClientGeneralSubaccounts(this);
        }

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
    }
}
