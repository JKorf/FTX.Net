using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Options
{
    /// <summary>
    /// 24 hours options volume
    /// </summary>
    public class FTXOptionsVolume
    {
        /// <summary>
        /// Number of contracts traded over the last 24 hours
        /// </summary>
        public decimal Contracts { get; set; }
        /// <summary>
        /// Notional value of the contracts traded over the last 24 hours
        /// </summary>
        [JsonProperty("underlying_total")]
        public decimal UnderlyingTotal { get; set; }
    }
}
