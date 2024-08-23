using Trendlink.Domain.Notifications;

namespace Trendlink.Infrastructure.Specifications.Notifications
{
    internal sealed class NotificationByIdWithUserSpecification
        : Specification<Notification, NotificationId>
    {
        public NotificationByIdWithUserSpecification(NotificationId notificationId)
            : base(notification => notification.Id == notificationId)
        {
            this.AddInclude(notification => notification.User);
        }
    }
}
