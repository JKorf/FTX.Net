using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Spot
{
    public class FTXOrderbook
    {
        /// <summary>
        /// Asks
        /// </summary>
        public IEnumerable<FTXOrderBookEntry> Asks { get; set; } = Array.Empty<FTXOrderBookEntry>();
        /// <summary>
        /// Bids
        /// </summary>
        public IEnumerable<FTXOrderBookEntry> Bids { get; set; } = Array.Empty<FTXOrderBookEntry>();
    }

    [JsonConverter(typeof(ArrayConverter))]
    public class FTXOrderBookEntry: ISymbolOrderBookEntry
    {
        /// <summary>
        /// Price of the entry
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity of the entry
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }
    }
}
