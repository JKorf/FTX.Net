using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Options
{
    /// <summary>
    /// Volume
    /// </summary>
    public class FTXOptionsHistoricalVolume
    {
        /// <summary>
        /// Volume (in BTC)
        /// </summary>
        [JsonProperty("numContracts")]
        public decimal NumberOfContract { get; set; }
        /// <summary>
        /// Start time
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
