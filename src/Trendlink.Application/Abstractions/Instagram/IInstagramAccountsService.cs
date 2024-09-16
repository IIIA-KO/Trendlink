using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Abstractions.Instagram
{
    public interface IInstagramAccountsService
    {
        Task<Result<InstagramUserInfo>> GetUserInfoAsync(
            string accessToken,
            string facebookPageId,
            string instagramUsername,
            CancellationToken cancellationToken = default
        );

        Task<Result<InstagramUserInfo>> GetUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        );
    }
}
