using FTX.Net.Interfaces;
using FTX.Net.Objects;
using FTX.Net.Testing;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FTX.Net.UnitTests
{
    [TestFixture]
    public class JsonTests
    {
        private JsonToObjectComparer<IFTXClient> _comparer = new JsonToObjectComparer<IFTXClient>((json) => TestHelpers.CreateResponseClient(json, new FTXClientOptions()
        { ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "123"), OutputOriginalData = true }, System.Net.HttpStatusCode.OK));

        [Test]
        public async Task ValidateBaseCalls()
        {   
            await _comparer.ProcessSubject("Base", c => c,
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
            await _comparer.ProcessSubject("Convert", c => c.Convert,
                useNestedJsonPropertyForAllCompare: new List<string> { "result" },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                }
                );
        }

        [Test]
        public async Task ValidateLeveragedTokensCalls()
        {
            await _comparer.ProcessSubject("LeveragedTokens", c => c.LeveragedTokens,
                useNestedJsonPropertyForAllCompare: new List<string> { "result" },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                }
                );
        }

        [Test]
        public async Task ValidateNFTCalls()
        {
            await _comparer.ProcessSubject("NFT", c => c.NFT,
                useNestedJsonPropertyForAllCompare: new List<string> { "result" },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                }
                );
        }

        [Test]
        public async Task ValidateOptionCalls()
        {
            await _comparer.ProcessSubject("Options", c => c.Options,
                useNestedJsonPropertyForAllCompare: new List<string> { "result" },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                }
                );
        }

        [Test]
        public async Task ValidateStakingCalls()
        {
            await _comparer.ProcessSubject("Staking", c => c.Staking,
                useNestedJsonPropertyForAllCompare: new List<string> { "result" },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                }
                );
        }

        [Test]
        public async Task ValidateSubaccountsCalls()
        {
            await _comparer.ProcessSubject("Subaccounts", c => c.Subaccounts,
                useNestedJsonPropertyForAllCompare: new List<string> { "result" },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                }
                );
        }
    }
}
