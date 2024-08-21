using Microsoft.EntityFrameworkCore;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Cooperations.BlockedDates;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class BlockedDateRepository
        : Repository<BlockedDate, BlockedDateId>,
            IBlockedDateRepository
    {
        public BlockedDateRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<BlockedDate?> GetByDateAndUserIdAsync(
            DateOnly date,
            UserId userId,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                .dbContext.Set<BlockedDate>()
                .FirstOrDefaultAsync(
                    blockedDate => blockedDate.Date == date && blockedDate.UserId == userId,
                    cancellationToken
                );
        }

        public async Task<bool> ExistsByDateAndUserIdAsync(
            DateOnly date,
            UserId userId,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ExistsAsync(
                blockedDate => blockedDate.Date == date && blockedDate.UserId == userId,
                cancellationToken
            );
        }
    }
}
