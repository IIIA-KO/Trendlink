using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;

namespace Trendlink.Application.Abstractions.Repositories
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
    }
}
