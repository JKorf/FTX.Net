using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Options
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
        public DateTime Expiry { get; set; }
    }
}
