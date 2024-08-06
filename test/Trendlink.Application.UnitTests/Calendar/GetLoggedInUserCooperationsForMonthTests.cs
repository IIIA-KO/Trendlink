using System.Data;
using FluentAssertions;
using NSubstitute;
using NSubstitute.DbConnection;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Calendar;
using Trendlink.Application.Calendar.GetLoggedInUserCooperations;
using Trendlink.Application.Calendar.GetLoggedInUserCooperationsForMonth;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.UnitTests.Calendar
{
    public class GetLoggedInUserCooperationsForMonthTests
    {
        private const string SqlCooperations = """
            SELECT
                id AS Id,
                name AS Name,
                description AS Description,
                scheduled_on_utc AS ScheduledOnUtc,
                price_amount AS PriceAmount,
                price_currency AS PriceCurrency,
                advertisement_id AS AdvertisementId,
                buyer_id AS BuyerId,
                seller_id AS SellerId,
                status AS Status
            FROM cooperations
            WHERE (buyer_id = @UserId OR seller_id = @UserId)
                AND EXTRACT(MONTH FROM scheduled_on_utc) = @Month
                AND EXTRACT(YEAR FROM scheduled_on_utc) = @Year
            """;

        private const string SqlBlockedDates = """
            SELECT 
                date AS Date
            FROM blocked_dates
            WHERE user_id = @UserId
                AND EXTRACT(MONTH FROM date) = @Month
                AND EXTRACT(YEAR FROM date) = @Year
            """;

        private static readonly GetLoggedInUserCooperationsForMonthQuery Query =
            new(DateTime.Now.Month, DateTime.Now.Year);

        private readonly IUserContext _userContextMock;
        private readonly ISqlConnectionFactory _sqlConnectionFactoryMock;

        private readonly GetLoggedInUserCooperationsForMonthQueryHandler _handler;

        public GetLoggedInUserCooperationsForMonthTests()
        {
            this._userContextMock = Substitute.For<IUserContext>();
            this._sqlConnectionFactoryMock = Substitute.For<ISqlConnectionFactory>();

            this._handler = new GetLoggedInUserCooperationsForMonthQueryHandler(
                this._userContextMock,
                this._sqlConnectionFactoryMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenCooperationsQueryThrows()
        {
            // Arrange
            using IDbConnection dbConnection = Substitute.For<IDbConnection>().SetupCommands();

            dbConnection.SetupQuery(SqlCooperations).Throws(new Exception("Database exception"));

            this._sqlConnectionFactoryMock.CreateConnection().Returns(dbConnection);

            // Act
            Result<IReadOnlyList<DateResponse>> result = await this._handler.Handle(Query, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Error.Unexpected);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenBlockedDatesQueryThrows()
        {
            // Arrange
            List<CooperationResponse> expectedCooperations = [];

            using IDbConnection dbConnection = Substitute.For<IDbConnection>().SetupCommands();

            dbConnection.SetupQuery(SqlCooperations).Returns(expectedCooperations);

            dbConnection.SetupQuery(SqlBlockedDates).Throws(new Exception("Database exception"));

            this._sqlConnectionFactoryMock.CreateConnection().Returns(dbConnection);

            // Act
            Result<IReadOnlyList<DateResponse>> result = await this._handler.Handle(Query, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Error.Unexpected);
        }

        [Fact]
        public async Task Handle_Should_ReturnCooperations_ForGivenMonth()
        {
            // Arrange
            List<CooperationResponse> expectedCooperations = [];

            var expectedBlockedDates = new List<DateOnly>
            {
                DateOnly.FromDateTime(DateTime.UtcNow)
            };

            using IDbConnection dbConnection = Substitute.For<IDbConnection>().SetupCommands();

            dbConnection.SetupQuery(SqlCooperations).Returns(expectedCooperations);
            dbConnection.SetupQuery(SqlBlockedDates).Returns(expectedBlockedDates);

            this._sqlConnectionFactoryMock.CreateConnection().Returns(dbConnection);

            this._userContextMock.UserId.Returns(UserId.New());

            // Act
            Result<IReadOnlyList<DateResponse>> result = await this._handler.Handle(Query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
