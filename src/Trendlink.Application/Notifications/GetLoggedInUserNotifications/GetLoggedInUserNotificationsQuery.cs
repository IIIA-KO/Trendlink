using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Notifications.GetLoggedInUserNotifications
{
    public sealed record GetLoggedInUserNotificationsQuery
        : IQuery<IReadOnlyList<NotificationResponse>>;
}
