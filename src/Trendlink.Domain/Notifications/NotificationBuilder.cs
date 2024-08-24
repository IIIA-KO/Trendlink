using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Domain.Notifications
{
    public sealed class NotificationBuilder
    {
        private UserId _userId;
        private NotificationType _type;
        private Title _title;
        private Message _message;
        private DateTime _createdOnUtc;

        private NotificationBuilder() { }

        public static NotificationBuilder CreateBuilder()
        {
            return new NotificationBuilder();
        }

        public NotificationBuilder ForUser(UserId userId)
        {
            this._userId = userId;
            return this;
        }

        public NotificationBuilder WithType(NotificationType type)
        {
            this._type = type;
            return this;
        }

        public NotificationBuilder WithTitle(string title)
        {
            this._title = new Title(title);
            return this;
        }

        public NotificationBuilder WithMessage(string message)
        {
            this._message = new Message(message);
            return this;
        }

        public NotificationBuilder CreatedOn(DateTime createdOnUtc)
        {
            this._createdOnUtc = createdOnUtc;
            return this;
        }

        public Result<Notification> Build()
        {
            if (
                this._title is null
                || string.IsNullOrEmpty(this._title.Value)
                || this._message is null
                || string.IsNullOrEmpty(this._message.Value)
            )
            {
                return Result.Failure<Notification>(NotificationErrors.Invalid);
            }

            return new Notification(
                NotificationId.New(),
                this._userId,
                this._type,
                this._title,
                this._message,
                this._createdOnUtc
            );
        }
    }
}
