using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;

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
            using var accessTokenRequestContent = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "client_id", this._instagramOptions.ClientId },
                    { "client_secret", this._instagramOptions.ClientSecret },
                    { "grant_type", "authorization_code" },
                    { "redirect_uri", this._instagramOptions.RedirectUri },
                    { "code", code }
                }
            );

            HttpResponseMessage response = await this._httpClient.PostAsync(
                this._instagramOptions.TokenUrl,
                accessTokenRequestContent,
                cancellationToken
            );

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync(cancellationToken);

                return JsonSerializer.Deserialize<FacebookTokenResponse>(content);
            }

            return null;
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
            FacebookUserInfo? facebookUserInfo = await this.GetFacebookUserInfoAsync(
                accessToken,
                cancellationToken
            );
            if (facebookUserInfo is null)
            {
                return Result.Failure<string>(UserErrors.FailedToGetFacebookPage);
            }

            string facebookUserAccountsRequestUrl =
                $"{this._instagramOptions.BaseUrl}/{facebookUserInfo.Id}/accounts?access_token={accessToken}";

            HttpResponseMessage facebookUserAccountsResponse = await this._httpClient.GetAsync(
                facebookUserAccountsRequestUrl,
                cancellationToken
            );
            if (!facebookUserAccountsResponse.IsSuccessStatusCode)
            {
                return Result.Failure<string>(UserErrors.FailedToGetFacebookPage);
            }

            string accountsResponse = await facebookUserAccountsResponse.Content.ReadAsStringAsync(
                cancellationToken
            );

            FacebookAccountsResponse? accountsData =
                JsonSerializer.Deserialize<FacebookAccountsResponse>(accountsResponse);
            if (accountsData is null)
            {
                return Result.Failure<string>(UserErrors.FailedToGetFacebookPage);
            }

            if (accountsData.Data.Length == 0)
            {
                return Result.Failure<string>(UserErrors.FailedToGetFacebookPage);
            }

            if (accountsData.Data.Length > 1)
            {
                return Result.Failure<string>(UserErrors.MoreThanOneFacebookPage);
            }

            return accountsData.Data[0].Id;
        }

        private async Task<FacebookUserInfo?> GetFacebookUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        )
        {
            string facebookUserInforRequestUrl =
                $"{this._instagramOptions.BaseUrl}me?access_token={accessToken}";

            HttpResponseMessage facebookUserInfoResponse = await this._httpClient.GetAsync(
                facebookUserInforRequestUrl,
                cancellationToken
            );
            if (!facebookUserInfoResponse.IsSuccessStatusCode)
            {
                return null;
            }

            string facebookUserInfoResponseContent =
                await facebookUserInfoResponse.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<FacebookUserInfo>(facebookUserInfoResponseContent);
        }

        private async Task<InstagramBusinessAccountResponse> GetInstagramBusinessAccountAsync(
            string accessToken,
            string facebookPageId,
            CancellationToken cancellationToken = default
        )
        {
            string instagramBusinessAccountRequestUrl =
                $"{this._instagramOptions.BaseUrl}{facebookPageId}?fields=instagram_business_account{{id,username}}&access_token={accessToken}";

            HttpResponseMessage instagramBusinessAccountResponse = await this._httpClient.GetAsync(
                instagramBusinessAccountRequestUrl,
                cancellationToken
            );

            string content = await instagramBusinessAccountResponse.Content.ReadAsStringAsync(
                cancellationToken
            );

            InstagramBusinessAccountResponse? instagramBusinessAccount =
                JsonSerializer.Deserialize<InstagramBusinessAccountResponse>(content);

            if (
                instagramBusinessAccount is null
                || instagramBusinessAccount.InstagramAccount is null
            )
            {
                return null;
            }

            return instagramBusinessAccount;
        }
    }
}
