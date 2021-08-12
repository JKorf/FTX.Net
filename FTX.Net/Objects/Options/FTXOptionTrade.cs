using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Options
{
    /// <summary>
    /// Option trade
    /// </summary>
    public class FTXOptionTrade
    {
        /// <summary>
        /// Trade id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Size of trade
        /// </summary>
        public decimal Size { get; set; }
        /// <summary>
        /// Price of trade
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Option
        /// </summary>
        public FTXOption Option { get; set; } = default!;
        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Time { get; set; }
    }
}
