using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Users.Instagarm.GetUserAudienceGenderPercentage;
using Trendlink.Application.Users.Instagarm.GetUserPosts;
using Trendlink.Domain.Abstraction;
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
        private readonly ILogger<InstagramService> _logger;

        public InstagramService(
            HttpClient httpClient,
            IOptions<InstagramOptions> instagramOptions,
            IInstagramPostsService instagramPostsService,
            IInstagramAccountsService instagramAccountsService,
            IInstagramAudienceService instagramAudienceService,
            ILogger<InstagramService> logger
        )
        {
            this._httpClient = httpClient;
            this._instagramOptions = instagramOptions.Value;
            this._instagramPostsService = instagramPostsService;
            this._instagramAccountsService = instagramAccountsService;
            this._instagramAudienceService = instagramAudienceService;
            this._logger = logger;
        }

        public async Task<FacebookTokenResponse?> GetAccessTokenAsync(
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

            if (response.IsSuccessStatusCode)
            {
                this._logger.LogInformation("Successfully retrieved access token.");

                FacebookTokenResponse? result = JsonSerializer.Deserialize<FacebookTokenResponse>(
                    await response.Content.ReadAsStringAsync(cancellationToken)
                );

                DateTimeOffset? expiresAt = await this.GetDataExpirationTime(
                    result!.AccessToken,
                    cancellationToken
                );
                if (!expiresAt.HasValue)
                {
                    return null;
                }

                result.ExpiresAtUtc = expiresAt.Value;

                return result;
            }
            else
            {
                this._logger.LogWarning(
                    "Failed to retrieve access token. Status code: {StatusCode}",
                    response.StatusCode
                );

                string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                Console.WriteLine(errorContent);

                return null;
            }
        }

        public async Task<FacebookTokenResponse?> RenewAccessTokenAsync(
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

            if (response.IsSuccessStatusCode)
            {
                this._logger.LogInformation("Successfully retrieved access token.");

                FacebookTokenResponse? result = JsonSerializer.Deserialize<FacebookTokenResponse>(
                    await response.Content.ReadAsStringAsync(cancellationToken)
                );

                DateTimeOffset? expiresAt = await this.GetDataExpirationTime(
                    result!.AccessToken,
                    cancellationToken
                );
                if (!expiresAt.HasValue)
                {
                    return null;
                }

                result.ExpiresAtUtc = expiresAt.Value;

                return result;
            }
            else
            {
                this._logger.LogWarning(
                    "Failed to renew access token. Status code: {StatusCode}",
                    response.StatusCode
                );

                string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                Console.WriteLine(errorContent);

                return null;
            }
        }

        private async Task<DateTimeOffset?> GetDataExpirationTime(
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
            if (
                root.TryGetProperty("data", out JsonElement dataElement)
                && dataElement.TryGetProperty(
                    "data_access_expires_at",
                    out JsonElement expiresAtElement
                )
            )
            {
                long expiresAt = expiresAtElement.GetInt64();
                this._logger.LogInformation(
                    "Data access expiration time retrieved successfully: {ExpiresAt}",
                    expiresAt
                );
                return DateTimeOffset.FromUnixTimeSeconds(expiresAt);
            }
            else
            {
                this._logger.LogWarning("Failed to find data_access_expires_at in the response");
                return null;
            }
        }

        public async Task<Result<InstagramUserInfo>> GetUserInfoAsync(
            string accessToken,
            string facebookPageId,
            string instagramUsername,
            CancellationToken cancellationToken = default
        )
        {
            return await this._instagramAccountsService.GetUserInfoAsync(
                accessToken,
                facebookPageId,
                instagramUsername,
                cancellationToken
            );
        }

        public async Task<Result<InstagramUserInfo>> GetUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        )
        {
            return await this._instagramAccountsService.GetUserInfoAsync(
                accessToken,
                cancellationToken
            );
        }

        public async Task<UserPostsResponse> GetUserPostsWithInsights(
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

        public async Task<List<AudienceGenderPercentageResponse>> GetUserAudienceGenderPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        )
        {
            return await this._instagramAudienceService.GetUserAudienceGenderPercentage(
                accessToken,
                instagramAccountId,
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
    }
}
