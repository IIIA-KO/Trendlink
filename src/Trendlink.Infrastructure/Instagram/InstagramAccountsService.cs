using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Domain.Abstraction;
using Trendlink.Infrastructure.Instagram.Abstraction;
using Trendlink.Infrastructure.Instagram.Models.Accounts;

namespace Trendlink.Infrastructure.Instagram
{
    internal sealed class InstagramAccountsService : InstagramBaseService, IInstagramAccountsService
    {
        private readonly IFacebookService _facebookService;
        private readonly ILogger<InstagramAccountsService> _logger;

        public InstagramAccountsService(
            HttpClient httpClient,
            IOptions<InstagramOptions> instagramOptions,
            IFacebookService facebookService,
            ILogger<InstagramAccountsService> logger
        )
            : base(httpClient, instagramOptions)
        {
            this._facebookService = facebookService;
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

            Result<string> adAccountIdResult = await this._facebookService.GetAdAccountIdAsync(
                accessToken,
                cancellationToken
            );
            if (adAccountIdResult.IsFailure)
            {
                return this.LogAndReturnFailure<InstagramUserInfo>(adAccountIdResult.Error);
            }

            InstagramBusinessAccountResponse instagramBusinessAccount =
                await this.GetInstagramBusinessAccountAsync(
                    accessToken,
                    facebookPageId,
                    cancellationToken
                );
            if (instagramBusinessAccount?.InstagramAccount == null)
            {
                return Result.Failure<InstagramUserInfo>(Error.NullValue);
            }

            if (string.IsNullOrEmpty(instagramUsername))
            {
                instagramUsername = instagramBusinessAccount.InstagramAccount.UserName;
            }

            InstagramUserInfo? instagramUserInfo = await this.FetchInstagramUserInfo(
                instagramBusinessAccount.InstagramAccount.Id,
                accessToken,
                instagramUsername,
                cancellationToken
            );
            if (instagramUserInfo != null)
            {
                instagramUserInfo.FacebookPageId = facebookPageId;
                instagramUserInfo.AdAccountId = adAccountIdResult.Value;
                this._logger.LogInformation(
                    "Successfully retrieved Instagram user info for user {Username}",
                    instagramUserInfo.BusinessDiscovery.Username
                );
            }

            return instagramUserInfo;
        }

        public async Task<Result<InstagramUserInfo>> GetUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        )
        {
            this._logger.LogInformation("Attempting to get Instagram user info.");

            Result<string> facebookPageIdResult =
                await this._facebookService.GetFacebookPageIdAsync(accessToken, cancellationToken);
            if (facebookPageIdResult.IsFailure)
            {
                return this.LogAndReturnFailure<InstagramUserInfo>(facebookPageIdResult.Error);
            }

            return await this.GetUserInfoAsync(
                accessToken,
                facebookPageIdResult.Value,
                string.Empty,
                cancellationToken
            );
        }

        private async Task<InstagramBusinessAccountResponse> GetInstagramBusinessAccountAsync(
            string accessToken,
            string facebookPageId,
            CancellationToken cancellationToken
        )
        {
            this._logger.LogInformation("Attempting to get Instagram business account.");

            var parameters = new Dictionary<string, string>
            {
                { "fields", "instagram_business_account{id,username}" },
                { "access_token", accessToken }
            };

            string url = this.BuildUrl(facebookPageId, parameters);
            return await this.GetAsync<InstagramBusinessAccountResponse>(url, cancellationToken);
        }

        private async Task<InstagramUserInfo?> FetchInstagramUserInfo(
            string accountId,
            string accessToken,
            string username,
            CancellationToken cancellationToken
        )
        {
            var parameters = new Dictionary<string, string>
            {
                {
                    "fields",
                    $"business_discovery.username({username}){{username,name,ig_id,id,profile_picture_url,biography,followers_count,media_count}}"
                },
                { "access_token", accessToken }
            };

            string url = this.BuildUrl(accountId, parameters);

            return await this.GetAsync<InstagramUserInfo>(url, cancellationToken);
        }

        private Result<T> LogAndReturnFailure<T>(Error error)
        {
            this._logger.LogWarning("Error: {Error}", error);
            return Result.Failure<T>(error);
        }
    }
}
