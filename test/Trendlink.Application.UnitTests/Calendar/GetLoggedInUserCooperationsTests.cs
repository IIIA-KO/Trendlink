﻿using System.Data;
using FluentAssertions;
using NSubstitute;
using NSubstitute.DbConnection;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Calendar;
using Trendlink.Application.Calendar.GetLoggedInUserCooperations;
using Trendlink.Application.UnitTests.Users;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.UnitTests.Calendar
{
    public class GetLoggedInUserCooperationsTests
    {
        private const string SqlCooperations = """
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
            WHERE buyer_id = @UserId 
                OR seller_id = @UserId
            """;

        private const string SqlBlockedDates = """
            SELECT 
                date AS Date
            FROM blocked_dates
            WHERE user_id = @UserId
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

            this._userContextMock.UserId.Returns(UserData.Create().Id);

            // Act
            Result<IReadOnlyList<DateResponse>> result = await this._handler.Handle(Query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
