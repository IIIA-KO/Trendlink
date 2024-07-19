using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.SignalR.Notifications;
using Trendlink.Application.Notifications.CreateNotification;
using Trendlink.Application.UnitTests.Users;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Notifications.ValueObjects;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.UnitTests.Notifications
{
    public class CreateNotificationTests
    {
        private static readonly CreateNotificationCommand Command =
            new(
                UserId.New(),
                NotificationData.NotificationType,
                NotificationData.Title,
                NotificationData.Message
            );

        private readonly CreateNotificationCommandHandler _handler;

        private readonly IDateTimeProvider _dateTimeProviderMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly INotificationRepository _notificationRepositoryMock;
        private readonly INotificationService _notificationServiceMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        public CreateNotificationTests()
        {
            this._dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._notificationRepositoryMock = Substitute.For<INotificationRepository>();
            this._notificationServiceMock = Substitute.For<INotificationService>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new CreateNotificationCommandHandler(
                this._dateTimeProviderMock,
                this._userRepositoryMock,
                this._notificationRepositoryMock,
                this._notificationServiceMock,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_UserNotFound()
        {
            // Arrange
            this._userRepositoryMock.GetByIdAsync(Command.UserId, default).Returns((User?)null);

            // Act
            Result<NotificationId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_NotificationCreationFails()
        {
            // Arrange
            User user = UserData.Create();
            this._userRepositoryMock.GetByIdAsync(Command.UserId, default).Returns(user);

            // Act
            var invalidCommand = new CreateNotificationCommand(
                Command.UserId,
                NotificationType.Message,
                new Title(string.Empty),
                Command.Message
            );
            Result<NotificationId> result = await this._handler.Handle(invalidCommand, default);

            // Assert
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_SuccessfullyCreateNotification()
        {
            // Arrange
            User user = UserData.Create();
            this._userRepositoryMock.GetByIdAsync(Command.UserId, default).Returns(user);
            this._dateTimeProviderMock.UtcNow.Returns(DateTime.UtcNow);

            // Act
            Result<NotificationId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
