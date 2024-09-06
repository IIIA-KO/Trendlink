using System.Data;
using Dapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.DbConnection;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Conditions.GetUserCondition;
using Trendlink.Application.UnitTests.Users;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Conditions
{
    public class GetUserConditionTests
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

        private static readonly GetUserConditionQuery Query = new(UserData.Create().Id);

        private readonly ISqlConnectionFactory _sqlConnectionFactoryMock;

        private readonly GetLUserConditionQueryHandler _handler;

        public GetUserConditionTests()
        {
            this._sqlConnectionFactoryMock = Substitute.For<ISqlConnectionFactory>();

            this._handler = new GetLUserConditionQueryHandler(this._sqlConnectionFactoryMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
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
