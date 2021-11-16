using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using FTX.Net.Converters;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.Socket
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
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
    }
}
