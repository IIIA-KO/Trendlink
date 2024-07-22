using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Notifications.GetUserNotifications
{
    public sealed record GetUserNotificationsQuery(UserId UserId)
        : IQuery<IReadOnlyList<NotificationResponse>>;
}
