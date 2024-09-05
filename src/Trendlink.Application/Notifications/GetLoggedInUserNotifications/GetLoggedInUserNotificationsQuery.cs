using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Pagination;

namespace Trendlink.Application.Notifications.GetLoggedInUserNotifications
{
    public sealed record GetLoggedInUserNotificationsQuery(
        string? SortColumn,
        string? SortOrder,
        int PageNumber,
        int PageSize
    ) : IQuery<PagedList<NotificationResponse>>;
}
