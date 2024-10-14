using System.Data;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Calendar.GetLoggedInUserCalendarForMonth
{
    internal sealed class GetLoggedInUserCalendarForMonthQueryHandler
        : IQueryHandler<GetLoggedInUserCalendarForMonthQuery, IReadOnlyList<LoggedInDateResponse>>
    {
        private readonly IUserContext _userContext;
        private readonly ICooperationRepository _cooperationRepository;

        public GetLoggedInUserCalendarForMonthQueryHandler(
            IUserContext userContext,
            ICooperationRepository cooperationRepository
        )
        {
            this._userContext = userContext;
            this._cooperationRepository = cooperationRepository;
        }

        public async Task<Result<IReadOnlyList<LoggedInDateResponse>>> Handle(
            GetLoggedInUserCalendarForMonthQuery request,
            CancellationToken cancellationToken
        )
        {
            UserId userId = this._userContext.UserId;

            IReadOnlyList<CooperationResponse> cooperations =
                await this._cooperationRepository.GetUserCooperationsForMonthAsync(
                    userId,
                    request.Month,
                    request.Year
                );

            IReadOnlyList<DateOnly> blockedDates =
                await this._cooperationRepository.GetBlockedDatesForUserAsync(
                    userId,
                    cancellationToken
                );

            var dateResponses = cooperations
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
