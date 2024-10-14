using System.Linq.Expressions;
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

        public IQueryable<Cooperation> SearchCooperations(
            UserId userId,
            CooperationSearchParameters parameters
        )
        {
            IQueryable<Cooperation> query = this
                .dbContext.Set<Cooperation>()
                .Where(cooperation =>
                    cooperation.BuyerId == userId || cooperation.SellerId == userId
                );

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                query = query.Where(cooperation =>
                    ((string)cooperation.Name).Contains(parameters.SearchTerm)
                    || ((string)cooperation.Description).Contains(parameters.SearchTerm)
                );
            }

            if (parameters.StartMonth.HasValue && parameters.StartYear.HasValue)
            {
                var startDate = new DateTime(
                    parameters.StartYear.Value,
                    parameters.StartMonth.Value,
                    day: 1,
                    hour: 0,
                    minute: 0,
                    second: 0,
                    DateTimeKind.Utc
                );

                query = query.Where(cooperation => cooperation.ScheduledOnUtc >= startDate);
            }

            if (parameters.EndMonth.HasValue && parameters.EndYear.HasValue)
            {
                var endDate = new DateTime(
                    parameters.EndYear.Value,
                    parameters.EndMonth.Value,
                    day: DateTime.DaysInMonth(parameters.EndYear.Value, parameters.EndMonth.Value),
                    hour: 23,
                    minute: 59,
                    second: 59,
                    DateTimeKind.Utc
                );

                query = query.Where(cooperation => cooperation.ScheduledOnUtc <= endDate);
            }

            if (parameters.CooperationStatus is not null)
            {
                query = query.Where(cooperation =>
                    cooperation.Status == parameters.CooperationStatus
                );
            }

            return query;
        }

        public async Task<IReadOnlyList<CooperationResponse>> GetUserCooperationsForMonthAsync(
            UserId userId,
            int month,
            int year
        )
        {
            IQueryable<Cooperation> cooperationsQuery = this
                .dbContext.Set<Cooperation>()
                .Where(cooperation => cooperation.SellerId == userId)
                .Where(cooperation =>
                    cooperation.ScheduledOnUtc.Month == month
                    && cooperation.ScheduledOnUtc.Year == year
                );

            return await cooperationsQuery
                .Select(cooperation => new CooperationResponse
                {
                    Id = cooperation.Id.Value,
                    Name = cooperation.Name.Value,
                    Description = cooperation.Description.Value,
                    ScheduledOnUtc = cooperation.ScheduledOnUtc,
                    PriceAmount = cooperation.Price.Amount,
                    PriceCurrency = cooperation.Price.Currency.Code,
                    AdvertisementId = cooperation.AdvertisementId.Value,
                    BuyerId = cooperation.BuyerId.Value,
                    SellerId = cooperation.SellerId.Value,
                    Status = cooperation.Status
                })
                .ToListAsync();
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
