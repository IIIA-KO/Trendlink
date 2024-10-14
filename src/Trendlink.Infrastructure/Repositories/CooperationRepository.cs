using Microsoft.EntityFrameworkCore;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Calendar;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Cooperations.BlockedDates;
using Trendlink.Domain.Users;

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

        public async Task<IReadOnlyList<CooperationResponse>> GetCooperationsForUserAsync(
            UserId userId,
            int? month = null,
            int? year = null,
            CancellationToken cancellationToken = default
        )
        {
            IQueryable<Cooperation> cooperationsQuery = this
                .dbContext.Set<Cooperation>()
                .Where(c => c.BuyerId == userId || c.SellerId == userId);

            if (month.HasValue && year.HasValue)
            {
                cooperationsQuery = cooperationsQuery.Where(c =>
                    c.ScheduledOnUtc.Month == month.Value && c.ScheduledOnUtc.Year == year.Value
                );
            }

            return await cooperationsQuery
                .Select(c => new CooperationResponse
                {
                    Id = c.Id.Value,
                    Name = c.Name.Value,
                    Description = c.Description.Value,
                    ScheduledOnUtc = c.ScheduledOnUtc,
                    PriceAmount = c.Price.Amount,
                    PriceCurrency = c.Price.Currency.Code,
                    AdvertisementId = c.AdvertisementId.Value,
                    BuyerId = c.BuyerId.Value,
                    SellerId = c.SellerId.Value,
                    Status = c.Status
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<DateOnly>> GetBlockedDatesForUserAsync(
            UserId userId,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                .dbContext.Set<BlockedDate>()
                .Where(bd => bd.UserId == userId)
                .Select(bd => bd.Date)
                .ToListAsync(cancellationToken);
        }
    }
}
