using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Infrastructure.Authentication.Instagram
{
    internal sealed class InstagramService : IInstagramService
    {
        private readonly HttpClient _httpClient;
        private readonly InstagramOptions _instagramOptions;
        private readonly ILogger<InstagramService> _logger;

        public InstagramService(
            HttpClient httpClient,
            IOptions<InstagramOptions> instagramOptions,
            ILogger<InstagramService> logger
        )
        {
            this._httpClient = httpClient;
            this._instagramOptions = instagramOptions.Value;
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
            this._logger.LogInformation("Attempting to get Instagram user info.");

            InstagramBusinessAccountResponse instagramBusinessAccount =
                await this.GetInstagramBusinessAccountAsync(
                    accessToken,
                    facebookPageId,
                    cancellationToken
                );

            string instagramUserMetadataRequestUrl =
                this._instagramOptions.BaseUrl
                + (instagramBusinessAccount.InstagramAccount?.Id)
                + $"?fields=business_discovery.username({instagramBusinessAccount.InstagramAccount?.UserName})"
                + "{username,name,ig_id,id,profile_picture_url,biography,followers_count,media_count}"
                + $"&access_token={accessToken}";

            HttpResponseMessage instagramUserMetadataResponse = await this._httpClient.GetAsync(
                instagramUserMetadataRequestUrl,
                cancellationToken
            );
            if (!instagramUserMetadataResponse.IsSuccessStatusCode)
            {
                this._logger.LogWarning(
                    "Failed to retrieve Instagram user info. Status code: {StatusCode}",
                    instagramUserMetadataResponse.StatusCode
                );
                return Result.Failure<InstagramUserInfo>(UserErrors.InvalidCredentials);
            }

            string content = await instagramUserMetadataResponse.Content.ReadAsStringAsync(
                cancellationToken
            );

            InstagramUserInfo? instagramUserInfo = JsonSerializer.Deserialize<InstagramUserInfo>(
                content
            );

            if (instagramUserInfo != null)
            {
                instagramUserInfo.FacebookPageId = facebookPageId;
                this._logger.LogInformation(
                    "Successfully retrieved Instagram user info for user {Username}",
                    instagramUserInfo.BusinessDiscovery.Username
                );
            }
            else
            {
                this._logger.LogWarning("Failed to deserialize Instagram user info.");
            }

            return instagramUserInfo;
        }

        public async Task<Result<InstagramUserInfo>> GetUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        )
        {
            this._logger.LogInformation("Attempting to get Instagram user info.");

            Result<string> facebookPageIdResult = await this.GetFacebookPageIdAsync(
                accessToken,
                cancellationToken
            );
            if (facebookPageIdResult.IsFailure)
            {
                this._logger.LogWarning(
                    "Failed to retrieve Facebook page ID. Error: {Error}",
                    facebookPageIdResult.Error
                );
                return Result.Failure<InstagramUserInfo>(facebookPageIdResult.Error);
            }
            string facebookPageId = facebookPageIdResult.Value;

            InstagramBusinessAccountResponse instagramBusinessAccount =
                await this.GetInstagramBusinessAccountAsync(
                    accessToken,
                    facebookPageId,
                    cancellationToken
                );

            string instagramUserMetadataRequestUrl =
                this._instagramOptions.BaseUrl
                + (instagramBusinessAccount.InstagramAccount?.Id)
                + $"?fields=business_discovery.username({instagramBusinessAccount.InstagramAccount?.UserName})"
                + "{username,name,ig_id,id,profile_picture_url,biography,followers_count,media_count}"
                + $"&access_token={accessToken}";

            HttpResponseMessage instagramUserMetadataResponse = await this._httpClient.GetAsync(
                instagramUserMetadataRequestUrl,
                cancellationToken
            );
            if (!instagramUserMetadataResponse.IsSuccessStatusCode)
            {
                this._logger.LogWarning(
                    "Failed to retrieve Instagram user info. Status code: {StatusCode}",
                    instagramUserMetadataResponse.StatusCode
                );
                return Result.Failure<InstagramUserInfo>(UserErrors.InvalidCredentials);
            }

            string content = await instagramUserMetadataResponse.Content.ReadAsStringAsync(
                cancellationToken
            );

            InstagramUserInfo? instagramUserInfo = JsonSerializer.Deserialize<InstagramUserInfo>(
                content
            );

            if (instagramUserInfo != null)
            {
                instagramUserInfo.FacebookPageId = facebookPageId;
                this._logger.LogInformation(
                    "Successfully retrieved Instagram user info for user {Username}",
                    instagramUserInfo.BusinessDiscovery.Username
                );
            }
            else
            {
                this._logger.LogWarning("Failed to deserialize Instagram user info.");
            }

            return instagramUserInfo;
        }

        private async Task<Result<string>> GetFacebookPageIdAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        )
        {
            this._logger.LogInformation("Attempting to get Facebook page ID.");

            FacebookUserInfo? userInfo = await this.GetFacebookUserInfoAsync(
                accessToken,
                cancellationToken
            );
            if (userInfo == null)
            {
                this._logger.LogWarning("Failed to retrieve Facebook user info.");
                return Result.Failure<string>(InstagramAccountErrors.FailedToGetFacebookPage);
            }

            string accountsUrl =
                $"{this._instagramOptions.BaseUrl}/{userInfo.Id}/accounts?access_token={accessToken}";

            HttpResponseMessage response = await this.SendGetRequestAsync(
                accountsUrl,
                cancellationToken
            );

            FacebookAccountsResponse? accountsData =
                JsonSerializer.Deserialize<FacebookAccountsResponse>(
                    await response.Content.ReadAsStringAsync(cancellationToken)
                );

            if (accountsData?.Data.Length == 1)
            {
                this._logger.LogInformation("Successfully retrieved Facebook page ID.");
                return Result.Success(accountsData.Data[0].Id);
            }
            else
            {
                this._logger.LogWarning("Incorrect number of Facebook pages found.");
                return Result.Failure<string>(InstagramAccountErrors.IncorrectFacebookPagesCount);
            }
        }

        private async Task<FacebookUserInfo?> GetFacebookUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        )
        {
            this._logger.LogInformation("Attempting to get Facebook user info.");

            string userInfoUrl = $"{this._instagramOptions.BaseUrl}me?access_token={accessToken}";
            HttpResponseMessage response = await this.SendGetRequestAsync(
                userInfoUrl,
                cancellationToken
            );

            if (response.IsSuccessStatusCode)
            {
                this._logger.LogInformation("Successfully retrieved Facebook user info.");
                return JsonSerializer.Deserialize<FacebookUserInfo>(
                    await response.Content.ReadAsStringAsync(cancellationToken)
                );
            }
            else
            {
                this._logger.LogWarning(
                    "Failed to retrieve Facebook user info. Status code: {StatusCode}",
                    response.StatusCode
                );
                return null;
            }
        }

        private async Task<InstagramBusinessAccountResponse> GetInstagramBusinessAccountAsync(
            string accessToken,
            string facebookPageId,
            CancellationToken cancellationToken = default
        )
        {
            this._logger.LogInformation("Attempting to get Instagram business account.");

            string businessAccountUrl =
                $"{this._instagramOptions.BaseUrl}{facebookPageId}?fields=instagram_business_account{{id,username}}&access_token={accessToken}";

            HttpResponseMessage response = await this.SendGetRequestAsync(
                businessAccountUrl,
                cancellationToken
            );

            if (response.IsSuccessStatusCode)
            {
                this._logger.LogInformation("Successfully retrieved Instagram business account.");
            }
            else
            {
                this._logger.LogWarning(
                    "Failed to retrieve Instagram business account. Status code: {StatusCode}",
                    response.StatusCode
                );
            }

            return JsonSerializer.Deserialize<InstagramBusinessAccountResponse>(
                await response.Content.ReadAsStringAsync(cancellationToken)
            );
        }

        public async Task<string> GetUserPosts(
            string accessToken,
            string instagramAccountId,
            int limit,
            string cursorType,
            string cursor,
            CancellationToken cancellationToken = default
        )
        {
            string postsUrl =
                $"{this._instagramOptions.BaseUrl}{instagramAccountId}/media"
                + "?fields=id,caption,media_type,media_url,permalink,thumbnail_url,timestamp"
                + $"&limit={limit}"
                + $"&access_token={accessToken}";

            HttpResponseMessage response = await this.SendGetRequestAsync(
                postsUrl,
                cancellationToken
            );

            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            Console.WriteLine(content);

            throw new NotImplementedException();
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
