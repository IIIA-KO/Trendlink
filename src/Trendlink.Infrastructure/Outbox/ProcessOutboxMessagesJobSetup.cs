using Microsoft.Extensions.Options;
using Quartz;

namespace Trendlink.Infrastructure.Outbox
{
    internal sealed class ProcessOutboxMessagesJobSetup : IConfigureOptions<QuartzOptions>
    {
        private readonly OutboxOptions _outboxOptions;

        public ProcessOutboxMessagesJobSetup(IOptions<OutboxOptions> outboxOptions)
        {
            this._outboxOptions = outboxOptions.Value;
        }

        public void Configure(QuartzOptions options)
        {
            const string jobName = nameof(ProcessOutboxMessagesJob);

            options
                .AddJob<ProcessOutboxMessagesJob>(configure => 
                    configure
                    .WithIdentity(jobName))
                    .AddTrigger(configure => configure
                        .ForJob(jobName)
                        .WithSimpleSchedule(schedule => schedule
                            .WithIntervalInSeconds(this._outboxOptions.IntervalInSeconds)
                            .RepeatForever()));
        }
    }
}
