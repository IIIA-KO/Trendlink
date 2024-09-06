namespace Trendlink.Infrastructure.BackgroundJobs.Outbox
{
    internal sealed record OutboxMessageResponse(Guid Id, string Content);
}
