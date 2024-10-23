using Trendlink.Application.Abstractions.Authentication.Models;

namespace Trendlink.Application.Abstractions.Authentication
{
    public interface IGoogleService
    {
        Task<GoogleTokenResponse?> GetAccessTokenAsync(
            string code,
            CancellationToken cancellationToken
        );

        Task<GoogleUserInfo?> GetUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken
        );
    }
}
