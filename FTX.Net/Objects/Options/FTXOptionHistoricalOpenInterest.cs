using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Options
{
    /// <summary>
    /// Interest
    /// </summary>
    public class FTXOptionHistoricalOpenInterest
    {
        /// <summary>
        /// Open interest (in BTC)
        /// </summary>
        [JsonProperty("numContracts")]
        public decimal NumberOfContracts { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        public DateTime Time { get; set; }
    }
}
