﻿using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System.Collections.Generic;

namespace FTX.Net.Converters
{
    internal class LiquidityTypeConverter : BaseConverter<LiquidityType>
    {
        public LiquidityTypeConverter() : this(true) { }
        public LiquidityTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<LiquidityType, string>> Mapping => new List<KeyValuePair<LiquidityType, string>>
        {
            new KeyValuePair<LiquidityType, string>(LiquidityType.Maker, "maker"),
            new KeyValuePair<LiquidityType, string>(LiquidityType.Taker, "taker"),
        };
    }
}
