using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;

namespace Trendlink.Infrastructure.Authentication.Google
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

        public async Task<GoogleTokenResponse?> GetAccessTokenAsync(
            string code,
            CancellationToken cancellationToken
        )
        {
            using HttpRequestMessage request = this.CreateAccessTokenRequest(code);

            HttpResponseMessage response = await this._httpClient.SendAsync(
                request,
                cancellationToken
            );

            return await ProcessResponseAsync<GoogleTokenResponse>(response, cancellationToken);
        }

        public async Task<GoogleUserInfo?> GetUserInfoAsync(
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

            return await ProcessResponseAsync<GoogleUserInfo>(response, cancellationToken);
        }

        private HttpRequestMessage CreateAccessTokenRequest(string code)
        {
            var content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "code", code },
                    { "client_id", this._googleOptions.ClientId },
                    { "client_secret", this._googleOptions.ClientSecret },
                    { "redirect_uri", this._googleOptions.RedirectUri },
                    { "grant_type", "authorization_code" }
                }
            );

            var request = new HttpRequestMessage(HttpMethod.Post, this._googleOptions.TokenUrl)
            {
                Content = content
            };

            request.Content.Headers.ContentType = new MediaTypeHeaderValue(
                "application/x-www-form-urlencoded"
            );

            return request;
        }

        private static async Task<T?> ProcessResponseAsync<T>(
            HttpResponseMessage response,
            CancellationToken cancellationToken
        )
        {
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync(cancellationToken);
                return JsonSerializer.Deserialize<T>(content);
            }

            return default;
        }
    }
}
