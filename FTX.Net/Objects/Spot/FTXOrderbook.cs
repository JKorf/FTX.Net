using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Spot
{
    /// <summary>
    /// Order book
    /// </summary>
    public class FTXOrderbook: ICommonOrderBook
    {
        /// <summary>
        /// Asks
        /// </summary>
        public IEnumerable<FTXOrderBookEntry> Asks { get; set; } = Array.Empty<FTXOrderBookEntry>();
        /// <summary>
        /// Bids
        /// </summary>
        public IEnumerable<FTXOrderBookEntry> Bids { get; set; } = Array.Empty<FTXOrderBookEntry>();

        IEnumerable<ISymbolOrderBookEntry> ICommonOrderBook.CommonBids => Bids;

        IEnumerable<ISymbolOrderBookEntry> ICommonOrderBook.CommonAsks => Asks;
    }

    /// <summary>
    /// Order book entry
    /// </summary>
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

        [JsonIgnore]
        internal string RawPrice { get; set; } = string.Empty;
        [JsonIgnore]
        internal string RawQuantity{get;set; } = string.Empty;
    }
}
