using System.Text.Json;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Authentication;

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

        public async Task<(string?, long?)> GetAccessTokenAsync(
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

                InstagramTokenResponse? tokenResponse =
                    JsonSerializer.Deserialize<InstagramTokenResponse>(content);

                return (tokenResponse?.AccessToken, tokenResponse?.UserId);
            }

            return (null, null);
        }

        public async Task<InstagramUserInfo?> GetUserInfoAsync(
            string accessToken,
            long instagramUserId,
            CancellationToken cancellationToken = default
        )
        {
            string requestUri =
                $"{this._instagramOptions.UserInfoUrl}{instagramUserId}?fields=id,username&access_token={accessToken}";

            HttpResponseMessage response = await this._httpClient.GetAsync(
                requestUri,
                cancellationToken
            );

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync(cancellationToken);

                return JsonSerializer.Deserialize<InstagramUserInfo>(content);
            }

            return null;
        }
    }
}
