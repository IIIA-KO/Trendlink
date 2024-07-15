using System.Data;
using Dapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.DbConnection;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Notifications;
using Trendlink.Application.Notifications.GetUserNotifications;
using Trendlink.Application.UnitTests.Users;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.UnitTests.Notifications
{
    public class GetUserNotificationTests
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
            """;

        private static readonly GetUserNotificationsQuery Query = new(UserId.New());

        private readonly ISqlConnectionFactory _sqlConnectionFactoryMock;
        private readonly IUserRepository _userRepositoryMock;

        private readonly GetUserNotificationsQueryHandler _handler;

        public GetUserNotificationTests()
        {
            this._sqlConnectionFactoryMock = Substitute.For<ISqlConnectionFactory>();
            this._userRepositoryMock = Substitute.For<IUserRepository>();

            this._handler = new GetUserNotificationsQueryHandler(
                this._sqlConnectionFactoryMock,
                this._userRepositoryMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_UserNotFound()
        {
            // Arrange
            this._userRepositoryMock.GetByIdWithRolesAsync(Query.UserId, default)
                .Returns((User?)null);

            // Act
            Result<IReadOnlyList<NotificationResponse>> result = await this._handler.Handle(
                Query,
                default
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_UserNotAdmin()
        {
            // Arrange
            User user = UserData.Create();
            this._userRepositoryMock.GetByIdWithRolesAsync(Query.UserId, default).Returns(user);

            // Act
            Result<IReadOnlyList<NotificationResponse>> result = await this._handler.Handle(
                Query,
                default
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotAuthorized);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_QueryThrowsException()
        {
            // Arrange
            User user = UserData.Create();
            user.AddRole(Role.Administrator);

            this._userRepositoryMock.GetByIdWithRolesAsync(Query.UserId, default).Returns(user);

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
            user.AddRole(Role.Administrator);

            this._userRepositoryMock.GetByIdWithRolesAsync(Query.UserId, default).Returns(user);

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
