using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications.ValueObjects;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Domain.Notifications
{
    public sealed class Notification : Entity<NotificationId>
    {
        private Notification(
            NotificationId id,
            UserId userId,
            NotificationType type,
            Title title,
            Message message,
            DateTime createdOnUtc
        )
            : base(id)
        {
            this.UserId = userId;
            this.NotificationType = type;
            this.Title = title;
            this.Message = message;
            this.IsRead = false;
            this.CreatedOnUtc = createdOnUtc;
        }

        private Notification() { }

        public UserId UserId { get; private set; }

        public User User { get; init; }

        public NotificationType NotificationType { get; private set; }

        public Title Title { get; private set; }

        public Message Message { get; private set; }

        public bool IsRead { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public static Result<Notification> Create(
            UserId userId,
            NotificationType type,
            Title title,
            Message message,
            DateTime createdOnUtc
        )
        {
            if (
                title is null
                || string.IsNullOrEmpty(title.Value)
                || message is null
                || string.IsNullOrEmpty(message.Value)
            )
            {
                return Result.Failure<Notification>(NotificationErrors.Invalid);
            }

            var notification = new Notification(
                NotificationId.New(),
                userId,
                type,
                title,
                message,
                createdOnUtc
            );

            return notification;
        }

        public void MarkAsRead()
        {
            if (!this.IsRead)
            {
                this.IsRead = true;
            }
        }
    }
}
