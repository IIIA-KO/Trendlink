﻿using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.ValueObjects;
using AccessTokenResponse = Trendlink.Application.Users.LogInUser.AccessTokenResponse;

namespace Trendlink.Application.Abstractions.Authentication
{
    public interface IJwtService
    {
        Task<Result<AccessTokenResponse>> GetAccessTokenAsync(
            Email email,
            string password,
            CancellationToken cancellationToken = default
        );

        Task<Result<AccessTokenResponse>> RefreshTokenAsync(
            string refreshToken,
            CancellationToken cancellationToken = default
        );

        Task<Result<AccessTokenResponse>> AuthenticateWithGoogleAsync(
            GoogleUserInfo userInfo,
            CancellationToken cancellationToken = default
        );

        Task<bool> CheckUserExistsInKeycloak(
            string email,
            CancellationToken cancellationToken = default
        );

        Task<bool> LinkInstagramAccountToKeycloakUserAsync(
            string userId,
            string providerUserId,
            string providerUsername,
            CancellationToken cancellationToken = default
        );
    }
}
