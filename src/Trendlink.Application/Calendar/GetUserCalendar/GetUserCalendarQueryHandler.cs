using System.Data;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;

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
            IReadOnlyList<CooperationResponse> cooperations =
                await this._cooperationRepository.GetCooperationsForUserAsync(
                    request.UserId,
                    cancellationToken: cancellationToken
                );

            IReadOnlyList<DateOnly> blockedDates =
                await this._cooperationRepository.GetBlockedDatesForUserAsync(
                    request.UserId,
                    cancellationToken
                );

            var dateResponses = cooperations
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
