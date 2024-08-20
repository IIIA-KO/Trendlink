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
    internal sealed class JwtService : IJwtService
    {
        internal record CachedAuthorizationToken(
            string AccessToken,
            string RefreshToken,
            DateTimeOffset AcquiredAt,
            int ExpiresIn
        );

        internal record FederatedIdentity(string IdentityProvider, string UserId, string UserName);

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
            string cacheKey = $"{CacheKeyPrefix}{email.Value}";

            CachedAuthorizationToken? cachedToken =
                await this._cacheService.GetAsync<CachedAuthorizationToken>(
                    cacheKey,
                    cancellationToken
                );

            if (cachedToken is not null)
            {
                TimeSpan remainingTime =
                    cachedToken.AcquiredAt.AddSeconds(cachedToken.ExpiresIn)
                    - DateTimeOffset.UtcNow;

                return new AccessTokenResponse(
                    cachedToken.AccessToken,
                    cachedToken.RefreshToken,
                    (int)remainingTime.TotalSeconds
                );
            }

            try
            {
                var authRequestParameters = new KeyValuePair<string, string>[]
                {
                    new("client_id", this._keycloakOptions.AuthClientId),
                    new("client_secret", this._keycloakOptions.AuthClientSecret),
                    new("scope", "openid email"),
                    new("grant_type", "password"),
                    new("username", email.Value),
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
                    return Result.Failure<AccessTokenResponse>(AuthenticationFailed);
                }

                var cachedAuthorizationToken = new CachedAuthorizationToken(
                    authorizationToken.AccessToken,
                    authorizationToken.RefreshToken,
                    DateTimeOffset.UtcNow,
                    authorizationToken.ExpiresIn
                );

                await this._cacheService.SetAsync(
                    cacheKey,
                    cachedAuthorizationToken,
                    TimeSpan.FromSeconds(authorizationToken.ExpiresIn),
                    cancellationToken
                );

                return new AccessTokenResponse(
                    authorizationToken.AccessToken,
                    authorizationToken.RefreshToken,
                    authorizationToken.ExpiresIn
                );
            }
            catch (HttpRequestException)
            {
                return Result.Failure<AccessTokenResponse>(AuthenticationFailed);
            }
        }

        public async Task<Result<AccessTokenResponse>> RefreshTokenAsync(
            string refreshToken,
            CancellationToken cancellationToken = default
        )
        {
            return await this.RequestTokenAsync(
                [
                    new("client_id", this._keycloakOptions.AuthClientId),
                    new("client_secret", this._keycloakOptions.AuthClientSecret),
                    new("scope", "openid email"),
                    new("grant_type", "refresh_token"),
                    new("refresh_token", refreshToken),
                ],
                email: null,
                cancellationToken
            );
        }

        private async Task<Result<AccessTokenResponse>> RequestTokenAsync(
            KeyValuePair<string, string>[] authRequestParameters,
            string? email,
            CancellationToken cancellationToken
        )
        {
            try
            {
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
                    return Result.Failure<AccessTokenResponse>(AuthenticationFailed);
                }

                if (!string.IsNullOrEmpty(email))
                {
                    var cachedAuthorizationToken = new CachedAuthorizationToken(
                        authorizationToken.AccessToken,
                        authorizationToken.RefreshToken,
                        DateTimeOffset.UtcNow,
                        authorizationToken.ExpiresIn
                    );

                    await this._cacheService.SetAsync(
                        $"{CacheKeyPrefix}{email}",
                        cachedAuthorizationToken,
                        TimeSpan.FromSeconds(authorizationToken.ExpiresIn),
                        cancellationToken
                    );
                }

                return new AccessTokenResponse(
                    authorizationToken.AccessToken,
                    authorizationToken.RefreshToken,
                    authorizationToken.ExpiresIn
                );
            }
            catch (HttpRequestException)
            {
                return Result.Failure<AccessTokenResponse>(AuthenticationFailed);
            }
        }

        public async Task<Result<AccessTokenResponse>> AuthenticateWithGoogleAsync(
            GoogleUserInfo userInfo,
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
                    new("username", userInfo.Email),
                    new("password", userInfo.Email),
                };

                using var authorizationRequestContent = new FormUrlEncodedContent(
                    authRequestParameters
                );

                HttpResponseMessage response = await this._httpClient.PostAsync(
                    this._keycloakOptions.TokenUrl,
                    authorizationRequestContent,
                    cancellationToken
                );

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync(cancellationToken);
                    AuthorizationToken? authorizationToken =
                        JsonSerializer.Deserialize<AuthorizationToken>(content);

                    if (authorizationToken is null)
                    {
                        return Result.Failure<AccessTokenResponse>(AuthenticationFailed);
                    }

                    var cachedAuthorizationToken = new CachedAuthorizationToken(
                        authorizationToken.AccessToken,
                        authorizationToken.RefreshToken,
                        DateTimeOffset.UtcNow,
                        authorizationToken.ExpiresIn
                    );

                    string cacheKey = $"{CacheKeyPrefix}{userInfo.Email}";
                    await this._cacheService.SetAsync(
                        cacheKey,
                        cachedAuthorizationToken,
                        TimeSpan.FromSeconds(authorizationToken.ExpiresIn),
                        cancellationToken
                    );

                    return new AccessTokenResponse(
                        authorizationToken.AccessToken,
                        authorizationToken.RefreshToken,
                        authorizationToken.ExpiresIn
                    );
                }
                else
                {
                    return Result.Failure<AccessTokenResponse>(AuthenticationFailed);
                }
            }
            catch (HttpRequestException)
            {
                return Result.Failure<AccessTokenResponse>(AuthenticationFailed);
            }
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

            var identityProviderLink = new
            {
                identityProvider = providerName,
                userId = providerUserId,
                userName = providerUsername
            };

            var requestContent = new StringContent(
                JsonSerializer.Serialize(identityProviderLink),
                Encoding.UTF8,
                "application/json"
            );

            using var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{this._keycloakOptions.BaseUrl}/admin/realms/{this._keycloakOptions.Realm}/users/{userIdentityId}/federated-identity/{providerName}"
            )
            {
                Content = requestContent
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await this._httpClient.SendAsync(
                request,
                cancellationToken
            );

            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure(UserErrors.InvalidCredentials);
        }

        public async Task<bool> IsInstagramAccountLinkedAsync(
            string userTdentityId,
            CancellationToken cancellationToken = default
        )
        {
            string accessToken = await this.GetAdminAccessTokenAsync(cancellationToken);

            string requestUrl =
                $"{this._keycloakOptions.BaseUrl}/admin/realms/{this._keycloakOptions.Realm}/users/{userTdentityId}/federated-identity";

            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await this._httpClient.SendAsync(
                request,
                cancellationToken
            );

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            List<FederatedIdentity>? federatedIdentities = JsonSerializer.Deserialize<
                List<FederatedIdentity>
            >(await response.Content.ReadAsStringAsync(cancellationToken));

            return federatedIdentities?.Any(f => f.IdentityProvider == "instagram") ?? false;
        }

        public async Task<bool> CheckUserExistsInKeycloak(
            string email,
            CancellationToken cancellationToken = default
        )
        {
            string requestUrl =
                $"{this._keycloakOptions.BaseUrl}/admin/realms/{this._keycloakOptions.Realm}/users?email={email}";

            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                await this.GetAdminAccessTokenAsync(cancellationToken)
            );

            HttpResponseMessage response = await this._httpClient.SendAsync(
                request,
                cancellationToken
            );

            return response.IsSuccessStatusCode
                && (await response.Content.ReadAsStringAsync(cancellationToken)).Contains(email);
        }

        private async Task<string> GetAdminAccessTokenAsync(CancellationToken cancellationToken)
        {
            var adminRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", this._keycloakOptions.AdminClientId),
                new("client_secret", this._keycloakOptions.AdminClientSecret),
                new("grant_type", "client_credentials")
            };

            using var adminRequestContent = new FormUrlEncodedContent(adminRequestParameters);

            HttpResponseMessage response = await this._httpClient.PostAsync(
                $"{this._keycloakOptions.BaseUrl}/realms/{this._keycloakOptions.Realm}/protocol/openid-connect/token",
                adminRequestContent,
                cancellationToken
            );

            response.EnsureSuccessStatusCode();

            AuthorizationToken? tokenResponse = JsonSerializer.Deserialize<AuthorizationToken>(
                await response.Content.ReadAsStringAsync(cancellationToken)
            );

            return tokenResponse?.AccessToken;
        }
    }
}
