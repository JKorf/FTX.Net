using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Spot
{
    /// <summary>
    /// Kline info
    /// </summary>
    public class FTXKline
    {
        /// <summary>
        /// Close price
        /// </summary>
        public decimal Close { get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        public decimal Open { get; set; }
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
        /// Start time
        /// </summary>
        public DateTime StartTime { get; set; }
    }
}
