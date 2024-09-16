using Trendlink.Application.Abstractions.Authentication.Models;

namespace Trendlink.Application.Abstractions.Instagram
{
    public interface IInstagramService
        : IInstagramAccountsService,
            IInstagramPostsService,
            IInstagramAudienceService
    {
        Task<FacebookTokenResponse?> GetAccessTokenAsync(
            string code,
            CancellationToken cancellationToken = default
        );

        Task<FacebookTokenResponse?> RenewAccessTokenAsync(
            string code,
            CancellationToken cancellationToken = default
        );
    }
}
