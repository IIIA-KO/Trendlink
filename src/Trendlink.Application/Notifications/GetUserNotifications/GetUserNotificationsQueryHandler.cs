using System.Linq.Expressions;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Pagination;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Notifications.GetUserNotifications
{
    internal sealed class GetUserNotificationsQueryHandler
        : IQueryHandler<GetUserNotificationsQuery, PagedList<NotificationResponse>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;

        public GetUserNotificationsQueryHandler(
            INotificationRepository notificationRepository,
            IUserRepository userRepository,
            IUserContext userContext
        )
        {
            this._notificationRepository = notificationRepository;
            this._userRepository = userRepository;
            this._userContext = userContext;
        }

        public async Task<Result<PagedList<NotificationResponse>>> Handle(
            GetUserNotificationsQuery request,
            CancellationToken cancellationToken
        )
        {
            bool userExists = await this._userRepository.ExistsByIdAsync(
                request.UserId,
                cancellationToken
            );
            if (!userExists)
            {
                return Result.Failure<PagedList<NotificationResponse>>(UserErrors.NotFound);
            }

            User user = await this._userRepository.GetByIdWithRolesAsync(
                this._userContext.UserId,
                cancellationToken
            );
            if (!user!.HasRole(Role.Administrator))
            {
                return Result.Failure<PagedList<NotificationResponse>>(UserErrors.NotAuthorized);
            }

            IQueryable<Notification> notificationsQuery =
                this._notificationRepository.DbSetAsQueryable();

            notificationsQuery = notificationsQuery.Where(notification =>
                notification.UserId == request.UserId
            );

            notificationsQuery =
                request.SortOrder?.ToUpperInvariant() == "DESC"
                    ? notificationsQuery.OrderByDescending(GetSortProperty(request))
                    : notificationsQuery.OrderBy(GetSortProperty(request));

            IQueryable<NotificationResponse> notificationResponsesQuery = notificationsQuery.Select(
                notification => new NotificationResponse(
                    notification.Id.Value,
                    notification.UserId.Value,
                    notification.NotificationType,
                    notification.Title.Value,
                    notification.Message.Value,
                    notification.IsRead,
                    notification.CreatedOnUtc
                )
            );

            return await PagedList<NotificationResponse>.CreateAsync(
                notificationResponsesQuery,
                request.PageNumber,
                request.PageSize
            );
        }

        private static Expression<Func<Notification, object>> GetSortProperty(
            GetUserNotificationsQuery request
        )
        {
            return request.SortColumn?.ToUpperInvariant() switch
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
