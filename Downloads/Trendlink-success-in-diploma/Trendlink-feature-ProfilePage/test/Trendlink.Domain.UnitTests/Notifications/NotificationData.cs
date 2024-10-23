using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users;

namespace Trendlink.Domain.UnitTests.Notifications
{
    internal static class NotificationData
    {
        public static readonly UserId UserId = UserId.New();

        public static readonly NotificationType NotificationType = NotificationType.Message;

        public static readonly Title Title = new("Title");

        public static readonly Message Message = new("Message");

        public static readonly DateTime CreatedOnUtc = DateTime.UtcNow;
    }
}
