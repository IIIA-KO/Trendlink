using Microsoft.EntityFrameworkCore;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class InstagramAccountRepository
        : Repository<InstagramAccount, InstagramAccountId>,
            IInstagramAccountRepository
    {
        public InstagramAccountRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<InstagramAccount?> GetByUserIdAsync(
            UserId userId,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                .dbContext.Set<InstagramAccount>()
                .FirstOrDefaultAsync(
                    instagramAccount => instagramAccount.UserId == userId,
                    cancellationToken
                );
        }
    }
}
