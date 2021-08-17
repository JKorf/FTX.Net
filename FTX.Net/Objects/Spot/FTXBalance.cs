using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace FTX.Net.Objects
{
    /// <summary>
    /// Balance info
    /// </summary>
    public class FTXBalance: ICommonBalance
    {
        /// <summary>
        /// The asset the balance info is for
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Amount free
        /// </summary>
        public decimal Free { get; set; }
        /// <summary>
        /// Amount borrowed with spot margin
        /// </summary>
        public decimal SpotBorrow { get; set; }
        /// <summary>
        /// Total amount
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// Approximate total USD value
        /// </summary>
        public decimal UsdValue { get; set; }
        /// <summary>
        /// Amount available without borrowing
        /// </summary>
        public decimal AvailableWithoutBorrow { get; set; }

        string ICommonBalance.CommonAsset => Asset;

        decimal ICommonBalance.CommonAvailable => Free;

        decimal ICommonBalance.CommonTotal => Total;
    }
}
