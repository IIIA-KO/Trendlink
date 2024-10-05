using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using AccessTokenResponse = Trendlink.Application.Accounts.LogInUser.AccessTokenResponse;

namespace Trendlink.Application.Abstractions.Authentication
{
    public interface IKeycloakService
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

        Task<Result> LinkExternalIdentityProviderAccountToKeycloakUserAsync(
            string userIdentityId,
            string providerName,
            string providerUserId,
            string providerUsername,
            CancellationToken cancellationToken = default
        );

        Task<bool> IsExternalIdentityProviderAccountLinkedAsync(
            string userIdentityId,
            string providerName,
            CancellationToken cancellationToken = default
        );

        Task<Result> DeleteAccountAsync(
            string userIdentityId,
            CancellationToken cancellationToken = default
        );
    }
}
