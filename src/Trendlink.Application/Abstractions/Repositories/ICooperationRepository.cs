using Trendlink.Application.Calendar;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Users;

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

        Task<bool> HasActiveCooperationsForAdvertisement(
            Advertisement advertisement,
            CancellationToken cancellationToken = default
        );

        Task<IReadOnlyList<CooperationResponse>> GetCooperationsForUserAsync(
            UserId userId,
            int? month = null,
            int? year = null,
            CancellationToken cancellationToken = default
        );

        Task<IReadOnlyList<DateOnly>> GetBlockedDatesForUserAsync(
            UserId userId,
            CancellationToken cancellationToken = default
        );

        void Add(Cooperation cooperation);
    }
}
