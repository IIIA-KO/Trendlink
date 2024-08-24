using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class InstagramAccountRepository
        : Repository<InstagramAccount, InstagramAccountId>,
            IInstagramAccountRepository
    {
        public InstagramAccountRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
