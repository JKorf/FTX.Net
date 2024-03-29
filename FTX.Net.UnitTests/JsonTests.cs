﻿using FTX.Net.Objects;
using FTX.Net.Testing;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using FTX.Net.Interfaces.Clients;

namespace FTX.Net.UnitTests
{
    [TestFixture]
    public class JsonTests
    {
        private JsonToObjectComparer<IFTXClient> _comparer = new JsonToObjectComparer<IFTXClient>((json) => TestHelpers.CreateResponseClient(json, new FTXClientOptions()
        {
            ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "123"),
            AutoTimestamp = false,
            OutputOriginalData = true,
            ApiOptions = new CryptoExchange.Net.Objects.RestApiClientOptions
            {
                RateLimiters = new List<IRateLimiter>()
            }
        }, System.Net.HttpStatusCode.OK));

        [Test]
        public async Task ValidateAccountCalls()
        {   
            await _comparer.ProcessSubject("Account", c => c.TradeApi.Account,
                useNestedJsonPropertyForAllCompare: new List<string> { "result" },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                }
                );
        }

        [Test]
        public async Task ValidateTradingCalls()
        {
            await _comparer.ProcessSubject("Trading", c => c.TradeApi.Trading,
                useNestedJsonPropertyForAllCompare: new List<string> { "result" },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                }
                );
        }

        [Test]
        public async Task ValidateExchangeDataCalls()
        {
            await _comparer.ProcessSubject("ExchangeData", c => c.TradeApi.ExchangeData,
                useNestedJsonPropertyForAllCompare: new List<string> { "result" },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                    { "GetExpiredFuturesAsync", new List<string>{ "moveStart" } }
                }
                );
        }

        [Test]
        public async Task ValidateConvertCalls()
        {
            await _comparer.ProcessSubject("Convert", c => c.GeneralApi.Convert,
                useNestedJsonPropertyForAllCompare: new List<string> { "result" },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                }
                );
        }

        [Test]
        public async Task ValidateNFTCalls()
        {
            await _comparer.ProcessSubject("NFT", c => c.GeneralApi.NFT,
                useNestedJsonPropertyForAllCompare: new List<string> { "result" },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                }
                );
        }

        [Test]
        public async Task ValidateStakingCalls()
        {
            await _comparer.ProcessSubject("Staking", c => c.GeneralApi.Staking,
                useNestedJsonPropertyForAllCompare: new List<string> { "result" },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                }
                );
        }

        [Test]
        public async Task ValidateSubaccountsCalls()
        {
            await _comparer.ProcessSubject("Subaccounts", c => c.GeneralApi.Subaccounts,
                useNestedJsonPropertyForAllCompare: new List<string> { "result" },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                }
                );
        }
    }
}
