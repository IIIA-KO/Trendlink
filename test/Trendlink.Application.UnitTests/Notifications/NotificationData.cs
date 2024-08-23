using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Notifications
{
    internal static class NotificationData
    {
        public static Notification Create() =>
            Notification.Create(UserId, NotificationType, Title, Message, CreatedOnUtc).Value;

        public static readonly UserId UserId = UserId.New();

        public static readonly NotificationType NotificationType = NotificationType.Message;

        public static readonly Title Title = new("Title");

        public static readonly Message Message = new("Message");

        public static readonly DateTime CreatedOnUtc = DateTime.UtcNow;
    }
}
