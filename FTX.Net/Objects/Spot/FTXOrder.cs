using CryptoExchange.Net.ExchangeInterfaces;
using FTX.Net.Converters;
using FTX.Net.Enums;
using Newtonsoft.Json;
using System;

namespace FTX.Net.Objects
{
    /// <summary>
    /// Order info
    /// </summary>
    public class FTXOrder: ICommonOrderId, ICommonOrder
    {
        /// <summary>
        /// Id of the order
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// When the order was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The quantity that is filled
        /// </summary>
        [JsonProperty("filledSize")]
        public decimal FilledQuantity { get; set; }
        /// <summary>
        /// The future the order is for
        /// </summary>
        public string Future { get; set; } = string.Empty;
        /// <summary>
        /// The symbol to order is for
        /// </summary>
        [JsonProperty("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The price of the order
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// The remaining quantity
        /// </summary>
        [JsonProperty("remainingSize")]
        public decimal RemainingQuantity { get; set; }
        /// <summary>
        /// The side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// The order type
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }
        /// <summary>
        /// The total quantity of the order
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        [JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Reduce only order
        /// </summary>
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// Immediate or cancel order
        /// </summary>
        [JsonProperty("ioc")]
        public bool ImmediateOrCancel { get; set; }
        /// <summary>
        /// Post only order
        /// </summary>
        public bool PostOnly { get; set; }
        /// <summary>
        /// Client id
        /// </summary>
        public string? ClientId { get; set; }
        /// <summary>
        /// Average execution price
        /// </summary>
        [JsonProperty("avgFillPrice")]
        public decimal? AverageFillPrice { get; set; }

        string ICommonOrder.CommonSymbol => Symbol;

        decimal ICommonOrder.CommonPrice => Price ?? 0;

        decimal ICommonOrder.CommonQuantity => Quantity;

        IExchangeClient.OrderStatus ICommonOrder.CommonStatus
        {
            get
            {
                if (Status == OrderStatus.New)
                    return IExchangeClient.OrderStatus.Active;
                
                if (Status == OrderStatus.Open)
                    return IExchangeClient.OrderStatus.Active;

                if (RemainingQuantity > 0)
                    return IExchangeClient.OrderStatus.Canceled;

                return IExchangeClient.OrderStatus.Filled;
            }
        }

        bool ICommonOrder.IsActive => Status == OrderStatus.New || Status == OrderStatus.Open;

        IExchangeClient.OrderSide ICommonOrder.CommonSide => Side == OrderSide.Buy ? IExchangeClient.OrderSide.Buy : IExchangeClient.OrderSide.Sell;

        IExchangeClient.OrderType ICommonOrder.CommonType => Type == OrderType.Limit ? IExchangeClient.OrderType.Limit : IExchangeClient.OrderType.Market;

        DateTime ICommonOrder.CommonOrderTime => CreatedAt;

        string ICommonOrderId.CommonId => Id.ToString();
    }
}
