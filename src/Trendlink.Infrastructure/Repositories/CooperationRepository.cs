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
                        && Cooperation.ActiveCooperationStatuses.Contains(cooperation.Status),
                    cancellationToken
                );
        }

        public async Task<bool> HasActiveCooperationsForAdvertisement(
            Advertisement advertisement,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                .dbContext.Set<Cooperation>()
                .AnyAsync(
                    cooperation =>
                        cooperation.AdvertisementId == advertisement.Id
                        && Cooperation.ActiveCooperationStatuses.Contains(cooperation.Status),
                    cancellationToken
                );
        }
    }
}
