using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Notifications;

namespace Trendlink.Domain.Cooperations
{
    public interface ICooperationRepository
    {
        Task<Cooperation?> GetByIdAsync(
            CooperationId id,
            CancellationToken cancellationToken = default
        );

        Task<bool> IsAlreadyStarted(
            Advertisement advertisement,
            DateTimeOffset scheduledOnUtc,
            CancellationToken cancellationToken = default
        );

        void Add(Cooperation cooperation);

        IQueryable<Cooperation> DbSetAsQueryable();
    }
}
