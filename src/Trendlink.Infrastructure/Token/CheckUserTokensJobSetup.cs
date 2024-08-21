using Microsoft.Extensions.Options;
using Quartz;

namespace Trendlink.Infrastructure.Token
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
            const string jobName = nameof(CheckUserTokensJobSetup);

            options
                .AddJob<CheckUserTokensJob>(configure => configure.WithIdentity(jobName))
                .AddTrigger(configure =>
                    configure
                        .ForJob(jobName)
                        .WithSimpleSchedule(schedule =>
                            schedule
                                .WithIntervalInSeconds(this._tokenOptions.IntervalInSeconds)
                                .RepeatForever()
                        )
                );
        }
    }
}
