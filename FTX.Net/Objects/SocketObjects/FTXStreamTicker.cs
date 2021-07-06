using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.SocketObjects
{
    public class FTXStreamTicker
    {
        [JsonProperty("ask")]
        public decimal? BestAsk { get; set; }
        [JsonProperty("bid")]
        public decimal? BestBid { get; set; }
        [JsonProperty("last")]
        public decimal? LastTrade { get; set; }
    }
}
