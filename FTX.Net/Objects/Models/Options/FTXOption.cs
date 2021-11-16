using System;
using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.Options
{
    /// <summary>
    /// Option info
    /// </summary>
    public class FTXOption
    {
        /// <summary>
        /// Underlying
        /// </summary>
        public string Underlying { get; set; } = string.Empty;
        /// <summary>
        /// Option type
        /// </summary>
        [JsonConverter(typeof(OptionTypeConverter))]
        public OptionType Type { get; set; }
        /// <summary>
        /// Strike
        /// </summary>
        public decimal Strike { get; set; }
        /// <summary>
        /// Expiry time
        /// </summary>
        [JsonProperty("expiry")]
        public DateTime ExpiryTime { get; set; }
    }
}
