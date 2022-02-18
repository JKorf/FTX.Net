using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTX.Net.Interfaces.Clients;
using FTX.Net.Objects.Models;
using FTX.Net.Objects.Models.Socket;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FTX.Net.UnitTests
{
    internal class JsonSocketTests
    {
        [Test]
        public async Task ValidateTickerUpdateStreamJson()
        {
            await TestFileToObject<FTXStreamTicker>(@"JsonResponses\Socket\TickerUpdate.txt");
        }

        [Test]
        public async Task ValidateSymbolsUpdateStreamJson()
        {
            await TestFileToObject<Dictionary<string, FTXStreamSymbol>>(@"JsonResponses\Socket\SymbolsUpdate.txt");
        }

        [Test]
        public async Task ValidateTradeUpdateStreamJson()
        {
            await TestFileToObject<IEnumerable<FTXTrade>>(@"JsonResponses\Socket\TradeUpdate.txt");
        }

        [Test]
        public async Task ValidateOrderBookUpdateStreamJson()
        {
            await TestFileToObject<FTXStreamOrderBook>(@"JsonResponses\Socket\OrderBookUpdate.txt");
        }

        [Test]
        public async Task ValidateUserTradeUpdateStreamJson()
        {
            await TestFileToObject<FTXUserTrade>(@"JsonResponses\Socket\UserTradeUpdate.txt");
        }

        [Test]
        public async Task ValidateOrderUpdateStreamJson()
        {
            await TestFileToObject<FTXOrder>(@"JsonResponses\Socket\OrderUpdate.txt");
        }


        private static async Task TestFileToObject<T>(string filePath, List<string> ignoreProperties = null)
        {
            var listener = new EnumValueTraceListener();
            Trace.Listeners.Add(listener);
            var path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string json;
            try
            {
                var file = File.OpenRead(Path.Combine(path, filePath));
                using var reader = new StreamReader(file);
                json = await reader.ReadToEndAsync();
            }
            catch (FileNotFoundException)
            {
                throw;
            }

            var result = JsonConvert.DeserializeObject<T>(json);
            JsonToObjectComparer<IFTXSocketClient>.ProcessData("", result, json, ignoreProperties: new Dictionary<string, List<string>>
            {
                { "", ignoreProperties ?? new List<string>() }
            });
            Trace.Listeners.Remove(listener);
        }
    }

    internal class EnumValueTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            if (message.Contains("Cannot map"))
                throw new Exception("Enum value error: " + message);
        }

        public override void WriteLine(string message)
        {
            if (message.Contains("Cannot map"))
                throw new Exception("Enum value error: " + message);
        }
    }
}
