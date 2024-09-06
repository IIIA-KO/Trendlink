namespace Trendlink.Infrastructure.BackgroundJobs.Outbox
{
    internal sealed class OutboxOptions
    {
        public int IntervalInSeconds { get; init; }

        public int BatchSize { get; init; }
    }
}
