using CryptoExchange.Net.Converters;
using FTX.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Spot.Socket
{
    /// <summary>
    /// Stream order book update
    /// </summary>
    public class FTXStreamOrderBook
    {
        /// <summary>
        /// Update action, `partial` for the initial snapshot, `update` for updates to that snapshot
        /// </summary>
        public string Action { get; set; } = string.Empty;
        /// <summary>
        /// Changed bids
        /// </summary>
        [JsonProperty(ItemConverterType = typeof(OrderBookEntryConverter))]
        public IEnumerable<FTXOrderBookEntry> Bids { get; set; } = Array.Empty<FTXOrderBookEntry>();
        /// <summary>
        /// Changed asks
        /// </summary>
        [JsonProperty(ItemConverterType = typeof(OrderBookEntryConverter))]
        public IEnumerable<FTXOrderBookEntry> Asks { get; set; } = Array.Empty<FTXOrderBookEntry>();
        /// <summary>
        /// Checksum
        /// </summary>
        public uint Checksum { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime Time { get; set; }
    }
}
