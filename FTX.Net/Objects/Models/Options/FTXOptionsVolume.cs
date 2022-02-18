using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.Options
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
