﻿using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Pagination;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;

namespace Trendlink.Application.Notifications.GetLoggedInUserNotifications
{
    internal sealed class GetLoggedInUserNotificationsQueryHandler
        : IQueryHandler<GetLoggedInUserNotificationsQuery, PagedList<NotificationResponse>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserContext _userContext;

        public GetLoggedInUserNotificationsQueryHandler(
            INotificationRepository notificationRepository,
            IUserContext userContext
        )
        {
            this._notificationRepository = notificationRepository;
            this._userContext = userContext;
        }

        public async Task<Result<PagedList<NotificationResponse>>> Handle(
            GetLoggedInUserNotificationsQuery request,
            CancellationToken cancellationToken
        )
        {
            IQueryable<Notification> notificationsQuery =
                this._notificationRepository.SearchNotificationsForUser(
                    new NotificationSearchParameters(request.SortColumn, request.SortOrder),
                    this._userContext.UserId
                );

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
    }
}
