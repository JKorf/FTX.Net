using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.Models.NFT;

namespace FTX.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// FTX NFT endpoints
    /// </summary>
    public interface IFTXClientGeneralApiNft
    {
        /// <summary>
        /// Get list of NFTs
        /// <para><a href="https://docs.ftx.com/#list-nfts" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNft>>> GetNftsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get info on a NFT
        /// <para><a href="https://docs.ftx.com/#get-nft-info" /></para>
        /// </summary>
        /// <param name="nftId">Id of the NFT</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNft>> GetNftAsync(long nftId, CancellationToken ct = default);

        /// <summary>
        /// Get info on the trades of a NFT
        /// <para><a href="https://docs.ftx.com/#get-nft-trades" /></para>
        /// </summary>
        /// <param name="nftId">Id of the NFT</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNftTrade>>> GetNftTradesAsync(long nftId, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get all NFT trades
        /// <para><a href="https://docs.ftx.com/#get-all-nft-trades" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNftTradeAll>>> GetNftAllTradesAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get details on a NFT for the user
        /// <para><a href="https://docs.ftx.com/#get-nft-account-info" /></para>
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<NFTUserInfo>> GetNftUserInfoAsync(long nftId, CancellationToken ct = default);

        /// <summary>
        /// Get all collections
        /// <para><a href="https://docs.ftx.com/#get-all-nft-collections" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNftCollection>>> GetNftCollectionsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get user balances
        /// <para><a href="https://docs.ftx.com/#get-nft-balances" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNft>>> GetNftBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Create an offer of an NFT
        /// <para><a href="https://docs.ftx.com/#make-nft-offer" /></para>
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="price">Price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNft>> CreateNftOfferAsync(long nftId, decimal price, CancellationToken ct = default);

        /// <summary>
        /// Buy a NFT
        /// <para><a href="https://docs.ftx.com/#buy-nft" /></para>
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="price">Price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNft>> BuyNftAsync(long nftId, decimal price, CancellationToken ct = default);

        /// <summary>
        /// Create a new auction
        /// <para><a href="https://docs.ftx.com/#create-auction" /></para>
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
        /// <para><a href="https://docs.ftx.com/#edit-auction" /></para>
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="reservationPrice">Reservation price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNft>> EditAuctionAsync(long nftId, decimal reservationPrice, CancellationToken ct = default);

        /// <summary>
        /// Cancel an auction
        /// <para><a href="https://docs.ftx.com/#cancel-auction" /></para>
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNft>> CancelAuctionAsync(long nftId, CancellationToken ct = default);

        /// <summary>
        /// Get bids
        /// <para><a href="https://docs.ftx.com/#get-bids" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNft>>> GetBidsAsync(CancellationToken ct = default);

        /// <summary>
        /// Place a bid on an NFT auction
        /// <para><a href="https://docs.ftx.com/#place-bid" /></para>
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="price">Bid price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNft>> PlaceBidAsync(long nftId, decimal price, CancellationToken ct = default);

        /// <summary>
        /// Get NFT deposits
        /// <para><a href="https://docs.ftx.com/#get-nft-deposits" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNftDeposit>>> GetNftDepositsAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get NFT withdrawals
        /// <para><a href="https://docs.ftx.com/#get-nft-withdrawals" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNftWithdrawal>>> GetNftWithdrawalsAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get NFT trades
        /// <para><a href="https://docs.ftx.com/#get-nft-fills" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<FTXNftUserTrade>>> GetNftUserTradesAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem a NFT
        /// <para><a href="https://docs.ftx.com/#redeem-nft" /></para>
        /// </summary>
        /// <param name="nftId">NFT id to redeem</param>
        /// <param name="address">Address to redeem to</param>
        /// <param name="notes">Notes</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNftRedeem>> RedeemNftAsync(long nftId, string address, string? notes = null, CancellationToken ct = default);

        /// <summary>
        /// Get NFT gallery
        /// <para><a href="https://docs.ftx.com/#get-nft-gallery" /></para>
        /// </summary>
        /// <param name="galleryId">Id of the gallery</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNftGallery>> GetNftGalleryAsync(long galleryId, CancellationToken ct = default);

        /// <summary>
        /// Get NFT gallery settings
        /// <para><a href="https://docs.ftx.com/#get-gallery-settings" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<FTXNftGallerySettings>> GetGallerySettingsAsync(CancellationToken ct = default);

        /// <summary>
        /// Edit NFT gallery settings
        /// <para><a href="https://docs.ftx.com/#edit-gallery-settings" /></para>
        /// </summary>
        /// <param name="isPublic">Gallery is public or not</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> EditGallerySettingsAsync(bool isPublic, CancellationToken ct = default);
    }
}