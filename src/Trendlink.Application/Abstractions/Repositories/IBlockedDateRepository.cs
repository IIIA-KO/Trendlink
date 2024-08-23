using Trendlink.Domain.Cooperations.BlockedDates;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Abstractions.Repositories
{
    public interface IBlockedDateRepository
    {
        Task<BlockedDate?> GetByIdAsync(
            BlockedDateId id,
            CancellationToken cancellationToken = default
        );

        Task<BlockedDate?> GetByDateAndUserIdAsync(
            DateOnly date,
            UserId userId,
            CancellationToken cancellationToken = default
        );

        Task<bool> ExistsByDateAndUserIdAsync(
            DateOnly date,
            UserId userId,
            CancellationToken cancellationToken = default
        );

        void Add(BlockedDate date);

        void Remove(BlockedDate date);
    }
}
