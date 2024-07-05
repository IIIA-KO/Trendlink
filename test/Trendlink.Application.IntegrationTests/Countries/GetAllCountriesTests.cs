using FluentAssertions;
using Trendlink.Application.Countries.GetAllCountries;
using Trendlink.Application.IntegrationTests.Infrastructure;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.IntegrationTests.Countries
{
    public class GetAllCountriesTests : BaseIntegrationTest
    {
        public GetAllCountriesTests(IntegrationTestWebAppFactory factory) 
            : base(factory) { }

        [Fact]
        public async Task GetAllCountries_Should_ReturnListOfCountriesResponse()
        {
            // Arrange
            var query = new GetAllCountriesQuery();

            // Act
            Result<IReadOnlyCollection<CountryResponse>> result = await this.Sender.Send(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
