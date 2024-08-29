using Microsoft.Extensions.Options;
using Quartz;
using Trendlink.Infrastructure.Authentication.Instagram;

namespace Trendlink.Infrastructure.BackgroundJobs.InstagramAccounts
{
    internal sealed class UpdateInstagramAccountJobSetup : IConfigureOptions<QuartzOptions>
    {
        private readonly InstagramOptions _instagramOptions;

        public UpdateInstagramAccountJobSetup(IOptions<InstagramOptions> instagramOptions)
        {
            this._instagramOptions = instagramOptions.Value;
        }

        public void Configure(QuartzOptions options)
        {
            const string jobName = nameof(UpdateInstagramAccontsJob);

            options
                .AddJob<UpdateInstagramAccontsJob>(configure => configure.WithIdentity(jobName))
                .AddTrigger(configure =>
                    configure
                        .ForJob(jobName)
                        .WithSimpleSchedule(schedule =>
                            schedule
                                .WithIntervalInSeconds(this._instagramOptions.IntervalInSeconds)
                                .RepeatForever()
                        )
                );
        }
    }
}
