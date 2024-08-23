using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Abstractions.Repositories
{
    public interface IInstagramAccountRepository
    {
        Task<InstagramAccount?> GetByIdAsync(
            InstagramAccountId id,
            CancellationToken cancellationToken = default
        );

        void Add(InstagramAccount instagramAccount);
    }
}
