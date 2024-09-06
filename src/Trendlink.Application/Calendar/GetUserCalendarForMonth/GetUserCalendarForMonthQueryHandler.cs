using System.Data;
using Dapper;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Calendar.GetUserCalendarForMonth
{
    internal sealed class GetUserCalendarForMonthQueryHandler
        : IQueryHandler<GetUserCalendarForMonthQuery, IReadOnlyList<DateResponse>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserCalendarForMonthQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<DateResponse>>> Handle(
            GetUserCalendarForMonthQuery request,
            CancellationToken cancellationToken
        )
        {
            using IDbConnection dbConnection = this._sqlConnectionFactory.CreateConnection();

            const string sqlCooperations = """
                SELECT
                    scheduled_on_utc AS ScheduledOnUtc
                FROM cooperations
                WHERE (seller_id = @UserId)
                    AND EXTRACT(MONTH FROM scheduled_on_utc) = @Month
                    AND EXTRACT(YEAR FROM scheduled_on_utc) = @Year
                """;

            const string sqlBlockedDates = """
                SELECT 
                    date AS Date
                FROM blocked_dates
                WHERE user_id = @UserId
                    AND EXTRACT(MONTH FROM date) = @Month
                    AND EXTRACT(YEAR FROM date) = @Year
                """;

            try
            {
                var parameters = new
                {
                    UserId = request.UserId.Value,
                    request.Month,
                    request.Year
                };

                IEnumerable<CooperationResponse> cooperations =
                    await dbConnection.QueryAsync<CooperationResponse>(sqlCooperations, parameters);

                IEnumerable<DateOnly> blockedDates = await dbConnection.QueryAsync<DateOnly>(
                    sqlBlockedDates,
                    parameters
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

                return dateResponses.OrderBy(d => d.Date).ToList();
            }
            catch (Exception)
            {
                return Result.Failure<IReadOnlyList<DateResponse>>(Error.Unexpected);
            }
        }
    }
}
