using System.Data;
using FluentAssertions;
using NSubstitute;
using NSubstitute.DbConnection;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Conditions.GetLoggedInUserCondition;
using Trendlink.Application.Cooperations.GetLoggedInUserCooperations;
using Trendlink.Application.Countries.GetAllCountries;
using Trendlink.Application.UnitTests.Users;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.UnitTests.Cooperations
{
    public class GetLoggedInUserCooperationsTests
    {
        private const string Sql = """
            SELECT
                id AS Id,
                name AS Name,
                description AS Description,
                scheduled_on_utc AS ScheduledOnUtc,
                advertisement_id AS AdvertisementId,
                buyer_id AS BuyerId,
                seller_id AS SellerId,
                status AS Status
            FROM cooperations
            WHERE buyer_id = @UserId OR seller_id = @UserId
            """;

        public static readonly GetLoggedInUserCooperationsQuery Query = new();

        private readonly IUserContext _userContextMock;
        private readonly ISqlConnectionFactory _sqlConnectionFactoryMock;

        private readonly GetLoggedInUserCooperationsQueryHandler _handler;

        public GetLoggedInUserCooperationsTests()
        {
            this._userContextMock = Substitute.For<IUserContext>();
            this._sqlConnectionFactoryMock = Substitute.For<ISqlConnectionFactory>();

            this._handler = new GetLoggedInUserCooperationsQueryHandler(
                this._userContextMock,
                this._sqlConnectionFactoryMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenConnectionThrows()
        {
            // Arrange
            using IDbConnection dbConnection = Substitute.For<IDbConnection>().SetupCommands();

            dbConnection.SetupQuery(Sql).Throws(new Exception("Database exception"));

            this._sqlConnectionFactoryMock.CreateConnection().Returns(dbConnection);

            // Act
            Result<IReadOnlyList<CooperationResponse>> result = await this._handler.Handle(
                Query,
                default
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Error.Unexpected);
        }

        [Fact]
        public async Task Handle_Should_Return_Success()
        {
            // Arrange
            List<CooperationResponse> expectedCooperations = [];

            using IDbConnection dbConnection = Substitute.For<IDbConnection>().SetupCommands();

            dbConnection.SetupQuery(Sql).Returns(expectedCooperations);

            this._sqlConnectionFactoryMock.CreateConnection().Returns(dbConnection);

            this._userContextMock.UserId.Returns(UserData.Create().Id);

            // Act
            Result<IReadOnlyList<CooperationResponse>> result = await this._handler.Handle(
                Query,
                default
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
