using Trendlink.Domain.Users;

namespace Trendlink.Domain.Notifications
{
    public sealed class NotificationBuilder : INotificationBuilder
    {
        public UserId UserId { get; set; }
        public NotificationType Type { get; set; }
        public Title Title { get; set; }
        public Message Message { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public NotificationBuilder()
        {
            this.UserId = UserId.New();
            this.Type = NotificationType.System;
            this.Title = new Title(string.Empty);
            this.Message = new Message(string.Empty);
            this.CreatedOnUtc = DateTime.UtcNow;
        }

        public static IExpectsType ForUser(UserId userId)
        {
            return new NotificationBuilder { UserId = userId };
        }

        public IExpectsTitle WithType(NotificationType type)
        {
            this.Type = type;
            return this;
        }

        public IExpectsMessage WithTitle(string title)
        {
            this.Title = new Title(ValidateTitle(title));
            return this;
        }

        private static string ValidateTitle(string title)
        {
            return !string.IsNullOrEmpty(title)
                ? title
                : throw new ArgumentException("Title is required.", nameof(title));
        }

        public IExpectsCreationDate WithMessage(string message)
        {
            this.Message = new Message(ValidateMessage(message));
            return this;
        }

        private static string ValidateMessage(string message)
        {
            return !string.IsNullOrEmpty(message)
                ? message
                : throw new ArgumentException("Message is required.", nameof(message));
        }

        public INotificationBuilder CreatedOn(DateTime createdOnUtc)
        {
            this.CreatedOnUtc = createdOnUtc;
            return this;
        }

        public Notification Build()
        {
            return new Notification(
                NotificationId.New(),
                this.UserId,
                this.Type,
                this.Title,
                this.Message,
                this.CreatedOnUtc
            );
        }
    }
}
