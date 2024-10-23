using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Abstractions.Repositories
{
    public interface IInstagramAccountRepository
    {
        Task<InstagramAccount?> GetByIdAsync(
            InstagramAccountId id,
            CancellationToken cancellationToken = default
        );

        Task<InstagramAccount?> GetByUserIdAsync(
            UserId userId,
            CancellationToken cancellationToken = default
        );

        void Add(InstagramAccount instagramAccount);

        void Remove(InstagramAccount instagramAccount);
    }
}
