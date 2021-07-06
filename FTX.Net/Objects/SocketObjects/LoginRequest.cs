using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.SocketObjects
{
    public class LoginRequest : SocketRequest
    {
        [JsonProperty("args")]
        public LoginParams Parameters { get; set; }

        public LoginRequest(string key, string sign, long time): base("login")
        {
            Parameters = new LoginParams
            {
                Key = key,
                Sign = sign,
                Time = time
            };
        }
    }

    public class LoginParams
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("sign")]
        public string Sign { get; set; }
        [JsonProperty("time")]
        public long Time { get; set; }
    }
}
