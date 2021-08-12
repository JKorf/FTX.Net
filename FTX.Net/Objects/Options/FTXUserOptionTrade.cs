using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Options
{
    /// <summary>
    /// Option fill
    /// </summary>
    public class FTXUserOptionTrade : FTXOptionTrade
    {
        /// <summary>
        /// Liquidity 
        /// </summary>
        [JsonConverter(typeof(LiquidityTypeConverter))]
        public LiquidityType Liquidity { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee rate
        /// </summary>
        public decimal FeeRate { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
    }
}
