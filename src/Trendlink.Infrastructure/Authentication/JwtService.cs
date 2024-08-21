using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;
using Trendlink.Infrastructure.Authentication.Keycloak;
using Trendlink.Infrastructure.Authentication.Models;
using AccessTokenResponse = Trendlink.Application.Users.LogInUser.AccessTokenResponse;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Trendlink.Infrastructure.Authentication
{
    internal record CachedAuthorizationToken(
        string AccessToken,
        string RefreshToken,
        DateTimeOffset AcquiredAt,
        int ExpiresIn
    );

    internal record FederatedIdentity(string IdentityProvider, string UserId, string UserName);

    internal sealed class JwtService : IJwtService
    {
        private static readonly Error AuthenticationFailed =
            new(
                "Keycloak.AuthenticationFailed",
                "Failed to acquire access token due to authentication failure"
            );

        private const string CacheKeyPrefix = "jwt:user-";

        private readonly HttpClient _httpClient;

        private readonly KeycloakOptions _keycloakOptions;

        private readonly ICacheService _cacheService;

        public JwtService(
            HttpClient httpClient,
            IOptions<KeycloakOptions> keycloakOptions,
            ICacheService cacheService
        )
        {
            this._httpClient = httpClient;
            this._keycloakOptions = keycloakOptions.Value;
            this._cacheService = cacheService;
        }

        public async Task<Result<AccessTokenResponse>> GetAccessTokenAsync(
            Email email,
            string password,
            CancellationToken cancellationToken = default
        )
        {
            CachedAuthorizationToken? cachedToken = await this.GetCachedAuthorizationTokenAsync(
                email.Value,
                cancellationToken
            );
            if (cachedToken is not null)
            {
                return new AccessTokenResponse(
                    cachedToken.AccessToken,
                    cachedToken.RefreshToken,
                    CalculateRemainingTime(cachedToken)
                );
            }

            return await this.RequestTokenAsync(
                this.GetPasswordGrantParameters(email.Value, password),
                email: email.Value,
                cancellationToken
            );
        }

        public async Task<Result<AccessTokenResponse>> RefreshTokenAsync(
            string refreshToken,
            CancellationToken cancellationToken = default
        )
        {
            return await this.RequestTokenAsync(
                this.GetRefreshTokenGrantParameters(refreshToken),
                email: null,
                cancellationToken
            );
        }

        public async Task<Result<AccessTokenResponse>> AuthenticateWithGoogleAsync(
            GoogleUserInfo userInfo,
            CancellationToken cancellationToken = default
        )
        {
            return await this.RequestTokenAsync(
                this.GetPasswordGrantParameters(userInfo.Email, userInfo.Email),
                email: userInfo.Email,
                cancellationToken
            );
        }

        public async Task<Result> LinkExternalIdentityProviderAccountToKeycloakUserAsync(
            string userIdentityId,
            string providerName,
            string providerUserId,
            string providerUsername,
            CancellationToken cancellationToken = default
        )
        {
            string accessToken = await this.GetAdminAccessTokenAsync(cancellationToken);
            string requestUrl =
                $"{this._keycloakOptions.BaseUrl}/admin/realms/{this._keycloakOptions.Realm}/users/{userIdentityId}/federated-identity/{providerName}";

            HttpResponseMessage response = await this.SendAuthorizedRequestAsync(
                HttpMethod.Post,
                requestUrl,
                accessToken,
                new
                {
                    identityProvider = providerName,
                    userId = providerUserId,
                    userName = providerUsername
                },
                cancellationToken
            );

            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure(UserErrors.InvalidCredentials);
        }

        public async Task<bool> IsExternalIdentityProviderAccountAccountLinkedAsync(
            string userIdentityId,
            string providerName,
            CancellationToken cancellationToken = default
        )
        {
            string accessToken = await this.GetAdminAccessTokenAsync(cancellationToken);
            string requestUrl =
                $"{this._keycloakOptions.BaseUrl}/admin/realms/{this._keycloakOptions.Realm}/users/{userIdentityId}/federated-identity";

            HttpResponseMessage response = await this.SendAuthorizedRequestAsync(
                HttpMethod.Get,
                requestUrl,
                accessToken,
                content: string.Empty,
                cancellationToken
            );
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            List<FederatedIdentity>? federatedIdentities = await DeserializeResponseAsync<
                List<FederatedIdentity>
            >(response, cancellationToken);
            return federatedIdentities?.Any(f => f.IdentityProvider == providerName) ?? false;
        }

        public async Task<bool> CheckUserExistsInKeycloak(
            string email,
            CancellationToken cancellationToken = default
        )
        {
            string requestUrl =
                $"{this._keycloakOptions.BaseUrl}/admin/realms/{this._keycloakOptions.Realm}/users?email={email}";

            string accessToken = await this.GetAdminAccessTokenAsync(cancellationToken);

            HttpResponseMessage response = await this.SendAuthorizedRequestAsync(
                HttpMethod.Get,
                requestUrl,
                accessToken,
                content: string.Empty,
                cancellationToken
            );

            return response.IsSuccessStatusCode
                && (await response.Content.ReadAsStringAsync(cancellationToken)).Contains(email);
        }

        private async Task<string> GetAdminAccessTokenAsync(CancellationToken cancellationToken)
        {
            AuthorizationToken token = await this.GetAccessTokenFromKeycloakAsync(
                [
                    new("client_id", this._keycloakOptions.AdminClientId),
                    new("client_secret", this._keycloakOptions.AdminClientSecret),
                    new("grant_type", "client_credentials")
                ],
                cancellationToken
            );

            return token.AccessToken;
        }

        private async Task<Result<AccessTokenResponse>> RequestTokenAsync(
            KeyValuePair<string, string>[] authRequestParameters,
            string? email,
            CancellationToken cancellationToken
        )
        {
            AuthorizationToken tokenResult = await this.GetAccessTokenFromKeycloakAsync(
                authRequestParameters,
                cancellationToken
            );
            if (string.IsNullOrEmpty(tokenResult.AccessToken))
            {
                return Result.Failure<AccessTokenResponse>(AuthenticationFailed);
            }

            if (!string.IsNullOrEmpty(email))
            {
                var cachedAuthorizationToken = new CachedAuthorizationToken(
                    tokenResult.AccessToken,
                    tokenResult.RefreshToken,
                    DateTimeOffset.UtcNow,
                    tokenResult.ExpiresIn
                );

                await this._cacheService.SetAsync(
                    $"{CacheKeyPrefix}{email}",
                    cachedAuthorizationToken,
                    TimeSpan.FromSeconds(tokenResult.ExpiresIn),
                    cancellationToken
                );
            }

            return new AccessTokenResponse(
                tokenResult.AccessToken,
                tokenResult.RefreshToken,
                tokenResult.ExpiresIn
            );
        }

        private async Task<CachedAuthorizationToken?> GetCachedAuthorizationTokenAsync(
            string email,
            CancellationToken cancellationToken
        )
        {
            return await this._cacheService.GetAsync<CachedAuthorizationToken>(
                $"{CacheKeyPrefix}{email}",
                cancellationToken
            );
        }

        private static int CalculateRemainingTime(CachedAuthorizationToken cachedToken)
        {
            return (int)
                (
                    cachedToken.AcquiredAt.AddSeconds(cachedToken.ExpiresIn) - DateTimeOffset.UtcNow
                ).TotalSeconds;
        }

        private KeyValuePair<string, string>[] GetPasswordGrantParameters(
            string email,
            string password
        )
        {
            return
            [
                new("client_id", this._keycloakOptions.AuthClientId),
                new("client_secret", this._keycloakOptions.AuthClientSecret),
                new("scope", "openid email"),
                new("grant_type", "password"),
                new("username", email),
                new("password", password),
            ];
        }

        private KeyValuePair<string, string>[] GetRefreshTokenGrantParameters(string refreshToken)
        {
            return
            [
                new("client_id", this._keycloakOptions.AuthClientId),
                new("client_secret", this._keycloakOptions.AuthClientSecret),
                new("scope", "openid email"),
                new("grant_type", "refresh_token"),
                new("refresh_token", refreshToken),
            ];
        }

        private async Task<HttpResponseMessage> SendAuthorizedRequestAsync(
            HttpMethod method,
            string url,
            string accessToken,
            object? content = null,
            CancellationToken cancellationToken = default
        )
        {
            using var request = new HttpRequestMessage(method, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            if (content is not null)
            {
                request.Content = new StringContent(
                    JsonSerializer.Serialize(content),
                    Encoding.UTF8,
                    "application/json"
                );
            }

            return await this._httpClient.SendAsync(request, cancellationToken);
        }

        private static async Task<T?> DeserializeResponseAsync<T>(
            HttpResponseMessage response,
            CancellationToken cancellationToken
        )
        {
            return JsonSerializer.Deserialize<T>(
                await response.Content.ReadAsStringAsync(cancellationToken)
            );
        }

        private async Task<AuthorizationToken> GetAccessTokenFromKeycloakAsync(
            KeyValuePair<string, string>[] parameters,
            CancellationToken cancellationToken
        )
        {
            using var content = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response = await this._httpClient.PostAsync(
                string.Empty,
                content,
                cancellationToken
            );
            response.EnsureSuccessStatusCode();

            AuthorizationToken? tokenResponse = JsonSerializer.Deserialize<AuthorizationToken>(
                await response.Content.ReadAsStringAsync(cancellationToken)
            );

            return tokenResponse ?? throw new InvalidOperationException("Token response is null.");
        }
    }
}
