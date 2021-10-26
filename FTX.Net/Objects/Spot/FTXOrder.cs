using CryptoExchange.Net.ExchangeInterfaces;
using FTX.Net.Converters;
using FTX.Net.Enums;
using FTX.Net.Objects.Spot;
using Newtonsoft.Json;
using System;

namespace FTX.Net.Objects
{
    /// <summary>
    /// Order info
    /// </summary>
    public class FTXOrder: FTXOrderBase, ICommonOrderId, ICommonOrder
    {
        /// <summary>
        /// The order type
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        [JsonProperty("type")]
        public override OrderType Type { get; set; }

        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonProperty("price")]
        public override decimal? Price { get; set; }

        /// <summary>
        /// The remaining quantity
        /// </summary>
        [JsonProperty("remainingSize")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        [JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }
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
        [JsonProperty("clientId")]
        public string? ClientOrderId { get; set; }

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

                if (QuantityRemaining > 0)
                    return IExchangeClient.OrderStatus.Canceled;

                return IExchangeClient.OrderStatus.Filled;
            }
        }

        bool ICommonOrder.IsActive => Status == OrderStatus.New || Status == OrderStatus.Open;

        IExchangeClient.OrderSide ICommonOrder.CommonSide => Side == OrderSide.Buy ? IExchangeClient.OrderSide.Buy : IExchangeClient.OrderSide.Sell;

        IExchangeClient.OrderType ICommonOrder.CommonType => Type == OrderType.Limit ? IExchangeClient.OrderType.Limit : IExchangeClient.OrderType.Market;

        DateTime ICommonOrder.CommonOrderTime => CreateTime;

        string ICommonOrderId.CommonId => Id.ToString();
    }
}
