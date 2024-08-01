using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Authentication;

namespace Trendlink.Infrastructure.Authentication
{
    public sealed class GoogleService : IGoogleService
    {
        private readonly HttpClient _httpClient;
        private readonly GoogleOptions _googleOptions;

        public GoogleService(HttpClient httpClient, IOptions<GoogleOptions> googleOptions)
        {
            this._httpClient = httpClient;
            this._googleOptions = googleOptions.Value;
        }

        public async Task<string?> GetAccessTokenAsync(
            string code,
            CancellationToken cancellationToken
        )
        {
            using var accessTokenRequestContent = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "code", code },
                    { "client_id", this._googleOptions.ClientId },
                    { "client_secret", this._googleOptions.ClientSecret },
                    { "redirect_uri", this._googleOptions.RedirectUri },
                    { "grant_type", "authorization_code" }
                }
            );

            using var request = new HttpRequestMessage(
                HttpMethod.Post,
                this._googleOptions.TokenUrl
            )
            {
                Content = accessTokenRequestContent
            };

            request.Content.Headers.ContentType = new MediaTypeHeaderValue(
                "application/x-www-form-urlencoded"
            );

            HttpResponseMessage response = await this._httpClient.SendAsync(
                request,
                cancellationToken
            );

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync(cancellationToken);
                GoogleTokenResponse? tokenResponse =
                    JsonSerializer.Deserialize<GoogleTokenResponse>(content);
                return tokenResponse?.AccessToken;
            }

            return null;
        }

        public async Task<UserInfo?> GetUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken
        )
        {
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                accessToken
            );

            HttpResponseMessage response = await this._httpClient.GetAsync(
                this._googleOptions.UserInfoUrl,
                cancellationToken
            );

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync(cancellationToken);
                return JsonSerializer.Deserialize<UserInfo>(content);
            }

            return null;
        }
    }
}
