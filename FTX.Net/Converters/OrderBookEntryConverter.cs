using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;
using FTX.Net.Objects.Models;

namespace FTX.Net.Converters
{
    internal class OrderBookEntryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var data = JArray.Load(reader);
            return ParseEntry(data);                     
        }

        private static FTXOrderBookEntry ParseEntry(JArray data)
        {
            // Not pretty, but it works.
            // I've not found any other way to consistently get the correct string value from a decimal
            // which keeps the trailing zero in for example `2543.0`
            var split = Regex.Replace(data.ToString(), @"(\s+|\r|\n| |\[|\]|)", "").Split(',');

            var result = new FTXOrderBookEntry()
            {
                Price = (decimal)data[0],
                Quantity = (decimal)data[1],
                RawPrice = split[0].ToLower(),
                RawQuantity = split[1].ToLower()
            };
            return result;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
