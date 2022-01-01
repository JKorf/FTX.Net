using System;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models
{
    /// <summary>
    /// Kline info
    /// </summary>
    public class FTXKline
    {
        /// <summary>
        /// Close price
        /// </summary>
        [JsonProperty("close")]
        public decimal ClosePrice{ get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        [JsonProperty("open")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// High price
        /// </summary>
        [JsonProperty("high")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Low price
        /// </summary>
        [JsonProperty("low")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        public decimal? Volume { get; set; }
        /// <summary>
        /// Open time
        /// </summary>
        [JsonProperty("startTime")]
        public DateTime OpenTime { get; set; }
    }
}
