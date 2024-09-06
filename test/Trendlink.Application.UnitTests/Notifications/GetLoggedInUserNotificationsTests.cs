using FluentAssertions;
using MockQueryable;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Notifications;
using Trendlink.Application.Notifications.GetLoggedInUserNotifications;
using Trendlink.Application.Pagination;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Notifications
{
    public class GetLoggedInUserNotificationsTests
    {
        private static readonly GetLoggedInUserNotificationsQuery Query =
            new("CreatedOnUtc", "DESC", 1, 10);

        private readonly GetLoggedInUserNotificationsQueryHandler _handler;

        private readonly INotificationRepository _notificationRepositoryMock;
        private readonly IUserContext _userContextMock;

        public GetLoggedInUserNotificationsTests()
        {
            this._notificationRepositoryMock = Substitute.For<INotificationRepository>();
            this._userContextMock = Substitute.For<IUserContext>();

            this._handler = new GetLoggedInUserNotificationsQueryHandler(
                this._notificationRepositoryMock,
                this._userContextMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnPagedNotifications_ForLoggedInUser()
        {
            // Arrange
            UserId userId = NotificationData.UserId;
            this._userContextMock.UserId.Returns(userId);

            IQueryable<Notification> notifications = new List<Notification>
            {
                NotificationBuilder
                    .ForUser(userId)
                    .WithType(NotificationType.Message)
                    .WithTitle(NotificationData.Title.Value)
                    .WithMessage(NotificationData.Message.Value)
                    .CreatedOn(NotificationData.CreatedOnUtc)
                    .Build(),
                NotificationBuilder
                    .ForUser(userId)
                    .WithType(NotificationType.Message)
                    .WithTitle(NotificationData.Title.Value)
                    .WithMessage(NotificationData.Message.Value)
                    .CreatedOn(NotificationData.CreatedOnUtc)
                    .Build()
            }.AsQueryable();

            this._notificationRepositoryMock.SearchNotificationsForUser(
                Arg.Any<NotificationSearchParameters>(),
                Arg.Any<UserId>()
            )
                .Returns(notifications.BuildMock());

            // Act
            Result<PagedList<NotificationResponse>> result = await this._handler.Handle(
                Query,
                default
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().HaveCount(2);
            result.Value[0].Title.Should().Be(NotificationData.Title.Value);
            result.Value[1].Title.Should().Be(NotificationData.Title.Value);
        }

        [Fact]
        public async Task Handle_Should_ReturnEmpty_When_NoNotificationsFound()
        {
            // Arrange
            UserId userId = NotificationData.UserId;
            this._userContextMock.UserId.Returns(userId);

            IQueryable<Notification> emptyNotifications = new List<Notification>().AsQueryable();

            this._notificationRepositoryMock.SearchNotificationsForUser(
                Arg.Any<NotificationSearchParameters>(),
                Arg.Any<UserId>()
            )
                .Returns(emptyNotifications.BuildMock());

            // Act
            Result<PagedList<NotificationResponse>> result = await this._handler.Handle(
                Query,
                default
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEmpty();
        }
    }
}
