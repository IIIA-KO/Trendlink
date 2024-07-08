using System.Data;
using FluentAssertions;
using NSubstitute;
using NSubstitute.DbConnection;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Countries.GetStates;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.Countries;

namespace Trendlink.Application.UnitTests.Countries
{
    public class GetStatesTests
    {
        private const string Sql = """
            SELECT
                id AS Id,
                name AS Name
            FROM states
            WHERE country_id = @CountryId
            """;

        private readonly List<StateResponse> ExpectedStates =
        [
            new(Query.CountryId, "TestState1"),
            new(Query.CountryId, "TestState2"),
            new(Query.CountryId, "TestState3"),
        ];

        public static readonly GetStatesQuery Query = new(Guid.NewGuid());

        private readonly ISqlConnectionFactory _sqlConnectionFactoryMock;
        private readonly ICountryRepository _countryRepositoryMock;

        private readonly GetStatesQueryHandler _handler;

        public GetStatesTests()
        {
            this._sqlConnectionFactoryMock = Substitute.For<ISqlConnectionFactory>();
            this._countryRepositoryMock = Substitute.For<ICountryRepository>();

            this._handler = new GetStatesQueryHandler(
                this._sqlConnectionFactoryMock,
                this._countryRepositoryMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenCountryIsNull()
        {
            // Arrange
            this._countryRepositoryMock.CountryExists(new CountryId(Query.CountryId))
                .Returns(false);

            using IDbConnection dbConnectionMock = Substitute.For<IDbConnection>().SetupCommands();

            dbConnectionMock.SetupQuery(Sql).Returns(this.ExpectedStates);

            this._sqlConnectionFactoryMock.CreateConnection().Returns(dbConnectionMock);

            // Act
            Result<IReadOnlyCollection<StateResponse>> result = await this._handler.Handle(
                Query,
                default
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CountryErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenConnectionThrows()
        {
            // Arrange
            this._countryRepositoryMock.CountryExists(new CountryId(Query.CountryId))
                .Returns(true);

            using IDbConnection dbConnectionMock = Substitute.For<IDbConnection>().SetupCommands();

            dbConnectionMock.SetupQuery(Sql).Throws(new Exception("Database exception"));

            this._sqlConnectionFactoryMock.CreateConnection().Returns(dbConnectionMock);

            // Act
            Result<IReadOnlyCollection<StateResponse>> result = await this._handler.Handle(
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
            this._countryRepositoryMock.CountryExists(new CountryId(Query.CountryId))
                .Returns(true);

            using IDbConnection dbConnectionMock = Substitute.For<IDbConnection>().SetupCommands();

            dbConnectionMock.SetupQuery(Sql).Returns(this.ExpectedStates);

            this._sqlConnectionFactoryMock.CreateConnection().Returns(dbConnectionMock);

            // Act
            Result<IReadOnlyCollection<StateResponse>> result = await this._handler.Handle(
                Query,
                default
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
