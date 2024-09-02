using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Notifications.GetLoggedInUserNotifications;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users;
using Trendlink.Infrastructure.Specifications.Notifications;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class NotificationRepository
        : Repository<Notification, NotificationId>,
            INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<Notification?> GetByIdWithUser(
            NotificationId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(new NotificationByIdWithUserSpecification(id))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Result> MarkAsReadAsync(
            NotificationId id,
            CancellationToken cancellationToken = default
        )
        {
            Notification notification = await this.GetByIdAsync(id, cancellationToken);

            if (notification is null)
            {
                return Result.Failure(NotificationErrors.NotFound);
            }

            notification.MarkAsRead();

            await this.dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<bool> ExistsByIdAsync(
            NotificationId notificationId,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ExistsAsync(
                notification => notification.Id == notificationId,
                cancellationToken
            );
        }

        public IQueryable<Notification> SearchNotificationsForUser(
            NotificationSearchParameters parameters,
            UserId userId
        )
        {
            IQueryable<Notification> query = this
                .dbContext.Set<Notification>()
                .Where(notification => notification.UserId == userId);

            return parameters.SortOrder?.ToUpperInvariant() == "DESC"
                ? query.OrderByDescending(GetSortProperty(parameters))
                : query.OrderBy(GetSortProperty(parameters));
        }

        private static Expression<Func<Notification, object>> GetSortProperty(
            NotificationSearchParameters parameters
        )
        {
            return parameters.SortColumn?.ToUpperInvariant() switch
            {
                "NOTIFICATIONTYPE" => notification => notification.NotificationType,
                "TITLE" => notification => notification.Title,
                "MESSAGE" => notification => notification.Message,
                "ISREAD" => notification => notification.IsRead,
                "CREATEDONUTC" => notification => notification.CreatedOnUtc,
                _ => notification => notification.Id
            };
        }
    }
}
