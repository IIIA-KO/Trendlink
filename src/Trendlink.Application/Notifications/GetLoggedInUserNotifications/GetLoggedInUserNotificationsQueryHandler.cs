using System.Data;
using Dapper;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Notifications.GetLoggedInUserNotifications
{
    internal sealed class GetLoggedInUserNotificationsQueryHandler
        : IQueryHandler<GetLoggedInUserNotificationsQuery, IReadOnlyList<NotificationResponse>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IUserContext _userContext;

        public GetLoggedInUserNotificationsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IUserContext userContext
        )
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
            this._userContext = userContext;
        }

        public async Task<Result<IReadOnlyList<NotificationResponse>>> Handle(
            GetLoggedInUserNotificationsQuery request,
            CancellationToken cancellationToken
        )
        {
            using IDbConnection dbConnection = this._sqlConnectionFactory.CreateConnection();

            const string sql = """
                SELECT
                    id AS Id,
                    user_id AS UserId,
                    notification_type AS NotificationType,
                    title AS Title,
                    message AS Message,
                    is_read AS IsRead,
                    created_on_utc AS CreatedOnUtc
                FROM notifications
                WHERE user_id = @UserId
                ORDER BY created_on_utc DESC
                """;

            try
            {
                return (
                    await dbConnection.QueryAsync<NotificationResponse>(
                        sql,
                        new { UserId = this._userContext.UserId.Value }
                    )
                ).ToList();
            }
            catch (Exception)
            {
                return Result.Failure<IReadOnlyList<NotificationResponse>>(Error.Unexpected);
            }
        }
    }
}
