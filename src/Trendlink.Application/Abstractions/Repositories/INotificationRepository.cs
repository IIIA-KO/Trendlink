using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Abstractions.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification?> GetByIdAsync(
            NotificationId id,
            CancellationToken cancellationToken = default
        );

        Task<Notification?> GetByIdWithUser(
            NotificationId id,
            CancellationToken cancellationToken = default
        );

        void Add(Notification notification);

        Task<Result> MarkAsReadAsync(
            NotificationId id,
            CancellationToken cancellationToken = default
        );

        Task<bool> ExistsByIdAsync(
            NotificationId notificationId,
            CancellationToken cancellationToken = default
        );

        IQueryable<Notification> SearchNotificationsForUser(
            NotificationSearchParameters parameters,
            UserId userId
        );
    }

    public sealed record NotificationSearchParameters(string? SortColumn, string? SortOrder);
}
