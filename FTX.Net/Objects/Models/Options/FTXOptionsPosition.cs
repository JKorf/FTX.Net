using FTX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Options
{
    /// <summary>
    /// Options position info
    /// </summary>
    public class FTXOptionsPosition
    {
        /// <summary>
        /// Entry price
        /// </summary>
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// Net amount, positive for long, negative for short
        /// </summary>
        public decimal NetSize { get; set; }
        /// <summary>
        /// Absolute value of NetSize
        /// </summary>
        public decimal Size { get; set; }
        /// <summary>
        /// Option
        /// </summary>
        public FTXOption Option { get; set; } = default!;
        /// <summary>
        /// Side, buy for long, sell for short
        /// </summary>
        public OrderSide Side { get; set; }
        /// <summary>
        /// Pessimistic valuation of this position used for margin purposes
        /// </summary>
        public decimal? PessimisticValuation { get; set; }
        /// <summary>
        /// Index price corresponding to pessimistic valuation
        /// </summary>
        public decimal? PessimisticIndexPrice { get; set; }
        /// <summary>
        /// Vol corresponding to pessimistic valuation
        /// </summary>
        public decimal? PessimisticVol { get; set; }
    }
}
