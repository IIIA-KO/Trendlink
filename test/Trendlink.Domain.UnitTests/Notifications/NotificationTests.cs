using FluentAssertions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Notifications.ValueObjects;
using Trendlink.Domain.UnitTests.Infrastructure;

namespace Trendlink.Domain.UnitTests.Notifications
{
    public class NotificationTests : BaseTest
    {
        [Fact]
        public void Create_Should_SetPropertyValues()
        {
            // Act
            Notification notification = Notification
                .Create(
                    NotificationData.UserId,
                    NotificationData.NotificationType,
                    NotificationData.Title,
                    NotificationData.Message,
                    NotificationData.CreatedOnUtc
                )
                .Value;

            // Assert
            notification.NotificationType.Should().Be(NotificationData.NotificationType);
            notification.Title.Should().Be(NotificationData.Title);
            notification.Message.Should().Be(NotificationData.Message);
            notification.IsRead.Should().BeFalse();
            notification.CreatedOnUtc.Should().Be(NotificationData.CreatedOnUtc);
        }

        [Fact]
        public void Create_Should_ReturnFailure_WhenTitleIsNull()
        {
            // Act
            Result<Notification> result = Notification.Create(
                NotificationData.UserId,
                NotificationData.NotificationType,
                new Title(null!),
                NotificationData.Message,
                NotificationData.CreatedOnUtc
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(NotificationErrors.Invalid);
        }

        [Fact]
        public void Create_Should_ReturnFailure_WhenMessageIsNull()
        {
            // Act
            Result<Notification> result = Notification.Create(
                NotificationData.UserId,
                NotificationData.NotificationType,
                NotificationData.Title,
                new Message(null!),
                NotificationData.CreatedOnUtc
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(NotificationErrors.Invalid);
        }

        [Fact]
        public void MarkAsRead_Should_SetIsReadToTrue()
        {
            // Arrange
            Notification notification = Notification
                .Create(
                    NotificationData.UserId,
                    NotificationData.NotificationType,
                    NotificationData.Title,
                    NotificationData.Message,
                    NotificationData.CreatedOnUtc
                )
                .Value;

            // Act
            notification.MarkAsRead();

            // Assert
            notification.IsRead.Should().BeTrue();
        }

        [Fact]
        public void MarkAsRead_ShouldNot_AlreadyMarkAlreadyRead()
        {
            // Arrange
            Notification notification = Notification
                .Create(
                    NotificationData.UserId,
                    NotificationData.NotificationType,
                    NotificationData.Title,
                    NotificationData.Message,
                    NotificationData.CreatedOnUtc
                )
                .Value;

            // Act
            notification.MarkAsRead();
            notification.MarkAsRead();

            // Assert
            notification.IsRead.Should().BeTrue();
        }
    }
}
