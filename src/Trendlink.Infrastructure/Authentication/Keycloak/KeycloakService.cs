using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;
using Trendlink.Infrastructure.Authentication.Models;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using AccessTokenResponse = Trendlink.Application.Users.LogInUser.AccessTokenResponse;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Trendlink.Infrastructure.Authentication.Keycloak
{
    internal sealed class KeycloakService : IKeycloakService
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
        private readonly ILogger<KeycloakService> _logger;

        public KeycloakService(
            HttpClient httpClient,
            IOptions<KeycloakOptions> keycloakOptions,
            ICacheService cacheService,
            ILogger<KeycloakService> logger
        )
        {
            this._httpClient = httpClient;
            this._keycloakOptions = keycloakOptions.Value;
            this._cacheService = cacheService;
            this._logger = logger;
        }

        public async Task<Result<AccessTokenResponse>> GetAccessTokenAsync(
            Email email,
            string password,
            CancellationToken cancellationToken = default
        )
        {
            this._logger.LogInformation(
                "Attempting to get access token for email: {Email}",
                email.Value
            );

            CachedAuthorizationToken? cachedToken = await this.GetCachedAuthorizationTokenAsync(
                email.Value,
                cancellationToken
            );
            if (cachedToken is not null)
            {
                this._logger.LogInformation(
                    "Access token found in cache for email: {Email}",
                    email.Value
                );
                return new AccessTokenResponse(
                    cachedToken.AccessToken,
                    cachedToken.RefreshToken,
                    CalculateRemainingTime(cachedToken)
                );
            }

            Result<AccessTokenResponse> result = await this.RequestTokenAsync(
                this.GetPasswordGrantParameters(email.Value, password),
                email: email.Value,
                cancellationToken
            );

            this._logger.LogInformation(
                "Access token request completed for email: {Email}",
                email.Value
            );

            return result;
        }

        public async Task<Result<AccessTokenResponse>> RefreshTokenAsync(
            string refreshToken,
            CancellationToken cancellationToken = default
        )
        {
            this._logger.LogInformation("Attempting to refresh access token");

            Result<AccessTokenResponse> result = await this.RequestTokenAsync(
                this.GetRefreshTokenGrantParameters(refreshToken),
                email: null,
                cancellationToken
            );

            this._logger.LogInformation("Access token refresh completed");

            return result;
        }

        public async Task<Result<AccessTokenResponse>> AuthenticateWithGoogleAsync(
            GoogleUserInfo userInfo,
            CancellationToken cancellationToken = default
        )
        {
            this._logger.LogInformation(
                "Attempting to authenticate with Google for email: {Email}",
                userInfo.Email
            );

            Result<AccessTokenResponse> result = await this.RequestTokenAsync(
                this.GetPasswordGrantParameters(userInfo.Email, userInfo.Email),
                email: userInfo.Email,
                cancellationToken
            );

            this._logger.LogInformation(
                "Google authentication completed for email: {Email}",
                userInfo.Email
            );

            return result;
        }

        public async Task<Result> LinkExternalIdentityProviderAccountToKeycloakUserAsync(
            string userIdentityId,
            string providerName,
            string providerUserId,
            string providerUsername,
            CancellationToken cancellationToken = default
        )
        {
            this._logger.LogInformation(
                "Linking external provider {ProviderName} to user {UserIdentityId}",
                providerName,
                userIdentityId
            );

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

            if (response.IsSuccessStatusCode)
            {
                this._logger.LogInformation(
                    "Successfully linked {ProviderName} account to user {UserIdentityId}",
                    providerName,
                    userIdentityId
                );
                return Result.Success();
            }
            else
            {
                this._logger.LogWarning(
                    "Failed to link {ProviderName} account to user {UserIdentityId}",
                    providerName,
                    userIdentityId
                );
                return Result.Failure(UserErrors.InvalidCredentials);
            }
        }

        public async Task<bool> IsExternalIdentityProviderAccountLinkedAsync(
            string userIdentityId,
            string providerName,
            CancellationToken cancellationToken = default
        )
        {
            this._logger.LogInformation(
                "Checking if external provider {ProviderName} is linked to user {UserIdentityId}",
                providerName,
                userIdentityId
            );

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
                this._logger.LogWarning(
                    "Failed to get federated identities for user {UserIdentityId}. Status code: {StatusCode}",
                    userIdentityId,
                    response.StatusCode
                );
                return false;
            }

            List<FederatedIdentity>? federatedIdentities = await DeserializeResponseAsync<
                List<FederatedIdentity>
            >(response, cancellationToken);

            bool isLinked =
                federatedIdentities?.Any(f =>
                    f.IdentityProvider.Equals(providerName, StringComparison.OrdinalIgnoreCase)
                ) ?? false;

            if (isLinked)
            {
                this._logger.LogInformation(
                    "Provider {ProviderName} is linked to user {UserId}",
                    providerName,
                    userIdentityId
                );
            }
            else
            {
                this._logger.LogInformation(
                    "Provider {ProviderName} is not linked to user {UserId}",
                    providerName,
                    userIdentityId
                );
            }

            return isLinked;
        }

        public async Task<Result> DeleteAccountAsync(
            string userIdentityId,
            CancellationToken cancellationToken = default
        )
        {
            string accessToken = await this.GetAdminAccessTokenAsync(cancellationToken);

            string url =
                $"{this._keycloakOptions.BaseUrl}/admin/realms/{this._keycloakOptions.Realm}/users/{userIdentityId}";

            using var request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await this._httpClient.SendAsync(
                request,
                cancellationToken
            );

            if (!response.IsSuccessStatusCode)
            {
                this._logger.LogError(
                    "Failed to delete account for user {UserIdentityId}.",
                    userIdentityId
                );
                return Result.Failure(UserErrors.FailedDeleteAccount);
            }

            this._logger.LogInformation(
                "User with ID {UserId} successfully deleted.",
                userIdentityId
            );
            return Result.Success();
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

            bool exists =
                response.IsSuccessStatusCode
                && (await response.Content.ReadAsStringAsync(cancellationToken)).Contains(email);

            return exists;
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
