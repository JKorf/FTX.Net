using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FTX.Net.Objects.Models.FTXPay
{
    /// <summary>
    /// App details
    /// </summary>
    public class FTXAppDetails
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Email to receive confirmations
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// User specific ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Coin that all payments must be in
        /// </summary>
        public string? AcceptedCoin { get; set; }
        /// <summary>
        /// Address to auto-withdraw funds to
        /// </summary>
        public string? WithdrawalAddress { get; set; }
        /// <summary>
        /// Wallet type for auto-withdrawal
        /// </summary>
        public string? WithdarwalWallet { get; set; }
        /// <summary>
        /// Auto-withdrawal period (in hours)
        /// </summary>
        public int WithdrawalPeriod { get; set; }
        /// <summary>
        /// Disabled
        /// </summary>
        public bool Disabled { get; set; }
        /// <summary>
        /// Deleted
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        /// Time the app was created
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Total value of all fetched payments
        /// </summary>
        public decimal TotalValue { get; set; }
        /// <summary>
        /// Quantity of fetched payments
        /// </summary>
        [JsonProperty("numPayments")]
        public int NumberOfPayments { get; set; }
        /// <summary>
        /// Exists
        /// </summary>
        public bool Exists { get; set; } = true;
        /// <summary>
        /// Payments
        /// </summary>
        public IEnumerable<FTXAppPayment> Payments { get; set; } = Array.Empty<FTXAppPayment>();
    }
}
