using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Enums
{
    /// <summary>
    /// Redeem status
    /// </summary>
    public enum NFTRedeemStatus
    {
        /// <summary>
        /// Requested
        /// </summary>
        Requested,
        /// <summary>
        /// Pending review
        /// </summary>
        PendingReview,
        /// <summary>
        /// Processing
        /// </summary>
        Processing,
        /// <summary>
        /// Sent
        /// </summary>
        Sent,
        /// <summary>
        /// Completed
        /// </summary>
        Completed,
        /// <summary>
        /// Cancelled
        /// </summary>
        Cancelled,
        /// <summary>
        /// Failed
        /// </summary>
        Failed
    }
}
