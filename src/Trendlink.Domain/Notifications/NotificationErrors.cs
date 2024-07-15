using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Notifications
{
    public static class NotificationErrors
    {
        public static readonly NotFoundError NotFound =
            new(
                "Notification.NotFound",
                "The notification with the specified identifier was not found"
            );

        public static readonly Error Invalid =
            new("Notification.Invalid", "The provided notification content was invalid");
    }
}
