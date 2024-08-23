using FluentAssertions;
using NSubstitute;
using NSubstitute.DbConnection;
using System.Data;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Conditions.GetLoggedInUserCondition;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Conditions
{
    public class GetLoggedInUserConditionTests
    {
        private const string Sql = """
            SELECT 
                c.id AS Id,
                c.user_id AS UserId,
                c.description AS Description,
                a.Id AS SplitProperty,
                a.id AS Id,
                a.name AS Name,
                a.price_amount AS PriceAmount,
                a.price_currency AS PriceCurrency,
                a.description AS Description
            FROM 
                conditions c
            LEFT JOIN 
                advertisements a ON c.id = a.condition_id
            WHERE 
                c.user_id = @UserId;
            """;

        private static readonly GetLoggedInUserConditionQuery Query = new();

        private readonly ISqlConnectionFactory _sqlConnectionFactoryMock;
        private readonly IUserContext _userContextMock;

        private readonly GetLoggedInUserConditionQueryHandler _handler;

        public GetLoggedInUserConditionTests()
        {
            this._sqlConnectionFactoryMock = Substitute.For<ISqlConnectionFactory>();
            this._userContextMock = Substitute.For<IUserContext>();

            this._handler = new GetLoggedInUserConditionQueryHandler(
                this._sqlConnectionFactoryMock,
                this._userContextMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            UserId userId = ConditionData.UserId;

            this._userContextMock.UserId.Returns(userId);

            using IDbConnection dbConnectionMock = Substitute.For<IDbConnection>().SetupCommands();

            dbConnectionMock.SetupQuery(Sql).Throws(new Exception("Databse exception"));

            this._sqlConnectionFactoryMock.CreateConnection().Returns(dbConnectionMock);

            // Act
            Result<ConditionResponse> result = await this._handler.Handle(Query, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Error.Unexpected);
        }
    }
}
