using System.Text.Json;
using Microsoft.Extensions.Logging;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.InstagramBusinessAccount;
using Trendlink.Infrastructure.Instagram.Abstraction;
using Trendlink.Infrastructure.Instagram.Models.Accounts;

namespace Trendlink.Infrastructure.Instagram
{
    internal class FacebookService : IFacebookService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FacebookService> _logger;

        public FacebookService(HttpClient httpClient, ILogger<FacebookService> logger)
        {
            this._httpClient = httpClient;
            this._logger = logger;
        }

        public async Task<Result<string>> GetFacebookPageIdAsync(
            string accessToken,
            CancellationToken cancellationToken
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

            string url = BuildUrl(
                $"{userInfo.Id}/accounts",
                new Dictionary<string, string> { { "access_token", accessToken } }
            );
            FacebookAccountsResponse? accountsData = await this.GetAsync<FacebookAccountsResponse>(
                url,
                cancellationToken
            );

            if (accountsData?.Data.Length == 1)
            {
                this._logger.LogInformation("Successfully retrieved Facebook page ID.");
                return Result.Success(accountsData.Data[0].Id);
            }

            this._logger.LogWarning("Incorrect number of Facebook pages found.");
            return Result.Failure<string>(InstagramAccountErrors.IncorrectFacebookPagesCount);
        }

        public async Task<Result<string>> GetAdAccountIdAsync(
            string accessToken,
            CancellationToken cancellationToken
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

            string url = BuildUrl(
                $"{userInfo.Id}/adaccounts",
                new Dictionary<string, string> { { "access_token", accessToken } }
            );
            JsonElement response = await this.GetAsync<JsonElement>(url, cancellationToken);

            return response
                .GetProperty("data")
                .EnumerateArray()
                .LastOrDefault()
                .GetProperty("id")
                .GetString();
        }

        public async Task<FacebookUserInfo?> GetFacebookUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken
        )
        {
            this._logger.LogInformation("Attempting to get Facebook user info.");

            string url = BuildUrl(
                "me",
                new Dictionary<string, string> { { "access_token", accessToken } }
            );
            return await this.GetAsync<FacebookUserInfo>(url, cancellationToken);
        }

        private static string BuildUrl(string path, Dictionary<string, string> parameters)
        {
            string queryString = string.Join("&", parameters.Select(kv => $"{kv.Key}={kv.Value}"));
            return $"{path}?{queryString}";
        }

        protected async Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await this._httpClient.GetAsync(url, cancellationToken);
            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonSerializer.Deserialize<T>(content);
        }
    }
}
