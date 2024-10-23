using Microsoft.Extensions.Options;
using Quartz;

namespace Trendlink.Infrastructure.BackgroundJobs.Token
{
    internal class CheckUserTokensJobSetup : IConfigureOptions<QuartzOptions>
    {
        private readonly TokenOptions _tokenOptions;

        public CheckUserTokensJobSetup(IOptions<TokenOptions> tokenOptions)
        {
            this._tokenOptions = tokenOptions.Value;
        }

        public void Configure(QuartzOptions options)
        {
            const string jobName = nameof(CheckUserTokensJob);

            options
                .AddJob<CheckUserTokensJob>(configure => configure.WithIdentity(jobName))
                .AddTrigger(configure =>
                    configure
                        .ForJob(jobName)
                        .WithSimpleSchedule(schedule =>
                            schedule
                                .WithIntervalInHours(this._tokenOptions.IntervalInHours)
                                .RepeatForever()
                        )
                );
        }
    }
}
