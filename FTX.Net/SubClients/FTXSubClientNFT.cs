using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.Convert;
using FTX.Net.Objects.NFT;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FTX.Net.SubClients
{
    /// <summary>
    /// NFT endpoints
    /// </summary>
    public class FTXSubClientNFT
    {
        private readonly FTXClient _baseClient;

        internal FTXSubClientNFT(FTXClient baseClient)
        {
            _baseClient = baseClient;
        }

        /// <summary>
        /// Get list of NFTs
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXNft>>> GetNftsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNft>>(_baseClient.GetUri("nft/nfts"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get info on a NFT
        /// </summary>
        /// <param name="nftId">Id of the NFT</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXNft>> GetNftAsync(long nftId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXNft>(_baseClient.GetUri("nft/nft/" + nftId ), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get info on the trades of a NFT
        /// </summary>
        /// <param name="nftId">Id of the NFT</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXNftTrade>>> GetNFTTradesAsync(long nftId, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            _baseClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNftTrade>>(_baseClient.GetUri($"nft/nft/{nftId}/trades"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get all NFT trades
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXNftTradeAll>>> GetNFTAllTradesAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            _baseClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNftTradeAll>>(_baseClient.GetUri($"nft/all_trades"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get details on a NFT for the user
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<NFTUserInfo>> GetNFTUserInfoAsync(long nftId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<NFTUserInfo>(_baseClient.GetUri($"nft/nft/{nftId}/account_info"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get all collections
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXNftCollection>>> GetNFTCollectionsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNftCollection>>(_baseClient.GetUri($"nft/collections"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get user balances
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXNft>>> GetNFTBalancesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNft>>(_baseClient.GetUri($"nft/balances"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Create an offer of an NFT
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="price">Price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXNft>> CreateNFTOfferAsync(long nftId, decimal price, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nftId", nftId);
            parameters.AddParameter("price", price.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXNft>(_baseClient.GetUri($"nft/offer"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Buy a NFT
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="price">Price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXNft>> BuyNFTAsync(long nftId, decimal price, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nftId", nftId);
            parameters.AddParameter("price", price.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXNft>(_baseClient.GetUri($"nft/buy"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a new auction
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="initialPrice">Initial price</param>
        /// <param name="reservationPrice">Reservation price</param>
        /// <param name="duration">Duration of the auction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXNft>> CreateAuctionAsync(long nftId, decimal initialPrice, decimal reservationPrice, TimeSpan duration, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nftId", nftId);
            parameters.AddParameter("duration", Math.Floor(duration.TotalSeconds));
            parameters.AddParameter("initialPrice", initialPrice.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("reservationPrice", reservationPrice.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXNft>(_baseClient.GetUri($"nft/auction"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Edit an auction
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="reservationPrice">Reservation price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXNft>> EditAuctionAsync(long nftId, decimal reservationPrice, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nftId", nftId);
            parameters.AddParameter("reservationPrice", reservationPrice.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXNft>(_baseClient.GetUri($"nft/edit_auction"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel an auction
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXNft>> CancelAuctionAsync(long nftId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nftId", nftId);
            return await _baseClient.SendFTXRequest<FTXNft>(_baseClient.GetUri($"nft/cancel_auction"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get bids
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXNft>>> GetBidsAsync(CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNft>>(_baseClient.GetUri($"nft/bids"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Place a bid on an NFT auction
        /// </summary>
        /// <param name="nftId">NFT id</param>
        /// <param name="price">Bid price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXNft>> PlaceBidAsync(long nftId, decimal price, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nftId", nftId);
            parameters.AddParameter("price", price.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXNft>(_baseClient.GetUri($"nft/edit_auction"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get NFT deposits
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXNftDeposit>>> GetNFTDepositsAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            _baseClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNftDeposit>>(_baseClient.GetUri($"nft/deposits"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get NFT withdrawals
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXNftWithdrawal>>> GetNFTWithdrawalsAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            _baseClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNftWithdrawal>>(_baseClient.GetUri($"nft/withdrawals"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get NFT trades
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<FTXNftUserTrade>>> GetNFTUserTradesAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            _baseClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNftUserTrade>>(_baseClient.GetUri($"nft/fills"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Redeem a NFT
        /// </summary>
        /// <param name="nftId">NFT id to redeem</param>
        /// <param name="address">Address to redeem to</param>
        /// <param name="notes">Notes</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXNftRedeem>> RedeemNftAsync(long nftId, string address, string? notes = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nftId", nftId);
            parameters.AddParameter("address", address);
            parameters.AddOptionalParameter("notes", notes);
            return await _baseClient.SendFTXRequest<FTXNftRedeem>(_baseClient.GetUri($"nft/redeem"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get NFT gallery
        /// </summary>
        /// <param name="galleryId">Id of the gallery</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXNftGallery>> GetNftGalleryAsync(long galleryId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXNftGallery>(_baseClient.GetUri($"nft/gallery/" + galleryId), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get NFT gallery settings
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<FTXNftGallerySettings>> GetGallerySettingsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXNftGallerySettings>(_baseClient.GetUri($"nft/gallery_settings"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Edit NFT gallery settings
        /// </summary>
        /// <param name="isPublic">Gallery is public or not</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult> EditGallerySettingsAsync(bool isPublic, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("public", isPublic);            
            return await _baseClient.SendFTXRequest(_baseClient.GetUri($"nft/gallery_settings"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }
    }
}
