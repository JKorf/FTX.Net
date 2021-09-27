using Newtonsoft.Json;

namespace FTX.Net.Objects.SocketObjects
{
    internal class UnsubscribeRequest: SocketRequest
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("market")]
        public string? Market { get; set; }

        public UnsubscribeRequest(string channel, string? symbol): base("unsubscribe")
        {
            Channel = channel;
            Market = symbol;
        }
    }
}
