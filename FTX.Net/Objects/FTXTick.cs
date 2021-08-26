using CryptoExchange.Net.ExchangeInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects
{
    /// <summary>
    /// FTX tick
    /// </summary>
    public class FTXTick: ICommonTicker
    {
        /// <summary>
        /// High price
        /// </summary>
        public decimal High { get; set; }
        /// <summary>
        /// Low price
        /// </summary>
        public decimal Low { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        string ICommonTicker.CommonSymbol => Symbol;

        decimal ICommonTicker.CommonHigh => High;

        decimal ICommonTicker.CommonLow => Low;

        decimal ICommonTicker.CommonVolume => Volume;
    }
}
