using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;
using Force.Crc32;
using FTX.Net.Objects;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using FTX.Net.Interfaces.Clients.Socket;
using FTX.Net.Clients.Socket;
using FTX.Net.Objects.Models;
using FTX.Net.Objects.Models.Socket;

namespace FTX.Net.SymbolOrderBooks
{
    /// <summary>
    /// Symbol order book
    /// </summary>
    public class FTXSymbolOrderBook : SymbolOrderBook
    {
        private readonly IFTXSocketClient _socketClient;
        private readonly bool _socketOwner;
        private readonly int? _grouping;

        /// <summary>
        /// Create a new order book
        /// </summary>
        /// <param name="symbol">Symbol the book is for</param>
        /// <param name="options">Options for the book</param>
        public FTXSymbolOrderBook(string symbol, FTXSymbolOrderBookOptions? options = null) : base("FTX", symbol, options ?? new FTXSymbolOrderBookOptions())
        {
            strictLevels = false;
            sequencesAreConsecutive = false;

            _socketClient = options?.Client ?? new FTXSocketClient(new FTXSocketClientOptions
            {
                LogLevel = options?.LogLevel ?? LogLevel.Information
            });
            _socketOwner = options?.Client == null;
            _grouping = options?.Grouping;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync()
        {
            CallResult<UpdateSubscription> subResult;
            if (_grouping.HasValue)
            {
                subResult = await _socketClient.Streams.SubscribeToGroupedOrderBookUpdatesAsync(Symbol, _grouping.Value, DataHandler).ConfigureAwait(false);
                if (!subResult)
                    return subResult;
            }
            else
            {
                subResult = await _socketClient.Streams.SubscribeToOrderBookUpdatesAsync(Symbol, DataHandler).ConfigureAwait(false);
                if (!subResult)
                    return subResult;
            }

            Status = OrderBookStatus.Syncing;
            var setResult = await WaitForSetOrderBookAsync(10000).ConfigureAwait(false);
            return setResult ? subResult : new CallResult<UpdateSubscription>(null, setResult.Error);
        }

        private void DataHandler(DataEvent<FTXStreamOrderBook> update)
        {
            if (update.Data.Action == "partial")
            {
                SetInitialOrderBook(update.Data.Timestamp.Ticks, update.Data.Bids, update.Data.Asks);
                if(!_grouping.HasValue)
                    AddChecksum((int)update.Data.Checksum);
            }
            else
            {
                UpdateOrderBook(update.Data.Timestamp.Ticks, update.Data.Bids, update.Data.Asks);
                if(!_grouping.HasValue)
                    AddChecksum((int)update.Data.Checksum);
            }
        }

        /// <inheritdoc />
        protected override bool DoChecksum(int checksum)
        {
            var checksumString = "";
            for (var i = 0; i < 100; i++)
            {
                if (bids.Count > i)
                {
                    var bid = (FTXOrderBookEntry)bids.ElementAt(i).Value;
                    checksumString += $"{bid.RawPrice}:{bid.RawQuantity}:";
                }
                if (asks.Count > i)
                {
                    var ask = (FTXOrderBookEntry)asks.ElementAt(i).Value;
                    checksumString += $"{ask.RawPrice}:{ask.RawQuantity}:";
                }
            }

            checksumString = checksumString.TrimEnd(':');

            var ourChecksumUtf = (int)Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(checksumString));

            if (ourChecksumUtf != checksum)
            {
                log.Write(LogLevel.Warning, $"{Symbol} Invalid checksum. Received from server: {checksum}, calculated local: {ourChecksumUtf}");
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync()
        {
            return await WaitForSetOrderBookAsync(10000).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            processBuffer.Clear();
            asks.Clear();
            bids.Clear();

            if(_socketOwner)
                _socketClient?.Dispose();
        }
    }
}
