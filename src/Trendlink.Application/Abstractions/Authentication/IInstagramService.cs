using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Abstractions.Authentication
{
    public interface IInstagramService
    {
        Task<FacebookTokenResponse?> GetAccessTokenAsync(
            string code,
            CancellationToken cancellationToken = default
        );

        Task<Result<InstagramUserInfo>> GetUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        );
    }
}
