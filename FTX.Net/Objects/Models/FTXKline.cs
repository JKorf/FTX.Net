using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Spot
{
    /// <summary>
    /// Kline info
    /// </summary>
    public class FTXKline: ICommonKline
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

        decimal ICommonKline.CommonHighPrice => HighPrice;

        decimal ICommonKline.CommonLowPrice => LowPrice;

        decimal ICommonKline.CommonOpenPrice => OpenPrice;

        decimal ICommonKline.CommonClosePrice => ClosePrice;

        DateTime ICommonKline.CommonOpenTime => OpenTime;

        decimal ICommonKline.CommonVolume => Volume ?? 0;
    }
}
