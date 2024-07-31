using System.Data;
using Dapper;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Calendar.GetLoggedInUserCooperations;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Calendar.GetLoggedInUserCooperationsForMonth
{
    internal sealed class GetLoggedInUserCooperationsForMonthQueryHandler
        : IQueryHandler<GetLoggedInUserCooperationsForMonthQuery, IReadOnlyList<DateResponse>>
    {
        private readonly IUserContext _userContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetLoggedInUserCooperationsForMonthQueryHandler(
            IUserContext userContext,
            ISqlConnectionFactory sqlConnectionFactory
        )
        {
            this._userContext = userContext;
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<DateResponse>>> Handle(
            GetLoggedInUserCooperationsForMonthQuery request,
            CancellationToken cancellationToken
        )
        {
            using IDbConnection dbConnection = this._sqlConnectionFactory.CreateConnection();

            const string sqlCooperations = """
                SELECT
                    id AS Id,
                    name AS Name,
                    description AS Description,
                    scheduled_on_utc AS ScheduledOnUtc,
                    advertisement_id AS AdvertisementId,
                    buyer_id AS BuyerId,
                    seller_id AS SellerId,
                    status AS Status
                FROM cooperations
                WHERE (buyer_id = @UserId OR seller_id = @UserId)
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
                IEnumerable<CooperationResponse> cooperations =
                    await dbConnection.QueryAsync<CooperationResponse>(
                        sqlCooperations,
                        new
                        {
                            UserId = this._userContext.UserId.Value,
                            request.Month,
                            request.Year
                        }
                    );

                IEnumerable<DateOnly> blockedDates = await dbConnection.QueryAsync<DateOnly>(
                    sqlBlockedDates,
                    new
                    {
                        UserId = this._userContext.UserId.Value,
                        request.Month,
                        request.Year
                    }
                );

                var groupedCooperations = cooperations
                    .GroupBy(c => DateOnly.FromDateTime(c.ScheduledOnUtc.UtcDateTime))
                    .Select(g => new DateResponse
                    {
                        Date = g.Key,
                        IsBlocked = blockedDates.Contains(g.Key),
                        Cooperations = g.ToList()
                    })
                    .OrderBy(g => g.Date)
                    .ToList();

                return Result.Success<IReadOnlyList<DateResponse>>(groupedCooperations);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.InnerException?.Message);
                return Result.Failure<IReadOnlyList<DateResponse>>(Error.Unexpected);
            }
        }
    }
}
