using FTX.Net.Objects.Futures;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects.Spot
{
    /// <summary>
    /// Account info
    /// </summary>
    public class FTXAccountInfo
    {
        /// <summary>
        /// Whether or not the account is a registered backstop liquidity provider
        /// </summary>
        public bool BackstopProvider { get; set; }
        /// <summary>
        /// Amount of collateral
        /// </summary>
        public decimal Collateral { get; set; }
        /// <summary>
        /// Amount of free collateral
        /// </summary>
        public decimal FreeCollateral { get; set; }
        /// <summary>
        /// Average of initialMarginRequirement for individual futures, weighed by position notional. Cannot open new positions if openMarginFraction falls below this value.
        /// </summary>
        public decimal InitialMarginRequirement { get; set; }
        /// <summary>
        /// True if the account is currently being liquidated
        /// </summary>
        public bool Liquidating { get; set; }
        /// <summary>
        /// Average of maintenanceMarginRequirement for individual futures, weighed by position notional. Account enters liquidation mode if margin fraction falls below this value.
        /// </summary>
        public decimal MaintenanceMarginRequirement { get; set; }
        /// <summary>
        /// Maker fee
        /// </summary>
        public decimal MakerFee { get; set; }
        /// <summary>
        /// Ratio between total account value and total account position notional.
        /// </summary>
        public decimal? MarginFraction { get; set; }
        /// <summary>
        /// Ratio between total realized account value and total open position notional
        /// </summary>
        public decimal? OpenMarginFraction { get; set; }
        /// <summary>
        /// Taker fee
        /// </summary>
        public decimal TakerFee { get; set; }
        /// <summary>
        /// Total value of the account, using mark price for positions
        /// </summary>
        public decimal TotalAccountValue { get; set; }
        /// <summary>
        /// Total size of positions held by the account, using mark price
        /// </summary>
        public decimal TotalPositionSize { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// Max account leverage
        /// </summary>
        public decimal Leverage { get; set; }
        /// <summary>
        /// Position limit
        /// </summary>
        public decimal? PositionLimit { get; set; }
        /// <summary>
        /// Position limit used
        /// </summary>
        public decimal? PositionLimitUsed { get; set; }
        /// <summary>
        /// Use FTT as collateral
        /// </summary>
        public bool UseFttCollateral { get; set; }
        /// <summary>
        /// Charge interest on negative USD
        /// </summary>
        public bool ChargeInterestOnNegativeUsd { get; set; }
        /// <summary>
        /// Margin enabled for spot
        /// </summary>
        public bool SpotMarginEnabled { get; set; }
        /// <summary>
        /// Lending enabled for spot
        /// </summary>
        public bool SpotLendingEnabled { get; set; }
        /// <summary>
        /// Positions
        /// </summary>
        public IEnumerable<FTXPosition> Positions { get; set; } = Array.Empty<FTXPosition>();
    }
}
