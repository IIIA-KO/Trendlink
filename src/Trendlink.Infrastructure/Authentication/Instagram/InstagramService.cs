using System.Text.Json;
using Trendlink.Application.Abstractions.Authentication;
using AccessTokenResponse = Trendlink.Application.Users.LogInUser.AccessTokenResponse;

namespace Trendlink.Infrastructure.Authentication.Instagram
{
    internal sealed class InstagramService : IInstagramService
    {
        private readonly HttpClient _httpClient;
        private readonly InstagramOptions _instagramOptions;

        public InstagramService(HttpClient httpClient, InstagramOptions instagramOptions)
        {
            this._httpClient = httpClient;
            this._instagramOptions = instagramOptions;
        }

        public async Task<AccessTokenResponse> GetAccessTokenAsync(
            string code,
            CancellationToken cancellationToken = default
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "client_id", this._instagramOptions.ClientId },
                { "client_secret", this._instagramOptions.ClientSecret },
                { "grant_type", "authorization_code" },
                { "redirect_uri", this._instagramOptions.RedirectUri },
                { "code", code }
            };

            using var content = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response = await this._httpClient.PostAsync(
                this._instagramOptions.TokenUrl,
                content,
                cancellationToken
            );
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync(
                    cancellationToken
                );
                return JsonSerializer.Deserialize<AccessTokenResponse>(responseContent);
            }

            return null;
        }

        public async Task<InstagramUserInfo?> GetUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        )
        {
            string requestUri = $"{this._instagramOptions.UserInfoUrl}&access_token={accessToken}";

            HttpResponseMessage response = await this._httpClient.GetAsync(
                requestUri,
                cancellationToken
            );
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync(
                    cancellationToken
                );
                return JsonSerializer.Deserialize<InstagramUserInfo>(responseContent);
            }

            return null;
        }
    }
}
