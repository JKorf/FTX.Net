using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Options
{
    /// <summary>
    /// Quote request
    /// </summary>
    public class FTXUserQuoteRequest: FTXQuoteRequest
    {
        /// <summary>
        /// Whether or not to hide your limit price if it exists
        /// </summary>
        public bool HideLimitPrice { get; set; }
        /// <summary>
        /// List of quotes for your quote request
        /// </summary>
        public IEnumerable<FTXOptionQuote> Quotes { get; set; } = Array.Empty<FTXOptionQuote>();
    }
}
