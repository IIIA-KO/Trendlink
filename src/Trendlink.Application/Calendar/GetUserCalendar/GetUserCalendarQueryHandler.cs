using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Cooperations;

namespace Trendlink.Application.Calendar.GetUserCalendar
{
    internal sealed class GetUserCalendarQueryHandler
        : IQueryHandler<GetUserCalendarQuery, IReadOnlyList<DateResponse>>
    {
        private readonly ICooperationRepository _cooperationRepository;

        public GetUserCalendarQueryHandler(ICooperationRepository cooperationRepository)
        {
            this._cooperationRepository = cooperationRepository;
        }

        public async Task<Result<IReadOnlyList<DateResponse>>> Handle(
            GetUserCalendarQuery request,
            CancellationToken cancellationToken
        )
        {
            IQueryable<Cooperation> cooperations = this._cooperationRepository.SearchCooperations(
                request.UserId,
                new CooperationSearchParameters(
                    request.SearchTerm,
                    request.StartMonth,
                    request.StartYear,
                    request.EndMonth,
                    request.EndYear,
                    request.CooperationStatus
                )
            );

            IQueryable<CooperationResponse> cooperationResponses = cooperations.Select(
                cooperation => new CooperationResponse
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
                }
            );

            IReadOnlyList<DateOnly> blockedDates =
                await this._cooperationRepository.GetBlockedDatesForUserAsync(
                    request.UserId,
                    cancellationToken
                );

            var dateResponses = cooperationResponses
                .GroupBy(c => DateOnly.FromDateTime(c.ScheduledOnUtc.UtcDateTime))
                .Select(g => new DateResponse
                {
                    Date = g.Key,
                    IsBlocked = blockedDates.Contains(g.Key),
                    CooperationsCount = g.Count()
                })
                .ToList();

            IEnumerable<DateResponse> additionalBlockedDates = blockedDates
                .Where(d => !dateResponses.Any(dr => dr.Date == d))
                .Select(d => new DateResponse
                {
                    Date = d,
                    IsBlocked = true,
                    CooperationsCount = 0
                });

            dateResponses.AddRange(additionalBlockedDates);

            return dateResponses.OrderBy(g => g.Date).ToList();
        }
    }
}
