using System.Data;
using Dapper;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Notifications.GetUserNotifications
{
    internal sealed class GetUserNotificationsQueryHandler
        : IQueryHandler<GetUserNotificationsQuery, IReadOnlyList<NotificationResponse>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IUserRepository _userRepository;

        public GetUserNotificationsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IUserRepository userRepository
        )
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
            this._userRepository = userRepository;
        }

        public async Task<Result<IReadOnlyList<NotificationResponse>>> Handle(
            GetUserNotificationsQuery request,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByIdWithRolesAsync(
                request.UserId,
                cancellationToken
            );
            if (user is null)
            {
                return Result.Failure<IReadOnlyList<NotificationResponse>>(UserErrors.NotFound);
            }

            if (!user.HasRole(Role.Administrator))
            {
                return Result.Failure<IReadOnlyList<NotificationResponse>>(
                    UserErrors.NotAuthorized
                );
            }

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
                """;

            try
            {
                return (
                    await dbConnection.QueryAsync<NotificationResponse>(
                        sql,
                        new { UserId = user.Id.Value }
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
