using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;
using Trendlink.Infrastructure.Instagram.Models.Accounts;

namespace Trendlink.Infrastructure.Instagram
{
    internal sealed class InstagramAccountsService : IInstagramAccountsService
    {
        private readonly HttpClient _httpClient;
        private readonly InstagramOptions _instagramOptions;
        private readonly ILogger<InstagramAccountsService> _logger;

        public InstagramAccountsService(
            HttpClient httpClient,
            IOptions<InstagramOptions> instagramOptions,
            ILogger<InstagramAccountsService> logger
        )
        {
            this._httpClient = httpClient;
            this._instagramOptions = instagramOptions.Value;
            this._logger = logger;
        }

        public async Task<Result<InstagramUserInfo>> GetUserInfoAsync(
            string accessToken,
            string facebookPageId,
            string instagramUsername,
            CancellationToken cancellationToken = default
        )
        {
            this._logger.LogInformation("Attempting to get Instagram user info.");

            Result<string> adAccountIdResult = await this.GetAdAccountIdAsync(
                accessToken,
                cancellationToken
            );
            if (adAccountIdResult.IsFailure)
            {
                this._logger.LogWarning(
                    "Failed to retrieve Advertisement Account ID. Error: {Error}",
                    adAccountIdResult.Error
                );
                return Result.Failure<InstagramUserInfo>(adAccountIdResult.Error);
            }
            string adAccountId = adAccountIdResult.Value;

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

            InstagramUserInfo instagramUserInfo = JsonSerializer.Deserialize<InstagramUserInfo>(
                content
            );

            if (instagramUserInfo != null)
            {
                instagramUserInfo.FacebookPageId = facebookPageId;
                instagramUserInfo.AdAccountId = adAccountId;
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

            Result<string> adAccountIdResult = await this.GetAdAccountIdAsync(
                accessToken,
                cancellationToken
            );
            if (adAccountIdResult.IsFailure)
            {
                this._logger.LogWarning(
                    "Failed to retrieve Advertisement Account ID. Error: {Error}",
                    adAccountIdResult.Error
                );
                return Result.Failure<InstagramUserInfo>(adAccountIdResult.Error);
            }
            string adAccountId = adAccountIdResult.Value;

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
                instagramUserInfo.AdAccountId = adAccountId;
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

        private async Task<Result<string>> GetAdAccountIdAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        )
        {
            this._logger.LogInformation("Attempting to get Advertisement Account ID.");

            FacebookUserInfo? userInfo = await this.GetFacebookUserInfoAsync(
                accessToken,
                cancellationToken
            );
            if (userInfo == null)
            {
                this._logger.LogWarning("Failed to retrieve Facebook user info.");
                return Result.Failure<string>(InstagramAccountErrors.FailedToGetFacebookPage);
            }

            string adAccountIdUrl =
                $"{this._instagramOptions.BaseUrl}{userInfo.Id}/adaccounts?access_token={accessToken}";

            HttpResponseMessage response = await this.SendGetRequestAsync(
                adAccountIdUrl,
                cancellationToken
            );

            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                this._logger.LogWarning(
                    "Failed to retrieve Advertisement Account ID. Status code: {StatusCode}",
                    response.StatusCode
                );

                return Result.Failure<string>(
                    InstagramAccountErrors.FailedToGetAdvertisementAccountId
                );
            }

            JsonElement jsonObject = JsonDocument.Parse(content).RootElement;

            this._logger.LogInformation("Successfully retrieved Advertisement Account ID.");

            return jsonObject
                .GetProperty("data")
                .EnumerateArray()
                .LastOrDefault()
                .GetProperty("id")
                .GetString();
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

            if (!response.IsSuccessStatusCode)
            {
                this._logger.LogWarning(
                    "Failed to retrieve Facebook user info. Status code: {StatusCode}",
                    response.StatusCode
                );
                return null;
            }

            this._logger.LogInformation("Successfully retrieved Facebook user info.");
            return JsonSerializer.Deserialize<FacebookUserInfo>(
                await response.Content.ReadAsStringAsync(cancellationToken)
            );
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

        private async Task<HttpResponseMessage> SendGetRequestAsync(
            string url,
            CancellationToken cancellationToken
        )
        {
            return await this._httpClient.GetAsync(url, cancellationToken);
        }
    }
}
