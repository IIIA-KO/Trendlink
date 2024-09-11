using Trendlink.Domain.Abstraction;
using Trendlink.Infrastructure.Instagram.Models.Accounts;

namespace Trendlink.Infrastructure.Instagram
{
    internal interface IInstagramAccountsService
    {
        Task<Result<string>> GetFacebookPageIdAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        );
        Task<FacebookUserInfo?> GetFacebookUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        );
        Task<InstagramBusinessAccountResponse> GetInstagramBusinessAccountAsync(
            string accessToken,
            string facebookPageId,
            CancellationToken cancellationToken = default
        );
        Task<HttpResponseMessage> SendGetRequestAsync(
            string url,
            CancellationToken cancellationToken
        );
    }
}
