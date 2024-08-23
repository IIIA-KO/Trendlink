namespace Trendlink.Domain.Notifications
{
    public sealed record NotificationId(Guid Value)
    {
        public static NotificationId New() => new(Guid.NewGuid());
    }
}
