using System.Data;
using FluentAssertions;
using NSubstitute;
using NSubstitute.DbConnection;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Notifications;
using Trendlink.Application.Notifications.GetLoggedInUserNotifications;
using Trendlink.Application.UnitTests.Users;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Notifications
{
    public class GetLoggedInUserNotificationsTests
    {
        private const string Sql = """
            SELECT
                id AS Id,
                user_id AS UserId,
                notification_type AS NotificationType,
                title AS Title,
                message AS Message,
                is_read AS IsRead,
                created_on_utc AS CreatedOnUtc
            FROM notifications
            WHERE user_id = @UserId
            ORDER BY created_on_utc DESC
            """;

        private static readonly GetLoggedInUserNotificationsQuery Query = new();

        private readonly ISqlConnectionFactory _sqlConnectionFactoryMock;
        private readonly IUserContext _userContextMock;

        private readonly GetLoggedInUserNotificationsQueryHandler _handler;

        public GetLoggedInUserNotificationsTests()
        {
            this._sqlConnectionFactoryMock = Substitute.For<ISqlConnectionFactory>();
            this._userContextMock = Substitute.For<IUserContext>();

            this._handler = new GetLoggedInUserNotificationsQueryHandler(
                this._sqlConnectionFactoryMock,
                this._userContextMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_QueryThrowsException()
        {
            // Arrange
            using IDbConnection dbConnectionMock = Substitute.For<IDbConnection>().SetupCommands();

            dbConnectionMock.SetupQuery(Sql).Throws(new Exception("Database exception"));

            this._sqlConnectionFactoryMock.CreateConnection().Returns(dbConnectionMock);

            // Act
            Result<IReadOnlyList<NotificationResponse>> result = await this._handler.Handle(
                Query,
                default
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Error.Unexpected);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            User user = UserData.Create();

            var notifications = new List<NotificationResponse>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id.Value,
                    NotificationType = NotificationData.NotificationType,
                    Title = NotificationData.Title.Value,
                    Message = NotificationData.Message.Value,
                    IsRead = false,
                    CreatedOnUtc = DateTime.UtcNow
                }
            };

            this._userContextMock.UserId.Returns(user.Id);

            using IDbConnection dbConnectionMock = Substitute.For<IDbConnection>().SetupCommands();

            dbConnectionMock.SetupQuery(Sql).Returns(notifications);

            this._sqlConnectionFactoryMock.CreateConnection().Returns(dbConnectionMock);

            // Act
            Result<IReadOnlyList<NotificationResponse>> result = await this._handler.Handle(
                Query,
                default
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
