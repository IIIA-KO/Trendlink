using Trendlink.Domain.Conditions;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Abstractions.Repositories
{
    public interface IConditionRepository
    {
        Task<Condition?> GetByIdAsync(
            ConditionId id,
            CancellationToken cancellationToken = default
        );

        Task<Condition?> GetByIdWithAdvertisementsAsync(
            ConditionId id,
            CancellationToken cancellationToken = default
        );

        Task<Condition?> GetByIdWithUserAsync(
            ConditionId id,
            CancellationToken cancellationToken = default
        );

        Task<Condition?> GetByUserIdAsync(
            UserId userId,
            CancellationToken cancellationToken = default
        );

        Task<Condition?> GetByUserIdWithAdvertisementAsync(
            UserId userId,
            CancellationToken cancellationToken = default
        );

        Task<bool> ExistsByUserId(UserId userId, CancellationToken cancellationToken = default);

        void Add(Condition condition);
    }
}
