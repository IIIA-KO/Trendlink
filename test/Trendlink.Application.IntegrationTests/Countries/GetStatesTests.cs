using FluentAssertions;
using Trendlink.Application.Countries.GetStates;
using Trendlink.Application.IntegrationTests.Infrastructure;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.Countries;

namespace Trendlink.Application.IntegrationTests.Countries
{
    public class GetStatesTests : BaseIntegrationTest
    {
        private static readonly Guid CountryId = Guid.NewGuid();

        public GetStatesTests(IntegrationTestWebAppFactory factory) 
            : base(factory) { }

        [Fact]
        public async Task GetStates_Should_ReturnFailure_WhenCountryIsNotFound()
        {
            // Arrange
            var query = new GetStatesQuery(CountryId);

            // Act
            Result<IReadOnlyCollection<StateResponse>> result = await this.Sender.Send(query, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CountryErrors.NotFound);
        }
    }
}
