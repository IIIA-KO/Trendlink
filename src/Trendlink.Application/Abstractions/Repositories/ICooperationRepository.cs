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

        IQueryable<Cooperation> SearchCooperations(
            UserId userId,
            CooperationSearchParameters parameters
        );

        Task<IReadOnlyList<CooperationResponse>> GetUserCooperationsForMonthAsync(
            UserId userId,
            int month,
            int year
        );

        Task<IReadOnlyList<DateOnly>> GetBlockedDatesForUserAsync(
            UserId userId,
            CancellationToken cancellationToken = default
        );

        void Add(Cooperation cooperation);
    }

    public sealed record CooperationSearchParameters(
        string? SearchTerm,
        int? StartMonth,
        int? StartYear,
        int? EndMonth,
        int? EndYear,
        CooperationStatus? CooperationStatus
    );
}
