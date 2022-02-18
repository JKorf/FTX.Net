using System;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.Options
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
