using Trendlink.Domain.Abstraction;
using Trendlink.Infrastructure.Instagram.Models.Accounts;

namespace Trendlink.Infrastructure.Instagram.Abstraction
{
    internal interface IFacebookService
    {
        Task<Result<string>> GetFacebookPageIdAsync(
            string accessToken,
            CancellationToken cancellationToken
        );

        Task<Result<string>> GetAdAccountIdAsync(
            string accessToken,
            CancellationToken cancellationToken
        );

        Task<FacebookUserInfo?> GetFacebookUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken
        );
    }
}
