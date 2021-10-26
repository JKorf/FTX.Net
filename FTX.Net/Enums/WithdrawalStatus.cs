using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Enums
{
    /// <summary>
    /// Withdrawal status
    /// </summary>
    public enum WithdrawalStatus
    {
        /// <summary>
        /// Requested
        /// </summary>
        Requested,
        /// <summary>
        /// Processing
        /// </summary>
        Processing,
        /// <summary>
        /// Complete
        /// </summary>
        Complete,
        /// <summary>
        /// Canceled
        /// </summary>
        Canceled
    }
}
