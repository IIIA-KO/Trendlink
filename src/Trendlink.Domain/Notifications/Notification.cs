using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Domain.Notifications
{
    public sealed class Notification : Entity<NotificationId>
    {
        internal Notification(
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

        public void MarkAsRead()
        {
            if (!this.IsRead)
            {
                this.IsRead = true;
            }
        }
    }
}
