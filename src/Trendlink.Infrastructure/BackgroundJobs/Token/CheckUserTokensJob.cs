using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using Quartz;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users;

namespace Trendlink.Infrastructure.BackgroundJobs.Token
{
    internal class CheckUserTokensJob : IJob
    {
        private const int DaysToCheck = 7;

        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly INotificationRepository _notificationRepository;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CheckUserTokensJob> _logger;

        public CheckUserTokensJob(
            IDateTimeProvider dateTimeProvider,
            INotificationRepository notificationRepository,
            ISqlConnectionFactory sqlConnectionFactory,
            IUnitOfWork unitOfWork,
            ILogger<CheckUserTokensJob> logger
        )
        {
            this._dateTimeProvider = dateTimeProvider;
            this._notificationRepository = notificationRepository;
            this._sqlConnectionFactory = sqlConnectionFactory;
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            this._logger.LogInformation("Beginning to check user tokens");

            using IDbConnection connection = this._sqlConnectionFactory.CreateConnection();
            using IDbTransaction transaction = connection.BeginTransaction();

            IReadOnlyList<UserTokenResponse> userTokens = await GetUserTokensAsync(
                connection,
                transaction
            );

            foreach (UserTokenResponse userToken in userTokens)
            {
                Exception? exception = null;

                try
                {
                    this.SendNotification(userToken);

                    await this._unitOfWork.SaveChangesAsync();
                }
                catch (Exception caughtException)
                {
                    this._logger.LogError(
                        caughtException,
                        "Exception while checking user token {UserTokenId}",
                        userToken.Id
                    );

                    exception = caughtException;
                }

                await this.UpdateUserTokenAsync(connection, transaction, userToken, exception);
            }

            transaction.Commit();

            this._logger.LogInformation("Completed checking user tokens");
        }

        private static async Task<IReadOnlyList<UserTokenResponse>> GetUserTokensAsync(
            IDbConnection connection,
            IDbTransaction transaction
        )
        {
            string sql = $"""
                SELECT 
                    id AS Id, 
                    user_id AS UserId,
                    access_token AS AccessToken,
                    expires_at_utc AS ExpiresAtUtc
                FROM user_tokens
                WHERE expires_at_utc <= CURRENT_DATE + INTERVAL '{DaysToCheck} days'
                ORDER BY expires_at_utc ASC
                """;

            IEnumerable<UserTokenResponse> userTokens =
                await connection.QueryAsync<UserTokenResponse>(sql, transaction: transaction);

            return userTokens.ToList();
        }

        private void SendNotification(UserTokenResponse userToken)
        {
            Notification notification = NotificationBuilder
                .ForUser(new UserId(userToken.UserId))
                .WithType(NotificationType.Alert)
                .WithTitle("Renew Instagram Permissions to Continue Using Trendlink")
                .WithMessage(
                    "Your Instagram permissions will expire in 7 days.\r\nTo continue using all the features of Trendlink, please renew your permissions."
                )
                .CreatedOn(this._dateTimeProvider.UtcNow)
                .Build();

            this._notificationRepository.Add(notification);
        }

        private async Task UpdateUserTokenAsync(
            IDbConnection connection,
            IDbTransaction transaction,
            UserTokenResponse userToken,
            Exception? exception
        )
        {
            const string sql =
                @"
                  UPDATE user_tokens
                  SET last_checked_on_utc = @LastCheckedOnUtc, error = @Error
                  WHERE id = @Id";

            await connection.ExecuteAsync(
                sql,
                new
                {
                    LastCheckedOnUtc = this._dateTimeProvider.UtcNow,
                    Error = exception?.ToString(),
                    userToken.Id
                },
                transaction: transaction
            );
        }
    }
}
