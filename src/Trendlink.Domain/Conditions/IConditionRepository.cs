using Trendlink.Domain.Conditions.ValueObjects;

namespace Trendlink.Domain.Conditions
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

        void Add(Condition condition);
    }
}
