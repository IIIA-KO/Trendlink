using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Infrastructure.Authentication.Instagram;

namespace Trendlink.Infrastructure.Token
{
    internal class CheckUserTokensJob : IJob
    {
        private const int DaysToCheck = 14;

        private readonly HttpClient _httpClient;
        private readonly InstagramOptions _instagramOptions;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly TokenOptions _tokenOptions;
        private readonly ILogger<CheckUserTokensJob> _logger;

        public CheckUserTokensJob(
            HttpClient httpClient,
            IOptions<InstagramOptions> instagramOptions,
            ISqlConnectionFactory sqlConnectionFactory,
            IDateTimeProvider dateTimeProvider,
            IOptions<TokenOptions> tokenOptions,
            ILogger<CheckUserTokensJob> logger
        )
        {
            this._httpClient = httpClient;
            this._instagramOptions = instagramOptions.Value;
            this._sqlConnectionFactory = sqlConnectionFactory;
            this._dateTimeProvider = dateTimeProvider;
            this._tokenOptions = tokenOptions.Value;
            this._logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            this._logger.LogInformation("Beginning to check user tokens");

            using IDbConnection connection = this._sqlConnectionFactory.CreateConnection();
            using IDbTransaction transaction = connection.BeginTransaction();

            IReadOnlyList<UserTokenResponse> userTokens = await this.GetUserTokensAsync(
                connection,
                transaction
            );

            foreach (UserTokenResponse userToken in userTokens)
            {
                try
                {
                    bool isValid = await this.ValidateTokenAsync(userToken.AccessToken);

                    if (!isValid)
                    {
                        this._logger.LogWarning(
                            "Token {UserTokenId} is invalid, consider updating or requesting a new one.",
                            userToken.Id
                        );
                    }
                }
                catch (Exception caughtException)
                {
                    this._logger.LogError(
                        caughtException,
                        "Exception while checking user token {UserTokenId}",
                        userToken.Id
                    );
                }

                await this.UpdateUserTokensAsync(connection, transaction, userToken);
            }

            transaction.Commit();

            this._logger.LogInformation("Completed checking user tokens");
        }

        private async Task<IReadOnlyList<UserTokenResponse>> GetUserTokensAsync(
            IDbConnection connection,
            IDbTransaction transaction
        )
        {
            string sql = $"""
                SELECT id, access_token,
                FROM user_token
                WHERE last_checked_on_utc IS NULL
                    OR last_checked_on_utc < NOW() - INTERVAL '{DaysToCheck} days'
                ORDER BY last_checked_on_utc ASC NULLS FIRST
                LIMIT {this._tokenOptions.BatchSize}
                """;

            IEnumerable<UserTokenResponse> userTokens =
                await connection.QueryAsync<UserTokenResponse>(sql, transaction: transaction);

            return userTokens.ToList();
        }

        private async Task UpdateUserTokensAsync(
            IDbConnection connection,
            IDbTransaction transaction,
            UserTokenResponse userToken
        )
        {
            const string sql =
                @"
                  UPDATE user_tokens
                  SET last_checked_on_utc = @LastCheckedOnUtc
                  WHERE id = @Id";

            await connection.ExecuteAsync(
                sql,
                new { LastCheckedOnUtc = this._dateTimeProvider.UtcNow, userToken.Id },
                transaction: transaction
            );
        }

        private async Task<bool> ValidateTokenAsync(string accessToken)
        {
            string validateTokenUrl =
                $"https://graph.facebook.com/oauth/access_token_info?"
                + $"client_id={this._instagramOptions.ClientId}"
                + $"&access_token={accessToken}";

            HttpResponseMessage response = await this._httpClient.GetAsync(validateTokenUrl);
            return response.IsSuccessStatusCode;
        }
    }
}
