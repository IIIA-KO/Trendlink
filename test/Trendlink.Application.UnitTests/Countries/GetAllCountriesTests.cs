using System.Data;
using FluentAssertions;
using NSubstitute;
using NSubstitute.DbConnection;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Countries.GetAllCountries;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.UnitTests.Countries
{
    public class GetAllCountriesTests
    {
        private const string Sql = """
            SELECT
                id AS Id,
                name AS Name
            FROM countries
            """;

        private readonly List<CountryResponse> ExpectedCountries =
        [
            new(Guid.NewGuid(), "Country1"),
            new(Guid.NewGuid(), "Country2"),
            new(Guid.NewGuid(), "Country3")
        ];

        public static readonly GetAllCountriesQuery Query = new();

        private readonly ISqlConnectionFactory _sqlConnectionFactoryMock;

        private readonly GetAllCountriesQueryHandler _handler;

        public GetAllCountriesTests()
        {
            this._sqlConnectionFactoryMock = Substitute.For<ISqlConnectionFactory>();

            this._handler = new GetAllCountriesQueryHandler(this._sqlConnectionFactoryMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenConnectionThrows()
        {
            // Arrange
            using IDbConnection dbConnectionMock = Substitute.For<IDbConnection>().SetupCommands();

            dbConnectionMock.SetupQuery(Sql).Throws(new Exception("Database exception"));

            this._sqlConnectionFactoryMock.CreateConnection().Returns(dbConnectionMock);

            // Act
            Result<IReadOnlyCollection<CountryResponse>> result = await this._handler.Handle(
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
            using IDbConnection dbConnectionMock = Substitute.For<IDbConnection>().SetupCommands();

            dbConnectionMock.SetupQuery(Sql).Returns(this.ExpectedCountries);

            this._sqlConnectionFactoryMock.CreateConnection().Returns(dbConnectionMock);

            // Act
            Result<IReadOnlyCollection<CountryResponse>> result = await this._handler.Handle(
                Query,
                default
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
