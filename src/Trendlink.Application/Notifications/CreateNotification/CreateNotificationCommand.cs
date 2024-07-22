using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Notifications.ValueObjects;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Notifications.CreateNotification
{
    public sealed record CreateNotificationCommand(
        UserId UserId,
        NotificationType NotificationType,
        Title Title,
        Message Message
    ) : ICommand<NotificationId>;
}
