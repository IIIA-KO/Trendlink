using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Instagarm.Audience.GetAudienceAgeRatio;
using Trendlink.Application.Instagarm.Audience.GetAudienceGenderRatio;
using Trendlink.Application.Instagarm.Audience.GetAudienceLocationPercentage;
using Trendlink.Application.Instagarm.Audience.GetAudienceLocationRatio;
using Trendlink.Application.Instagarm.Audience.GetAudienceReachPercentage;
using Trendlink.Application.Instagarm.Posts.GetPosts;
using Trendlink.Application.Instagarm.Statistics.GetEngagementStatistics;
using Trendlink.Application.Instagarm.Statistics.GetInteractionStatistics;
using Trendlink.Application.Instagarm.Statistics.GetOverviewStatistics;
using Trendlink.Application.Instagarm.Statistics.GetTableStatistics;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;
using Trendlink.Infrastructure.Instagram.Abstraction;
using Trendlink.Infrastructure.Instagram.Models.Audience;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Trendlink.Infrastructure.Instagram
{
    internal sealed class InstagramService : IInstagramService
    {
        private readonly HttpClient _httpClient;
        private readonly InstagramOptions _instagramOptions;
        private readonly IInstagramPostsService _instagramPostsService;
        private readonly IInstagramAccountsService _instagramAccountsService;
        private readonly IInstagramAudienceService _instagramAudienceService;
        private readonly IInstagramStatisticsService _instagramStatisticsService;
        private readonly ILogger<InstagramService> _logger;

        public InstagramService(
            HttpClient httpClient,
            IOptions<InstagramOptions> instagramOptions,
            IInstagramPostsService instagramPostsService,
            IInstagramAccountsService instagramAccountsService,
            IInstagramAudienceService instagramAudienceService,
            IInstagramStatisticsService instagramStatisticsService,
            ILogger<InstagramService> logger
        )
        {
            this._httpClient = httpClient;
            this._instagramOptions = instagramOptions.Value;
            this._instagramPostsService = instagramPostsService;
            this._instagramAccountsService = instagramAccountsService;
            this._instagramAudienceService = instagramAudienceService;
            this._instagramStatisticsService = instagramStatisticsService;
            this._logger = logger;
        }

        public async Task<Result<FacebookTokenResponse>> GetAccessTokenAsync(
            string code,
            CancellationToken cancellationToken = default
        )
        {
            this._logger.LogInformation("Attempting to get access token using authorization code.");

            using var content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "client_id", this._instagramOptions.ClientId },
                    { "client_secret", this._instagramOptions.ClientSecret },
                    { "grant_type", "authorization_code" },
                    { "redirect_uri", this._instagramOptions.RedirectUri },
                    { "code", code }
                }
            );

            HttpResponseMessage response = await this.SendPostRequestAsync(
                this._instagramOptions.TokenUrl,
                content,
                cancellationToken
            );

            if (!response.IsSuccessStatusCode)
            {
                this._logger.LogWarning(
                    "Failed to retrieve access token. Status code: {StatusCode}",
                    response.StatusCode
                );

                return Result.Failure<FacebookTokenResponse>(UserErrors.InvalidCredentials);
            }

            this._logger.LogInformation("Successfully retrieved access token.");

            FacebookTokenResponse? result = JsonSerializer.Deserialize<FacebookTokenResponse>(
                await response.Content.ReadAsStringAsync(cancellationToken)
            );

            Result<DateTimeOffset> expiresAtResult = await this.GetDataExpirationTime(
                result!.AccessToken,
                cancellationToken
            );
            if (expiresAtResult.IsFailure)
            {
                return Result.Failure<FacebookTokenResponse>(expiresAtResult.Error);
            }

            result.ExpiresAtUtc = expiresAtResult.Value;

            return result;
        }

        public async Task<Result<FacebookTokenResponse>> RenewAccessTokenAsync(
            string code,
            CancellationToken cancellationToken = default
        )
        {
            this._logger.LogInformation(
                "Attempting to renew access token using authorization code."
            );

            using var content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "client_id", this._instagramOptions.ClientId },
                    { "client_secret", this._instagramOptions.ClientSecret },
                    { "grant_type", "authorization_code" },
                    { "redirect_uri", this._instagramOptions.RenewRedirectUri },
                    { "code", code }
                }
            );

            HttpResponseMessage response = await this.SendPostRequestAsync(
                this._instagramOptions.TokenUrl,
                content,
                cancellationToken
            );

            if (!response.IsSuccessStatusCode)
            {
                this._logger.LogWarning(
                    "Failed to renew access token. Status code: {StatusCode}",
                    response.StatusCode
                );

                return Result.Failure<FacebookTokenResponse>(UserErrors.InvalidCredentials);
            }

            this._logger.LogInformation("Successfully retrieved access token.");

            FacebookTokenResponse? result = JsonSerializer.Deserialize<FacebookTokenResponse>(
                await response.Content.ReadAsStringAsync(cancellationToken)
            );

            Result<DateTimeOffset> expiresAtResult = await this.GetDataExpirationTime(
                result!.AccessToken,
                cancellationToken
            );
            if (!expiresAtResult.IsFailure)
            {
                return Result.Failure<FacebookTokenResponse>(expiresAtResult.Error);
            }

            result.ExpiresAtUtc = expiresAtResult.Value;

            return result;
        }

        private async Task<Result<DateTimeOffset>> GetDataExpirationTime(
            string accessToken,
            CancellationToken cancellationToken = default
        )
        {
            this._logger.LogInformation("Attempting to get data access expiration time");

            string expiresAtUrl =
                $"{this._instagramOptions.BaseUrl}debug_token?input_token={accessToken}&access_token={accessToken}";

            HttpResponseMessage expiresAtResponse = await this.SendGetRequestAsync(
                expiresAtUrl,
                cancellationToken
            );
            if (!expiresAtResponse.IsSuccessStatusCode)
            {
                this._logger.LogWarning(
                    "Failed to retrieve data access expiration time. Status code: {StatusCode}",
                    expiresAtResponse.StatusCode
                );
                return null;
            }

            string content = await expiresAtResponse.Content.ReadAsStringAsync(cancellationToken);

            using var jsonDocument = JsonDocument.Parse(content);
            JsonElement root = jsonDocument.RootElement;

            if (!TryGetExpirationTime(root, out long expiresAt))
            {
                this._logger.LogWarning("Failed to find data_access_expires_at in the response");

                return Result.Failure<DateTimeOffset>(
                    InstagramAccountErrors.FailedToGetExpirationForAccessToken
                );
            }

            this._logger.LogInformation(
                "Data access expiration time retrieved successfully: {ExpiresAt}",
                expiresAt
            );
            return DateTimeOffset.FromUnixTimeSeconds(expiresAt);
        }

        private static bool TryGetExpirationTime(JsonElement root, out long expiresAt)
        {
            expiresAt = 0;

            if (
                root.TryGetProperty("data", out JsonElement dataElement)
                && dataElement.TryGetProperty(
                    "data_access_expires_at",
                    out JsonElement expiresAtElement
                )
            )
            {
                expiresAt = expiresAtElement.GetInt64();
                return true;
            }

            return false;
        }

        public async Task<Result<InstagramAccount>> GetInstagramAccountAsync(
            UserId userId,
            string accessToken,
            string facebookPageId,
            string instagramUsername,
            CancellationToken cancellationToken = default
        )
        {
            Result<InstagramUserInfo> userInfoResult =
                await this._instagramAccountsService.GetUserInfoAsync(
                    accessToken,
                    facebookPageId,
                    instagramUsername,
                    cancellationToken
                );
            if (userInfoResult.IsFailure)
            {
                return Result.Failure<InstagramAccount>(userInfoResult.Error);
            }

            return userInfoResult.Value.CreateInstagramAccount(userId);
        }

        public async Task<Result<InstagramAccount>> GetInstagramAccountAsync(
            UserId userId,
            string accessToken,
            CancellationToken cancellationToken = default
        )
        {
            Result<InstagramUserInfo> userInfoResult =
                await this._instagramAccountsService.GetUserInfoAsync(
                    accessToken,
                    cancellationToken
                );
            if (userInfoResult.IsFailure)
            {
                return Result.Failure<InstagramAccount>(userInfoResult.Error);
            }

            return userInfoResult.Value.CreateInstagramAccount(userId);
        }

        public async Task<Result<PostsResponse>> GetUserPosts(
            string accessToken,
            string instagramAccountId,
            int limit,
            string cursorType,
            string cursor,
            CancellationToken cancellationToken = default
        )
        {
            return await this._instagramPostsService.GetUserPostsWithInsights(
                accessToken,
                instagramAccountId,
                limit,
                cursorType,
                cursor,
                cancellationToken
            );
        }

        public async Task<Result<GenderRatioResponse>> GetAudienceGenderPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        )
        {
            Result<GenderRatio> result =
                await this._instagramAudienceService.GetAudienceGenderPercentage(
                    accessToken,
                    instagramAccountId,
                    cancellationToken
                );
            if (result.IsFailure)
            {
                return Result.Failure<GenderRatioResponse>(result.Error);
            }

            return new GenderRatioResponse(result.Value.GenderPercentages);
        }

        public async Task<Result<ReachRatioResponse>> GetAudienceReachPercentage(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            Result<ReachRatio> result =
                await this._instagramAudienceService.GetAudienceReachPercentage(
                    request,
                    cancellationToken
                );
            if (result.IsFailure)
            {
                return Result.Failure<ReachRatioResponse>(result.Error);
            }
            ReachRatio reachRatio = result.Value;

            return new ReachRatioResponse(reachRatio.TotalReach, reachRatio.ReachPercentages);
        }

        public async Task<Result<LocationRatioResponse>> GetAudienceLocationPercentage(
            string accessToken,
            string instagramAccountId,
            LocationType locationType,
            CancellationToken cancellationToken = default
        )
        {
            Result<LocationRatio> result =
                await this._instagramAudienceService.GetAudienceTopLocations(
                    accessToken,
                    instagramAccountId,
                    locationType,
                    cancellationToken
                );
            if (result.IsFailure)
            {
                return Result.Failure<LocationRatioResponse>(result.Error);
            }

            return new LocationRatioResponse(result.Value.TopLocationPercentages);
        }

        public async Task<Result<AudienceAgeRatioResponse>> GetAudienceAgePercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        )
        {
            Result<AgeRatio> result =
                await this._instagramAudienceService.GetAudienceAgesPercentage(
                    accessToken,
                    instagramAccountId,
                    cancellationToken
                );
            if (result.IsFailure)
            {
                return Result.Failure<AudienceAgeRatioResponse>(result.Error);
            }

            return new AudienceAgeRatioResponse(result.Value.AgePercentages);
        }

        public async Task<Result<TableStatistics>> GetTableStatistics(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            return await this._instagramStatisticsService.GetTableStatistics(
                request,
                cancellationToken
            );
        }

        public async Task<Result<OverviewStatistics>> GetOverviewStatistics(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            return await this._instagramStatisticsService.GetOverviewStatistics(
                request,
                cancellationToken
            );
        }

        private async Task<HttpResponseMessage> SendPostRequestAsync(
            string url,
            HttpContent content,
            CancellationToken cancellationToken
        )
        {
            return await this._httpClient.PostAsync(url, content, cancellationToken);
        }

        private async Task<HttpResponseMessage> SendGetRequestAsync(
            string url,
            CancellationToken cancellationToken
        )
        {
            return await this._httpClient.GetAsync(url, cancellationToken);
        }

        public async Task<Result<InteractionStatistics>> GetInteractionStatistics(
            string instagramAdAccountId,
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            return await this._instagramStatisticsService.GetInteractionsStatistics(
                instagramAdAccountId,
                request,
                cancellationToken
            );
        }

        public async Task<Result<EngagementStatistics>> GetEngagementStatistics(
            int followersCount,
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            return await this._instagramStatisticsService.GetEngagementStatistics(
                followersCount,
                request,
                cancellationToken
            );
        }
    }
}
