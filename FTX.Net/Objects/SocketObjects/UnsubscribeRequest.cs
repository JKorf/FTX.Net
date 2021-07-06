using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.SocketObjects
{
    public class UnsubscribeRequest: SocketRequest
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }

        public UnsubscribeRequest(string channel, string symbol): base("unsubscribe")
        {
            Channel = channel;
            Market = symbol;
        }
    }
}
