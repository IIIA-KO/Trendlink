using Microsoft.EntityFrameworkCore;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Notifications.ValueObjects;
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
    }
}
