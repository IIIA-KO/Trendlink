using Microsoft.Extensions.Options;
using Quartz;
using Trendlink.Infrastructure.Emails;

namespace Trendlink.Infrastructure.BackgroundJobs.EmailVerificationTokens
{
    internal sealed class DeleteExpiredEmailVerificationTokensJobSetup
        : IConfigureOptions<QuartzOptions>
    {
        private readonly EmailOptions _emailOptions;

        public DeleteExpiredEmailVerificationTokensJobSetup(IOptions<EmailOptions> emailOptions)
        {
            this._emailOptions = emailOptions.Value;
        }

        public void Configure(QuartzOptions options)
        {
            const string jobName = nameof(DeleteExpiredEmailVerificationTokensJobSetup);

            options
                .AddJob<DeleteExpiredEmailVerificationTokensJob>(configure =>
                    configure.WithIdentity(jobName)
                )
                .AddTrigger(configure =>
                    configure
                        .ForJob(jobName)
                        .WithSimpleSchedule(schedule =>
                            schedule
                                .WithIntervalInHours(this._emailOptions.IntervalInHours)
                                .RepeatForever()
                        )
                );
        }
    }
}
