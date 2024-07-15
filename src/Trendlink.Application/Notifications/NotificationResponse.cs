using Trendlink.Domain.Notifications;

namespace Trendlink.Application.Notifications
{
    public class NotificationResponse
    {
        public NotificationResponse() { }

        public NotificationResponse(
            Guid id,
            Guid userId,
            NotificationType notificationType,
            string title,
            string message,
            bool isRead,
            DateTime createdOnUtc
        )
        {
            this.Id = id;
            this.UserId = userId;
            this.NotificationType = notificationType;
            this.Title = title;
            this.Message = message;
            this.IsRead = isRead;
            this.CreatedOnUtc = createdOnUtc;
        }

        public Guid Id { get; init; }

        public Guid UserId { get; init; }

        public NotificationType NotificationType { get; init; }

        public string Title { get; init; }

        public string Message { get; init; }

        public bool IsRead { get; init; }

        public DateTime CreatedOnUtc { get; init; }
    }
}
