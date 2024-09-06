using Microsoft.EntityFrameworkCore;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class CooperationRepository
        : Repository<Cooperation, CooperationId>,
            ICooperationRepository
    {
        private static readonly CooperationStatus[] ActiveCooperationStatuses =
        [
            CooperationStatus.Pending,
            CooperationStatus.Confirmed,
            CooperationStatus.Completed
        ];

        public CooperationRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<bool> IsAlreadyStarted(
            Advertisement advertisement,
            DateTimeOffset scheduledOnUtc,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                .dbContext.Set<Cooperation>()
                .AnyAsync(
                    cooperation =>
                        cooperation.AdvertisementId == advertisement.Id
                        && cooperation.ScheduledOnUtc == scheduledOnUtc
                        && ActiveCooperationStatuses.Contains(cooperation.Status),
                    cancellationToken
                );
        }
    }
}
