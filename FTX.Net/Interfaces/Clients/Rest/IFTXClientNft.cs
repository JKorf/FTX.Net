using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.NFT;

namespace FTX.Net.Interfaces.Clients.Rest
{
    /// <summary>
    /// NFT endpoints
    /// </summary>
    public interface IFTXClientNft
    {
        /// <summary>
        /// Get list of NFTs
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNft>>> GetNftsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get info on a NFT
        /// </summary>
        /// <param name="nftId">Id of the NFT</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNft>> GetNftAsync(long nftId, CancellationToken ct = default);

        /// <summary>
        /// Get info on the trades of a NFT
        /// </summary>
        /// <param name="nftId">Id of the NFT</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNftTrade>>> GetNftTradesAsync(long nftId, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get all NFT trades
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNftTradeAll>>> GetNftAllTradesAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get details on a NFT for the user
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<NFTUserInfo>> GetNftUserInfoAsync(long nftId, CancellationToken ct = default);

        /// <summary>
        /// Get all collections
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNftCollection>>> GetNftCollectionsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get user balances
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNft>>> GetNftBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Create an offer of an NFT
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="price">Price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNft>> CreateNftOfferAsync(long nftId, decimal price, CancellationToken ct = default);

        /// <summary>
        /// Buy a NFT
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="price">Price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNft>> BuyNftAsync(long nftId, decimal price, CancellationToken ct = default);

        /// <summary>
        /// Create a new auction
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="initialPrice">Initial price</param>
        /// <param name="reservationPrice">Reservation price</param>
        /// <param name="duration">Duration of the auction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNft>> CreateAuctionAsync(long nftId, decimal initialPrice, decimal reservationPrice, TimeSpan duration, CancellationToken ct = default);

        /// <summary>
        /// Edit an auction
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="reservationPrice">Reservation price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNft>> EditAuctionAsync(long nftId, decimal reservationPrice, CancellationToken ct = default);

        /// <summary>
        /// Cancel an auction
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNft>> CancelAuctionAsync(long nftId, CancellationToken ct = default);

        /// <summary>
        /// Get bids
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNft>>> GetBidsAsync(CancellationToken ct = default);

        /// <summary>
        /// Place a bid on an NFT auction
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="price">Bid price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNft>> PlaceBidAsync(long nftId, decimal price, CancellationToken ct = default);

        /// <summary>
        /// Get NFT deposits
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNftDeposit>>> GetNftDepositsAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get NFT withdrawals
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNftWithdrawal>>> GetNftWithdrawalsAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get NFT trades
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNftUserTrade>>> GetNftUserTradesAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem a NFT
        /// </summary>
        /// <param name="nftId">NFT id to redeem</param>
        /// <param name="address">Address to redeem to</param>
        /// <param name="notes">Notes</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNftRedeem>> RedeemNftAsync(long nftId, string address, string? notes = null, CancellationToken ct = default);

        /// <summary>
        /// Get NFT gallery
        /// </summary>
        /// <param name="galleryId">Id of the gallery</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNftGallery>> GetNftGalleryAsync(long galleryId, CancellationToken ct = default);

        /// <summary>
        /// Get NFT gallery settings
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNftGallerySettings>> GetGallerySettingsAsync(CancellationToken ct = default);

        /// <summary>
        /// Edit NFT gallery settings
        /// </summary>
        /// <param name="isPublic">Gallery is public or not</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> EditGallerySettingsAsync(bool isPublic, CancellationToken ct = default);
    }
}