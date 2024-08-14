using Trendlink.Application.Abstractions.Authentication.Models;

namespace Trendlink.Application.Abstractions.Authentication
{
    public interface IInstagramService
    {
        Task<InstagramTokenResponse?> GetAccessTokenAsync(
            string code,
            CancellationToken cancellationToken = default
        );

        Task<InstagramUserInfo?> GetUserInfoAsync(
            string accessToken,
            long instagramUserId,
            CancellationToken cancellationToken = default
        );
    }
}
