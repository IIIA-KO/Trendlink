using System.Data;
using Dapper;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Cooperations.GetLoggedInUserCooperations
{
    internal sealed class GetLoggedInUserCooperationsQueryHandler
        : IQueryHandler<GetLoggedInUserCooperationsQuery, IReadOnlyList<CooperationResponse>>
    {
        private readonly IUserContext _userContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetLoggedInUserCooperationsQueryHandler(
            IUserContext userContext,
            ISqlConnectionFactory sqlConnectionFactory
        )
        {
            this._userContext = userContext;
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<CooperationResponse>>> Handle(
            GetLoggedInUserCooperationsQuery request,
            CancellationToken cancellationToken
        )
        {
            using IDbConnection dbConnection = this._sqlConnectionFactory.CreateConnection();

            const string sql = """
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
                WHERE buyer_id = @UserId OR seller_id = @UserId
                """;

            try
            {
                return (
                    await dbConnection.QueryAsync<CooperationResponse>(
                        sql,
                        new { UserId = this._userContext.UserId.Value }
                    )
                ).ToList();
            }
            catch (Exception)
            {
                return Result.Failure<IReadOnlyList<CooperationResponse>>(Error.Unexpected);
            }
        }
    }
}
