using CryptoExchange.Net.Converters;
using FTX.Net.Enums;
using System.Collections.Generic;

namespace FTX.Net.Converters
{
    internal class FutureTypeConverter : BaseConverter<FutureType>
    {
        public FutureTypeConverter() : this(true) { }
        public FutureTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FutureType, string>> Mapping => new List<KeyValuePair<FutureType, string>>
        {
            new KeyValuePair<FutureType, string>(FutureType.Future, "future"),
            new KeyValuePair<FutureType, string>(FutureType.Perpetual, "perpetual"),
            new KeyValuePair<FutureType, string>(FutureType.Move, "move"),           
            new KeyValuePair<FutureType, string>(FutureType.Prediction, "prediction"),
        };
    }
}
