﻿using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.ValueObjects;
using Trendlink.Infrastructure.Authentication.Models;
using AccessTokenResponse = Trendlink.Application.Users.LogInUser.AccessTokenResponse;

namespace Trendlink.Infrastructure.Authentication
{
    public record CachedAuthorizationToken(
        string AccessToken,
        string RefreshToken,
        DateTimeOffset AcquiredAt,
        int ExpiresIn
    );

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
    }
}
