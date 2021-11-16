using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.LeveragedTokens
{
    /// <summary>
    /// Leveraged token info
    /// </summary>
    public class FTXLeveragedToken
    {
        /// <summary>
        /// Name of the token
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Name of the underlying futures contract used by this token
        /// </summary>
        public string Underlying { get; set; } = string.Empty;
        /// <summary>
        /// Target leverage
        /// </summary>
        public decimal Leverage { get; set; }
        /// <summary>
        /// Number of outstanding tokens
        /// </summary>
        public decimal Outstanding { get; set; }
        /// <summary>
        /// TotalNav divided by outstanding
        /// </summary>
        public decimal PricePerShare { get; set; }
        /// <summary>
        /// Futures positions per share: one element for each item in targetComponents
        /// </summary>
        public Dictionary<string, decimal> PositionsPerShare { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// Holdings per share
        /// </summary>
        public Dictionary<string, decimal> Basket { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// Futures to be included in the basket of the leverage token. For BVOL and IBVOL futures, this contains multiple entries
        /// </summary>
        public IEnumerable<string> TargetComponents { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Total value of the leveraged token holdings (basket holdings marked to market times outstanding)
        /// </summary>
        public decimal TotalNav { get; set; }
        /// <summary>
        /// Total collateral in the leveraged token account
        /// </summary>
        public decimal TotalCollateral { get; set; }
        /// <summary>
        /// Current leverage
        /// </summary>
        public decimal CurrentLeverage { get; set; }
        /// <summary>
        /// Underlying futures position held by each token
        /// </summary>
        public decimal PositionPerShare { get; set; }
        /// <summary>
        /// Current mark price of the underlying future
        /// </summary>
        public decimal UnderlyingMark { get; set; }
        /// <summary>
        /// ERC20 smart contract address of the token
        /// </summary>
        public string? ContractAddress { get; set; }
        /// <summary>
        /// Bep2 asset name
        /// </summary>
        public string? Bep2AssetName { get; set; }
        /// <summary>
        /// Change in the price of the token over the past hour
        /// </summary>
        [JsonProperty("change1h")]
        public decimal Change1Hour { get; set; }
        /// <summary>
        /// Change in the price of the token over the past day
        /// </summary>
        [JsonProperty("change24h")]
        public decimal Change24Hour { get; set; }
        /// <summary>
        /// Change in price since 00:00 UTC
        /// </summary>
        [JsonProperty("changeBod")]
        public decimal ChangeBeginingOfDay { get; set; }
    }
}
