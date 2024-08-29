using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;
using Trendlink.Infrastructure.Authentication.Instagram;
using Trendlink.Infrastructure.BackgroundJobs.Token;

namespace Trendlink.Infrastructure.BackgroundJobs.InstagramAccounts
{
    [DisallowConcurrentExecution]
    internal sealed class UpdateInstagramAccontsJob : IJob
    {
        private const int DaysToCheck = 1;

        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IInstagramService _instagramService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly InstagramOptions _instagramOptions;
        private readonly ILogger<UpdateInstagramAccontsJob> _logger;

        public UpdateInstagramAccontsJob(
            ISqlConnectionFactory sqlConnectionFactory,
            IDateTimeProvider dateTimeProvider,
            IInstagramService instagramService,
            IUnitOfWork unitOfWork,
            IOptions<InstagramOptions> instagramOptions,
            ILogger<UpdateInstagramAccontsJob> logger
        )
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
            this._dateTimeProvider = dateTimeProvider;
            this._instagramService = instagramService;
            this._unitOfWork = unitOfWork;
            this._instagramOptions = instagramOptions.Value;
            this._logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            this._logger.LogInformation("Beggining to update Instagram accounts");

            using IDbConnection connection = this._sqlConnectionFactory.CreateConnection();
            using IDbTransaction transaction = connection.BeginTransaction();

            IReadOnlyList<InstagramAccountResponse> instagramAccounts =
                await this.GetInstagramAccount(connection, transaction);

            foreach (InstagramAccountResponse instagramAccount in instagramAccounts)
            {
                try
                {
                    await this.UpdateInstagramAccountMetadata(
                        connection,
                        transaction,
                        instagramAccount
                    );

                    await this._unitOfWork.SaveChangesAsync();
                }
                catch (Exception exception)
                {
                    this._logger.LogError(
                        exception,
                        "Exception while updating Instagram account {InstagramAccountId}",
                        instagramAccount.Id
                    );

                    transaction.Rollback();
                }

                await this.UpdateInstagramAccount(connection, transaction, instagramAccount);
            }

            transaction.Commit();

            this._logger.LogInformation("Completed updating Instagram accounts");
        }

        private async Task UpdateInstagramAccountMetadata(
            IDbConnection connection,
            IDbTransaction transaction,
            InstagramAccountResponse instagramAccountResponse
        )
        {
            this._logger.LogInformation(
                "Updating Instagram account metadata {InstagramAccountId} for user {UserId}",
                instagramAccountResponse.Id,
                instagramAccountResponse.UserId
            );

            Guid userId = await connection.QueryFirstOrDefaultAsync<Guid>(
                "SELECT id FROM users WHERE id = @UserId",
                new { instagramAccountResponse.UserId },
                transaction: transaction
            );

            UserTokenResponse? userToken =
                await connection.QueryFirstOrDefaultAsync<UserTokenResponse>(
                    @"
                          SELECT
                            id AS Id,
                            user_id AS UserId,
                            access_token AS AccessToken,
                            expires_at_utc AS ExpiresAtUtc
                          FROM user_tokens
                          WHERE user_id = @UserId",
                    new { UserId = userId },
                    transaction: transaction
                );

            InstagramAccountResponse? currentInstagramAccount =
                await connection.QueryFirstOrDefaultAsync<InstagramAccountResponse>(
                    @"
                          SELECT
                            id AS Id,
                            user_id AS UserId,
                            facebook_page_id AS FacebookPageId,
                            metadata_id AS MetadataId,
                            metadata_ig_id AS MetadataIgId,
                            metadata_user_name AS MetadataUserName,
                            metadata_followers_count AS MetadataFollowersCount,
                            metadata_media_count AS MetadataMediaCount
                          FROM instagram_accounts
                          WHERE user_id = @UserId",
                    new { UserId = userId },
                    transaction: transaction
                );

            if (userToken is null || currentInstagramAccount is null)
            {
                this._logger.LogWarning("UserToken or InstagramAccount not found");
                return;
            }

            Result<InstagramUserInfo> userInfo = await this._instagramService.GetUserInfoAsync(
                userToken.AccessToken,
                currentInstagramAccount.FacebookPageId,
                currentInstagramAccount.MetadataUserName
            );

            if (userInfo is null)
            {
                this._logger.LogWarning("Unable to get Instagram account metadata");
                return;
            }

            InstagramAccount updatedInstagramAccount = userInfo.Value.CreateInstagramAccount(
                new UserId(userId)
            );

            const string updateQuery =
                @"
                  UPDATE instagram_accounts
                  SET metadata_id = @MetadataId,
                      metadata_ig_id = @MetadataIgId,
                      metadata_user_name = @MetadataUserName,
                      metadata_followers_count = @MetadataFollowersCount,
                      metadata_media_count = @MetadataMediaCount
                  WHERE id = @Id::uuid";

            await connection.ExecuteAsync(
                updateQuery,
                new
                {
                    MetadataId = updatedInstagramAccount.Metadata.Id,
                    MetadataIgId = updatedInstagramAccount.Metadata.IgId,
                    MetadataUserName = updatedInstagramAccount.Metadata.UserName,
                    MetadataFollowersCount = updatedInstagramAccount.Metadata.FollowersCount,
                    MetadataMediaCount = updatedInstagramAccount.Metadata.MediaCount,
                    currentInstagramAccount.Id
                },
                transaction: transaction
            );

            this._logger.LogInformation("Instagram account metadata updated successfully");
        }

        private async Task<IReadOnlyList<InstagramAccountResponse>> GetInstagramAccount(
            IDbConnection connection,
            IDbTransaction transaction
        )
        {
            string sql = $"""
                    SELECT
                        id AS Id,
                        user_id AS UserId,
                        facebook_page_id AS FacebookPageId,
                        metadata_id AS MetadataId,
                        metadata_ig_id AS MetadataIgId,
                        metadata_user_name AS MetadataUserName,
                        metadata_followers_count AS MetadataFollowersCount,
                        metadata_media_count AS MetadataMediaCount
                    FROM instagram_accounts
                    WHERE last_updated_at_utc IS NULL
                        OR last_updated_at_utc <= CURRENT_DATE - INTERVAL '{DaysToCheck} day'
                    ORDER BY last_updated_at_utc ASC NULLS FIRST
                    LIMIT {this._instagramOptions.BatchSize}
                """;

            IEnumerable<InstagramAccountResponse> instagramAccounts =
                await connection.QueryAsync<InstagramAccountResponse>(
                    sql,
                    transaction: transaction
                );

            return instagramAccounts.ToList();
        }

        private async Task UpdateInstagramAccount(
            IDbConnection connection,
            IDbTransaction transaction,
            InstagramAccountResponse instagramAccount
        )
        {
            const string sql =
                @"
                  UPDATE instagram_accounts
                  SET last_updated_at_utc = @LastUpdatedAtUtc
                  WHERE id = @Id";

            await connection.ExecuteAsync(
                sql,
                new { LastUpdatedAtUtc = this._dateTimeProvider.UtcNow, instagramAccount.Id },
                transaction: transaction
            );
        }
    }
}
