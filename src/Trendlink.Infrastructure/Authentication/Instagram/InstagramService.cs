using System.Text.Json;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Infrastructure.Authentication.Instagram
{
    internal sealed class InstagramService : IInstagramService
    {
        private readonly HttpClient _httpClient;
        private readonly InstagramOptions _instagramOptions;

        public InstagramService(HttpClient httpClient, IOptions<InstagramOptions> instagramOptions)
        {
            this._httpClient = httpClient;
            this._instagramOptions = instagramOptions.Value;
        }

        public async Task<FacebookTokenResponse?> GetAccessTokenAsync(
            string code,
            CancellationToken cancellationToken = default
        )
        {
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

            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<FacebookTokenResponse>(
                    await response.Content.ReadAsStringAsync(cancellationToken)
                )
                : null;
        }

        public async Task<Result<InstagramUserInfo>> GetUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        )
        {
            Result<string> pageIdResult = await this.GetFacebookPageIdAsync(
                accessToken,
                cancellationToken
            );
            if (pageIdResult.IsFailure)
            {
                return Result.Failure<InstagramUserInfo>(pageIdResult.Error);
            }

            InstagramBusinessAccountResponse instagramBusinessAccount =
                await this.GetInstagramBusinessAccountAsync(
                    accessToken,
                    pageIdResult.Value,
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
                return Result.Failure<InstagramUserInfo>(UserErrors.InvalidCredentials);
            }

            string content = await instagramUserMetadataResponse.Content.ReadAsStringAsync(
                cancellationToken
            );

            return JsonSerializer.Deserialize<InstagramUserInfo>(content);
        }

        private async Task<Result<string>> GetFacebookPageIdAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        )
        {
            FacebookUserInfo? userInfo = await this.GetFacebookUserInfoAsync(
                accessToken,
                cancellationToken
            );
            if (userInfo == null)
            {
                return Result.Failure<string>(UserErrors.FailedToGetFacebookPage);
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

            return accountsData?.Data.Length == 1
                ? Result.Success(accountsData.Data[0].Id)
                : Result.Failure<string>(UserErrors.IncorrectFacebookPagesCount);
        }

        private async Task<FacebookUserInfo?> GetFacebookUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        )
        {
            string userInfoUrl = $"{this._instagramOptions.BaseUrl}me?access_token={accessToken}";
            HttpResponseMessage response = await this.SendGetRequestAsync(
                userInfoUrl,
                cancellationToken
            );

            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<FacebookUserInfo>(
                    await response.Content.ReadAsStringAsync(cancellationToken)
                )
                : null;
        }

        private async Task<InstagramBusinessAccountResponse> GetInstagramBusinessAccountAsync(
            string accessToken,
            string facebookPageId,
            CancellationToken cancellationToken = default
        )
        {
            string businessAccountUrl =
                $"{this._instagramOptions.BaseUrl}{facebookPageId}?fields=instagram_business_account{{id,username}}&access_token={accessToken}";
            HttpResponseMessage response = await this.SendGetRequestAsync(
                businessAccountUrl,
                cancellationToken
            );

            return JsonSerializer.Deserialize<InstagramBusinessAccountResponse>(
                await response.Content.ReadAsStringAsync(cancellationToken)
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
