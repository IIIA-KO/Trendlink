using FluentAssertions;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users;

namespace Trendlink.Domain.UnitTests.Notifications
{
    public class NotificationBuilderTests
    {
        [Fact]
        public void ForUser_Should_SetUserId()
        {
            // Act
            IExpectsType builder = NotificationBuilder.ForUser(NotificationData.UserId);

            // Assert
            builder.Should().BeOfType<NotificationBuilder>();
            var notificationBuilder = builder as NotificationBuilder;
            notificationBuilder!.UserId.Should().Be(NotificationData.UserId);
        }

        [Fact]
        public void WithType_Should_SetNotificationType()
        {
            // Arrange
            IExpectsType builder = NotificationBuilder.ForUser(UserId.New());

            // Act
            IExpectsTitle result = builder.WithType(NotificationData.NotificationType);

            // Assert
            result.Should().BeOfType<NotificationBuilder>();
            var notificationBuilder = result as NotificationBuilder;
            notificationBuilder!.Type.Should().Be(NotificationData.NotificationType);
        }

        [Fact]
        public void WithTitle_Should_SetTitleWhenValid()
        {
            // Arrange
            IExpectsTitle builder = NotificationBuilder
                .ForUser(UserId.New())
                .WithType(NotificationData.NotificationType);

            // Act
            IExpectsMessage result = builder.WithTitle(NotificationData.Title.Value);

            // Assert
            result.Should().BeOfType<NotificationBuilder>();
            var notificationBuilder = result as NotificationBuilder;
            notificationBuilder!.Title.Should().Be(NotificationData.Title);
        }

        [Fact]
        public void WithTitle_Should_ThrowArgumentException_WhenTitleIsInvalid()
        {
            // Arrange
            IExpectsTitle builder = NotificationBuilder
                .ForUser(UserId.New())
                .WithType(NotificationData.NotificationType);

            // Act
            Action act = () => builder.WithTitle(string.Empty);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Title is required.*");
        }

        [Fact]
        public void WithMessage_Should_SetMessage_WhenValid()
        {
            // Arrange
            IExpectsMessage builder = NotificationBuilder
                .ForUser(UserId.New())
                .WithType(NotificationData.NotificationType)
                .WithTitle(NotificationData.Title.Value);

            // Act
            IExpectsCreationDate result = builder.WithMessage(NotificationData.Message.Value);

            // Assert
            result.Should().BeOfType<NotificationBuilder>();
            var notificationBuilder = result as NotificationBuilder;
            notificationBuilder!.Message.Should().Be(NotificationData.Message);
        }

        [Fact]
        public void WithMessage_Should_ThrowArgumentException_WhenMessageIsInvalid()
        {
            // Arrange
            IExpectsMessage builder = NotificationBuilder
                .ForUser(UserId.New())
                .WithType(NotificationData.NotificationType)
                .WithTitle(NotificationData.Title.Value);

            // Act
            Action act = () => builder.WithMessage(string.Empty);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Message is required.*");
        }

        [Fact]
        public void CreatedOn_Should_SetCreatedOnUtc()
        {
            // Arrange
            IExpectsCreationDate builder = NotificationBuilder
                .ForUser(UserId.New())
                .WithType(NotificationData.NotificationType)
                .WithTitle(NotificationData.Title.Value)
                .WithMessage(NotificationData.Message.Value);

            // Act
            INotificationBuilder result = builder.CreatedOn(NotificationData.CreatedOnUtc);

            // Assert
            result.Should().BeOfType<NotificationBuilder>();
            var notificationBuilder = result as NotificationBuilder;
            notificationBuilder!.CreatedOnUtc.Should().Be(NotificationData.CreatedOnUtc);
        }

        [Fact]
        public void Build_Should_CreateNotification()
        {
            // Arrange
            INotificationBuilder builder = NotificationBuilder
                .ForUser(NotificationData.UserId)
                .WithType(NotificationData.NotificationType)
                .WithTitle(NotificationData.Title.Value)
                .WithMessage(NotificationData.Message.Value)
                .CreatedOn(NotificationData.CreatedOnUtc);

            // Act
            Notification notification = builder.Build();

            // Assert
            notification.UserId.Should().Be(NotificationData.UserId);
            notification.NotificationType.Should().Be(NotificationData.NotificationType);
            notification.Title.Should().Be(NotificationData.Title);
            notification.Message.Should().Be(NotificationData.Message);
            notification.CreatedOnUtc.Should().Be(NotificationData.CreatedOnUtc);
        }
    }
}
