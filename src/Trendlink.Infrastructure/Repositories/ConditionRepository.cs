using Microsoft.EntityFrameworkCore;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.ValueObjects;
using Trendlink.Domain.Users.ValueObjects;
using Trendlink.Infrastructure.Specifications.Conditions;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class ConditionRepository
        : Repository<Condition, ConditionId>,
            IConditionRepository
    {
        public ConditionRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<Condition?> GetByIdWithAdvertisementsAsync(
            ConditionId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(
                    new ConditionByIdWithAdvertisementsSpecification(id)
                )
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Condition?> GetByIdWithUserAsync(
            ConditionId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(new ConditionByIdWithUserSpecification(id))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Condition?> GetByUserIdAsync(
            UserId userId,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                .dbContext.Set<Condition>()
                .FirstOrDefaultAsync(condition => condition.UserId == userId, cancellationToken);
        }

        public async Task<Condition?> GetByUserIdWithAdvertisementAsync(
            UserId userId,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(
                    new ConditionByUserIdWithAdvertisementsSpecification(userId)
                )
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> ExistsByUserId(
            UserId userId,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ExistsAsync(
                condition => condition.UserId == userId,
                cancellationToken
            );
        }
    }
}
