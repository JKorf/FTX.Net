using Newtonsoft.Json;

namespace FTX.Net.Objects.SocketObjects
{
    internal class LoginRequest : SocketRequest
    {
        [JsonProperty("args")]
        public LoginParams Parameters { get; set; }

        public LoginRequest(string key, string sign, long time, string? subaccount): base("login")
        {
            Parameters = new LoginParams
            {
                Key = key,
                Sign = sign,
                Time = time,
                Subaccount = subaccount
            };
        }
    }

    internal class LoginParams
    {
        [JsonProperty("key")]
        public string Key { get; set; } = string.Empty;
        [JsonProperty("subaccount", NullValueHandling = NullValueHandling.Ignore)]
        public string? Subaccount { get; set; }
        [JsonProperty("sign")]
        public string Sign { get; set; } = string.Empty;
        [JsonProperty("time")]
        public long Time { get; set; }
    }
}
