using Newtonsoft.Json;

namespace FTX.Net.Objects.SocketObjects
{
    internal class SubscribeRequest: SocketRequest
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("market")]
        public string? Market { get; set; }

        public SubscribeRequest(string channel, string? symbol): base("subscribe")
        {
            Channel = channel;
            Market = symbol;
        }
    }

    internal class GroupedOrderBookSubscribeRequest : SubscribeRequest 
    {
        [JsonProperty("grouping")]
        public int Grouping { get; set; }

        public GroupedOrderBookSubscribeRequest(string channel, string? symbol, int grouping) : base(channel, symbol)
        {
            Grouping = grouping;
        }
    }

}
