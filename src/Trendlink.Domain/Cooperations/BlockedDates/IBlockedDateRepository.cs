using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Domain.Cooperations.BlockedDates
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

        IQueryable<BlockedDate> DbSetAsQueryable();
    }
}
