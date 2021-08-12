using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Futures
{
    /// <summary>
    /// Funding payment info
    /// </summary>
    public class FTXFundingPayment
    {
        /// <summary>
        /// Future
        /// </summary>
        public string Future { get; set; } = string.Empty;
        /// <summary>
        /// Payment id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Amount payed
        /// </summary>
        public decimal Payment { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        public decimal Rate { get; set; }
    }
}
