using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Spot
{
    /// <summary>
    /// Asset info
    /// </summary>
    public class FTXAsset
    {
        /// <summary>
        /// Deposit enabled
        /// </summary>
        public bool CanDeposit { get; set; }
        /// <summary>
        /// Withdraw enabled
        /// </summary>
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// True if addresses for this coin have a tag
        /// </summary>
        public bool HasTag { get; set; }
        /// <summary>
        /// Identifier
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Full name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Bep2 asset
        /// </summary>
        public string Bep2Asset { get; set; } = string.Empty;
        /// <summary>
        /// Can convert
        /// </summary>
        public bool CanConvert { get; set; }
        /// <summary>
        /// Collateral
        /// </summary>
        public bool Collateral { get; set; }
        /// <summary>
        /// Collateral weight
        /// </summary>
        public decimal CollateralWeight { get; set; }
        /// <summary>
        /// Credit to
        /// </summary>
        public string CreditTo { get; set; } = string.Empty;
        /// <summary>
        /// Erc20 contract
        /// </summary>
        public string Erc20Contract { get; set; } = string.Empty;
        /// <summary>
        /// Is fiat
        /// </summary>
        public bool Fiat { get; set; }
        /// <summary>
        /// Is token
        /// </summary>
        public bool IsToken { get; set; }
        /// <summary>
        /// Networks for withdrawing/depositing
        /// </summary>
        [JsonProperty("methods")]
        public IEnumerable<string> Networks { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Spl mint
        /// </summary>
        public string SplMint { get; set; } = string.Empty;
        /// <summary>
        /// Trc20 contract
        /// </summary>
        public string Trc20Contract { get; set; } = string.Empty;
        /// <summary>
        /// Usd fungible
        /// </summary>
        public bool UsdFungible { get; set; }
    }
}
