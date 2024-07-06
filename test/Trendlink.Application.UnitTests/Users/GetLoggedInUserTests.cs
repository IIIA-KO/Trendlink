using System.Data;
using FluentAssertions;
using NSubstitute;
using NSubstitute.DbConnection;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Users.GetLoggedInUser;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.UnitTests.Users
{
    public class GetLoggedInUserTests
    {
        public static readonly GetLoggedInUserQuery Query = new();

        private readonly ISqlConnectionFactory _sqlConnectionFactoryMock;
        private readonly IUserContext _userContextMock;

        private readonly GetLoggedInUserQueryHandler _handler;

        public GetLoggedInUserTests()
        {
            this._sqlConnectionFactoryMock = Substitute.For<ISqlConnectionFactory>();
            this._userContextMock = Substitute.For<IUserContext>();

            this._handler = new GetLoggedInUserQueryHandler(
                this._sqlConnectionFactoryMock,
                this._userContextMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            var expectedUser = new UserResponse
            {
                Id = Guid.NewGuid(),
                FirstName = UserData.FirstName.Value,
                LastName = UserData.LastName.Value,
                Email = UserData.Email.Value,
            };

            using IDbConnection dbConnectionMock = Substitute.For<IDbConnection>().SetupCommands();

            const string sql = """
                SELECT
                    id AS Id,
                    first_name AS FirstName,
                    last_name AS LastName,
                    email AS Email
                FROM users
                WHERE identity_id = @IdentityId
                """;

            dbConnectionMock.SetupQuery(sql).Returns(expectedUser);

            this._sqlConnectionFactoryMock.CreateConnection().Returns(dbConnectionMock);

            // Act
            Result<UserResponse> result = await this._handler.Handle(Query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenConnectionThrows()
        {
            // Arrange
            using IDbConnection dbConnectionMock = Substitute.For<IDbConnection>().SetupCommands();

            const string sql = """
                SELECT
                    id AS Id,
                    first_name AS FirstName,
                    last_name AS LastName,
                    email AS Email
                FROM users
                WHERE identity_id = @IdentityId
                """;

            dbConnectionMock.SetupQuery(sql).Throws(new Exception("Database exception"));

            this._sqlConnectionFactoryMock.CreateConnection().Returns(dbConnectionMock);

            // Act
            Result<UserResponse> result = await this._handler.Handle(Query, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Error.Unexpected);
        }
    }
}
