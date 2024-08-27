using System.Data;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Infrastructure.BackgroundJobs.Outbox
{
    [DisallowConcurrentExecution]
    internal sealed class ProcessOutboxMessagesJob : IJob
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings =
            new() { TypeNameHandling = TypeNameHandling.All };

        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IPublisher _publisher;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly OutboxOptions _outboxOptions;
        private readonly ILogger<ProcessOutboxMessagesJob> _logger;

        public ProcessOutboxMessagesJob(
            ISqlConnectionFactory sqlConnectionFactory,
            IPublisher publisher,
            IDateTimeProvider dateTimeProvider,
            IOptions<OutboxOptions> outboxOptions,
            ILogger<ProcessOutboxMessagesJob> logger
        )
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
            this._publisher = publisher;
            this._dateTimeProvider = dateTimeProvider;
            this._outboxOptions = outboxOptions.Value;
            this._logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            this._logger.LogInformation("Beginning to process outbox messages");

            using IDbConnection connection = this._sqlConnectionFactory.CreateConnection();
            using IDbTransaction transaction = connection.BeginTransaction();

            IReadOnlyList<OutboxMessageResponse> outboxMessages = await this.GetOutboxMessagesAsync(
                connection,
                transaction
            );

            foreach (OutboxMessageResponse outboxMessage in outboxMessages)
            {
                Exception? exception = null;

                try
                {
                    IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                        outboxMessage.Content,
                        JsonSerializerSettings
                    )!;

                    await this._publisher.Publish(domainEvent, context.CancellationToken);
                }
                catch (Exception caughtException)
                {
                    this._logger.LogError(
                        caughtException,
                        "Exception while processing outbox message {MessageId}",
                        outboxMessage.Id
                    );

                    exception = caughtException;
                }

                await this.UpdateOutboxMessageAsync(
                    connection,
                    transaction,
                    outboxMessage,
                    exception
                );
            }

            transaction.Commit();

            this._logger.LogInformation("Completed processing outbox messages");
        }

        private async Task<IReadOnlyList<OutboxMessageResponse>> GetOutboxMessagesAsync(
            IDbConnection connection,
            IDbTransaction transaction
        )
        {
            string sql = $"""
                SELECT id, content
                FROM outbox_messages
                WHERE processed_on_utc IS NULL
                LIMIT {this._outboxOptions.BatchSize}
                FOR UPDATE
                """;

            IEnumerable<OutboxMessageResponse> outboxMessages =
                await connection.QueryAsync<OutboxMessageResponse>(sql, transaction: transaction);

            return outboxMessages.ToList();
        }

        private async Task UpdateOutboxMessageAsync(
            IDbConnection connection,
            IDbTransaction transaction,
            OutboxMessageResponse outboxMessage,
            Exception? exception
        )
        {
            const string sql =
                @"
                  UPDATE outbox_messages
                  SET processed_on_utc = @ProcessedOnUtc, error = @Error
                  WHERE id = @Id";

            await connection.ExecuteAsync(
                sql,
                new
                {
                    outboxMessage.Id,
                    ProcessedOnUtc = this._dateTimeProvider.UtcNow,
                    Error = exception?.ToString()
                },
                transaction: transaction
            );
        }
    }
}
