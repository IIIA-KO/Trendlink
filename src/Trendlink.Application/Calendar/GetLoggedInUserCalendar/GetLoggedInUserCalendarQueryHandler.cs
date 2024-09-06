using System.Data;
using Dapper;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Calendar.GetLoggedInUserCalendar
{
    internal sealed class GetLoggedInUserCalendarQueryHandler
        : IQueryHandler<GetLoggedInUserCalendarQuery, IReadOnlyList<LoggedInDateResponse>>
    {
        private readonly IUserContext _userContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetLoggedInUserCalendarQueryHandler(
            IUserContext userContext,
            ISqlConnectionFactory sqlConnectionFactory
        )
        {
            this._userContext = userContext;
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<LoggedInDateResponse>>> Handle(
            GetLoggedInUserCalendarQuery request,
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
                    price_amount AS PriceAmount,
                    price_currency AS PriceCurrency,
                    advertisement_id AS AdvertisementId,
                    buyer_id AS BuyerId,
                    seller_id AS SellerId,
                    status AS Status
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
                var userId = new { UserId = this._userContext.UserId.Value };

                IEnumerable<CooperationResponse> cooperations =
                    await dbConnection.QueryAsync<CooperationResponse>(sqlCooperations, userId);

                IEnumerable<DateOnly> blockedDates = await dbConnection.QueryAsync<DateOnly>(
                    sqlBlockedDates,
                    userId
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
            catch (Exception)
            {
                return Result.Failure<IReadOnlyList<LoggedInDateResponse>>(Error.Unexpected);
            }
        }
    }
}
