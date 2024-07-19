namespace Trendlink.Domain.Notifications.ValueObjects
{
    public sealed record NotificationId(Guid Value)
    {
        public static NotificationId New() => new(Guid.NewGuid());
    }
}
