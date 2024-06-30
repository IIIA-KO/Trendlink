using System.Net.Http.Json;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Domain.Abstraction;
using Trendlink.Infrastructure.Authentication.Models;

namespace Trendlink.Infrastructure.Authentication
{
    internal sealed class JwtService : IJwtService
    {
        private static readonly Error AuthenticationFailed =
            new(
                "Keycloak.AuthenticationFailed",
                "Failed to acquire access token due to authentication failure"
            );

        private readonly HttpClient _httpClient;
        private readonly KeycloakOptions _keycloakOptions;

        public JwtService(HttpClient httpClient, KeycloakOptions keycloakOptions)
        {
            this._httpClient = httpClient;
            this._keycloakOptions = keycloakOptions;
        }

        public async Task<Result<string>> GetAccessTokenAsync(
            string email,
            string password,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                var authRequestParameters = new KeyValuePair<string, string>[]
                {
                    new("client_id", this._keycloakOptions.AuthClientId),
                    new("client_secret", this._keycloakOptions.AuthClientSecret),
                    new("scope", "openid email"),
                    new("grant_type", "password"),
                    new("username", email),
                    new("password", password),
                };

                using var authorizationRequestContent = new FormUrlEncodedContent(
                    authRequestParameters
                );

                HttpResponseMessage response = await this._httpClient.PostAsync(
                    "",
                    authorizationRequestContent,
                    cancellationToken
                );

                response.EnsureSuccessStatusCode();

                AuthorizationToken? authorizationToken =
                    await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

                if (authorizationToken is null)
                {
                    return Result.Failure<string>(AuthenticationFailed);
                }

                return authorizationToken.AccessToken;
            }
            catch (HttpRequestException)
            {
                return Result.Failure<string>(AuthenticationFailed);
            }
        }
    }
}
