using System.Data;
using FluentAssertions;
using NSubstitute;
using NSubstitute.DbConnection;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Calendar;
using Trendlink.Application.Calendar.GetUserCalendar;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Calendar
{
    public class GetUserCalendarTests
    {
        private const string SqlCooperations = """
            SELECT
                scheduled_on_utc AS ScheduledOnUtc
            FROM cooperations
            WHERE buyer_id = @UserId
                OR seller_id = @UserId
            """;

        private const string SqlBlockedDates = """
            SELECT
                date AS Date
            FROM blocked_dates
            WHERE user_id = @UserId
            """;

        private static readonly GetUserCalendarQuery Query = new(UserId.New());

        private readonly GetUserCalendarQueryHandler _handler;

        private readonly ISqlConnectionFactory _sqlConnectionFactoryMock;

        public GetUserCalendarTests()
        {
            this._sqlConnectionFactoryMock = Substitute.For<ISqlConnectionFactory>();

            this._handler = new GetUserCalendarQueryHandler(this._sqlConnectionFactoryMock);
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
        public async Task Handle_Should_Return_Success()
        {
            // Arrange
            List<CooperationResponse> expectedCooperations = [];
            List<DateOnly> expectedBlockedDates = [];

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
