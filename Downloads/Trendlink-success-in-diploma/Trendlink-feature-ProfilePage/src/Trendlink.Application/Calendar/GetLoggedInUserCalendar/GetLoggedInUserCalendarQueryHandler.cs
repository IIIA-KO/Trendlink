using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Calendar.GetLoggedInUserCalendar
{
    internal sealed class GetLoggedInUserCalendarQueryHandler
        : IQueryHandler<GetLoggedInUserCalendarQuery, IReadOnlyList<LoggedInDateResponse>>
    {
        private readonly IUserContext _userContext;
        private readonly ICooperationRepository _cooperationRepository;

        public GetLoggedInUserCalendarQueryHandler(
            IUserContext userContext,
            ICooperationRepository cooperationRepository
        )
        {
            this._userContext = userContext;
            this._cooperationRepository = cooperationRepository;
        }

        public async Task<Result<IReadOnlyList<LoggedInDateResponse>>> Handle(
            GetLoggedInUserCalendarQuery request,
            CancellationToken cancellationToken
        )
        {
            UserId userId = this._userContext.UserId;

            IQueryable<Cooperation> cooperations = this._cooperationRepository.SearchCooperations(
                userId,
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
                    userId,
                    cancellationToken
                );

            var dateResponses = cooperationResponses
                .GroupBy(c => DateOnly.FromDateTime(c.ScheduledOnUtc.UtcDateTime))
                .Select(g => new LoggedInDateResponse
                {
                    Date = g.Key,
                    IsBlocked = blockedDates.Contains(g.Key),
                    Cooperations = g.ToList()
                })
                .ToList();

            IEnumerable<LoggedInDateResponse> additionalBlockedDates = blockedDates
                .Where(d => !dateResponses.Any(dr => dr.Date == d))
                .Select(d => new LoggedInDateResponse
                {
                    Date = d,
                    IsBlocked = true,
                    Cooperations = []
                });

            dateResponses.AddRange(additionalBlockedDates);

            return dateResponses.OrderBy(g => g.Date).ToList();
        }
    }
}
