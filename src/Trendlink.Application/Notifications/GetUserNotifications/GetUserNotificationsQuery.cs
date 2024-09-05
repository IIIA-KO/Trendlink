using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Pagination;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Notifications.GetUserNotifications
{
    public sealed record GetUserNotificationsQuery(
        UserId UserId,
        string? SortColumn,
        string? SortOrder,
        int PageNumber,
        int PageSize
    ) : IQuery<PagedList<NotificationResponse>>;
}
