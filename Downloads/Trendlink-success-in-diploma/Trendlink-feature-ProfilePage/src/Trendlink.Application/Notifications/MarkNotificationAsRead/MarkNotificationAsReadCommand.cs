using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Notifications;

namespace Trendlink.Application.Notifications.MarkNotificationAsRead
{
    public sealed record MarkNotificationAsReadCommand(NotificationId NotificationId) : ICommand;
}
