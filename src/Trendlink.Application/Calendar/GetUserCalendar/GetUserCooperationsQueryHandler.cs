using System.Data;
using Dapper;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Calendar.GetUserCalendar
{
    internal sealed class GetUserCooperationsQueryHandler
        : IQueryHandler<GetUserCalendarQuery, IReadOnlyList<DateResponse>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserCooperationsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<DateResponse>>> Handle(
            GetUserCalendarQuery request,
            CancellationToken cancellationToken
        )
        {
            using IDbConnection dbConnection = this._sqlConnectionFactory.CreateConnection();

            const string sqlCooperations = """
                SELECT
                    scheduled_on_utc AS ScheduledOnUtc
                FROM cooperations
                WHERE buyer_id = @UserId
                    OR seller_id = @UserId
                """;

            const string sqlBlockedDates = """
                SELECT
                    date AS Date
                FROM blocked_dates
                WHERE user_id = @UserId
                """;

            try
            {
                var userId = new { UserId = request.UserId.Value };

                IEnumerable<CooperationResponse> cooperations =
                    await dbConnection.QueryAsync<CooperationResponse>(sqlCooperations, userId);

                IEnumerable<DateOnly> blockedDates = await dbConnection.QueryAsync<DateOnly>(
                    sqlBlockedDates,
                    userId
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
            catch (Exception)
            {
                return Result.Failure<IReadOnlyList<DateResponse>>(Error.Unexpected);
            }
        }
    }
}
