using FluentAssertions;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.UnitTests.Infrastructure;

namespace Trendlink.Domain.UnitTests.Notifications
{
    public class NotificationTests : BaseTest
    {
        [Fact]
        public void Create_Should_SetPropertyValues()
        {
            Notification notification = NotificationBuilder
                .ForUser(NotificationData.UserId)
                .WithType(NotificationData.NotificationType)
                .WithTitle(NotificationData.Title.Value)
                .WithMessage(NotificationData.Message.Value)
                .CreatedOn(NotificationData.CreatedOnUtc)
                .Build();

            // Assert
            notification.NotificationType.Should().Be(NotificationData.NotificationType);
            notification.Title.Should().Be(NotificationData.Title);
            notification.Message.Should().Be(NotificationData.Message);
            notification.IsRead.Should().BeFalse();
            notification.CreatedOnUtc.Should().Be(NotificationData.CreatedOnUtc);
        }

        [Fact]
        public void MarkAsRead_Should_SetIsReadToTrue()
        {
            // Arrange
            Notification notification = NotificationBuilder
                .ForUser(NotificationData.UserId)
                .WithType(NotificationData.NotificationType)
                .WithTitle(NotificationData.Title.Value)
                .WithMessage(NotificationData.Message.Value)
                .CreatedOn(NotificationData.CreatedOnUtc)
                .Build();

            // Act
            notification.MarkAsRead();

            // Assert
            notification.IsRead.Should().BeTrue();
        }

        [Fact]
        public void MarkAsRead_ShouldNot_AlreadyMarkAlreadyRead()
        {
            // Arrange
            Notification notification = NotificationBuilder
                .ForUser(NotificationData.UserId)
                .WithType(NotificationData.NotificationType)
                .WithTitle(NotificationData.Title.Value)
                .WithMessage(NotificationData.Message.Value)
                .CreatedOn(NotificationData.CreatedOnUtc)
                .Build();

            // Act
            notification.MarkAsRead();
            notification.MarkAsRead();

            // Assert
            notification.IsRead.Should().BeTrue();
        }
    }
}
