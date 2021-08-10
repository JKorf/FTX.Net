using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Spot
{
    /// <summary>
    /// Deposit address
    /// </summary>
    public class FTXDepositAddress
    {
        /// <summary>
        /// Deposit address
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Tag
        /// </summary>
        public string? Tag { get; set; }
    }
}
