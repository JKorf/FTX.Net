using CryptoExchange.Net.ExchangeInterfaces;

namespace FTX.Net.Objects.Models
{
    /// <summary>
    /// FTX tick
    /// </summary>
    public class FTXTick: ICommonTicker
    {
        /// <summary>
        /// High price
        /// </summary>
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Low price
        /// </summary>
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        string ICommonTicker.CommonSymbol => Symbol;

        decimal ICommonTicker.CommonHighPrice => HighPrice;

        decimal ICommonTicker.CommonLowPrice => LowPrice;

        decimal ICommonTicker.CommonVolume => Volume;
    }
}
