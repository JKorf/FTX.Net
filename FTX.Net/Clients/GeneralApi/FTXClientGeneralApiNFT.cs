using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Objects.Models.NFT;
using FTX.Net.Interfaces.Clients.GeneralApi;

namespace FTX.Net.Clients.GeneralApi
{
    /// <summary>
    /// NFT endpoints
    /// </summary>
    public class FTXClientGeneralApiNFT : IFTXClientGeneralApiNft
    {
        private readonly FTXClientGeneralApi _baseClient;

        internal FTXClientGeneralApiNFT(FTXClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXNft>>> GetNftsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNft>>(_baseClient.GetUri("nft/nfts"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXNft>> GetNftAsync(long nftId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXNft>(_baseClient.GetUri("nft/nft/" + nftId), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXNftTrade>>> GetNftTradesAsync(long nftId, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNftTrade>>(_baseClient.GetUri($"nft/nft/{nftId}/trades"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXNftTradeAll>>> GetNftAllTradesAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNftTradeAll>>(_baseClient.GetUri("nft/all_trades"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<NFTUserInfo>> GetNftUserInfoAsync(long nftId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<NFTUserInfo>(_baseClient.GetUri($"nft/nft/{nftId}/account_info"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXNftCollection>>> GetNftCollectionsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNftCollection>>(_baseClient.GetUri("nft/collections"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXNft>>> GetNftBalancesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNft>>(_baseClient.GetUri("nft/balances"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXNft>> CreateNftOfferAsync(long nftId, decimal price, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nftId", nftId);
            parameters.AddParameter("price", price.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXNft>(_baseClient.GetUri("nft/offer"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXNft>> BuyNftAsync(long nftId, decimal price, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nftId", nftId);
            parameters.AddParameter("price", price.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXNft>(_baseClient.GetUri("nft/buy"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXNft>> CreateAuctionAsync(long nftId, decimal initialPrice, decimal reservationPrice, TimeSpan duration, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nftId", nftId);
            parameters.AddParameter("duration", Math.Floor(duration.TotalSeconds));
            parameters.AddParameter("initialPrice", initialPrice.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("reservationPrice", reservationPrice.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXNft>(_baseClient.GetUri("nft/auction"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXNft>> EditAuctionAsync(long nftId, decimal reservationPrice, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nftId", nftId);
            parameters.AddParameter("reservationPrice", reservationPrice.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXNft>(_baseClient.GetUri("nft/edit_auction"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXNft>> CancelAuctionAsync(long nftId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nftId", nftId);
            return await _baseClient.SendFTXRequest<FTXNft>(_baseClient.GetUri("nft/cancel_auction"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXNft>>> GetBidsAsync(CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNft>>(_baseClient.GetUri("nft/bids"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXNft>> PlaceBidAsync(long nftId, decimal price, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nftId", nftId);
            parameters.AddParameter("price", price.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXNft>(_baseClient.GetUri("nft/edit_auction"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXNftDeposit>>> GetNftDepositsAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNftDeposit>>(_baseClient.GetUri("nft/deposits"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXNftWithdrawal>>> GetNftWithdrawalsAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNftWithdrawal>>(_baseClient.GetUri("nft/withdrawals"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXNftUserTrade>>> GetNftUserTradesAsync(DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            FTXClient.AddFilter(parameters, startTime, endTime);
            return await _baseClient.SendFTXRequest<IEnumerable<FTXNftUserTrade>>(_baseClient.GetUri("nft/fills"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXNftRedeem>> RedeemNftAsync(long nftId, string address, string? notes = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("nftId", nftId);
            parameters.AddParameter("address", address);
            parameters.AddOptionalParameter("notes", notes);
            return await _baseClient.SendFTXRequest<FTXNftRedeem>(_baseClient.GetUri("nft/redeem"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXNftGallery>> GetNftGalleryAsync(long galleryId, CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXNftGallery>(_baseClient.GetUri("nft/gallery/" + galleryId), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXNftGallerySettings>> GetGallerySettingsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<FTXNftGallerySettings>(_baseClient.GetUri("nft/gallery_settings"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> EditGallerySettingsAsync(bool isPublic, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("public", isPublic);
            return await _baseClient.SendFTXRequest(_baseClient.GetUri("nft/gallery_settings"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }
    }
}
