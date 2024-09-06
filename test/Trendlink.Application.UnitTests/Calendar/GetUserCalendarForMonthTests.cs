using System.Data;
using FluentAssertions;
using NSubstitute;
using NSubstitute.DbConnection;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Calendar;
using Trendlink.Application.Calendar.GetUserCalendarForMonth;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Calendar
{
    public class GetUserCalendarForMonthTests
    {
        private const string SqlCooperations = """
            SELECT
                scheduled_on_utc AS ScheduledOnUtc
            FROM cooperations
            WHERE (seller_id = @UserId)
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

        private static readonly GetUserCalendarForMonthQuery Query =
            new(UserId.New(), DateTime.Now.Month, DateTime.Now.Year);

        private readonly GetUserCalendarForMonthQueryHandler _handler;

        private readonly ISqlConnectionFactory _sqlConnectionFactoryMock;

        public GetUserCalendarForMonthTests()
        {
            this._sqlConnectionFactoryMock = Substitute.For<ISqlConnectionFactory>();

            this._handler = new GetUserCalendarForMonthQueryHandler(this._sqlConnectionFactoryMock);
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

            // Act
            Result<IReadOnlyList<DateResponse>> result = await this._handler.Handle(Query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
