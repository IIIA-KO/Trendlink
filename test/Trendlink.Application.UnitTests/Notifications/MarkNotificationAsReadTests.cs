using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Notifications.MarkNotificationAsRead;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Notifications.ValueObjects;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.UnitTests.Notifications
{
    public class MarkNotificationAsReadTests
    {
        private static readonly MarkNotificationAsReadCommand Command = new(NotificationId.New());

        private readonly INotificationRepository _notificationRepositoryMock;
        private readonly IUserContext _userContextMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly MarkNotificationAsReadCommandHandler _handler;

        public MarkNotificationAsReadTests()
        {
            this._notificationRepositoryMock = Substitute.For<INotificationRepository>();
            this._userContextMock = Substitute.For<IUserContext>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new MarkNotificationAsReadCommandHandler(
                this._notificationRepositoryMock,
                this._userContextMock,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenNotificationNotFound()
        {
            // Arrange
            this._notificationRepositoryMock.GetByIdAsync(Command.NotificationId, default)
                .Returns((Notification?)null);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(NotificationErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserIsUnauthorized()
        {
            // Arrange
            Notification notification = NotificationData.Create();

            this._notificationRepositoryMock.GetByIdAsync(Command.NotificationId, default)
                .Returns(notification);

            this._userContextMock.UserId.Returns(UserId.New());

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Asert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotAuthorized);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            Notification notification = NotificationData.Create();

            this._notificationRepositoryMock.GetByIdAsync(Command.NotificationId, default)
                .Returns(notification);

            this._userContextMock.UserId.Returns(notification.UserId);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Asert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
