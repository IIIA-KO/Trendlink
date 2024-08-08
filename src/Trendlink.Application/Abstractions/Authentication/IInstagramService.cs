using Trendlink.Application.Users.LogInUser;

namespace Trendlink.Application.Abstractions.Authentication
{
    public interface IInstagramService
    {
        Task<AccessTokenResponse> GetAccessTokenAsync(
            string code,
            CancellationToken cancellationToken = default
        );

        Task<InstagramUserInfo?> GetUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        );
    }
}
