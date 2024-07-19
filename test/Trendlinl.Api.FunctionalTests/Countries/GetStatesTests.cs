using System.Net;
using FluentAssertions;
using Trendlink.Api.FunctionalTests.Infrastructure;
using Trendlinl.Api.FunctionalTests.Infrastructure;

namespace Trendlink.Api.FunctionalTests.Countries
{
    public class GetStatesTests : BaseFunctionalTest
    {
        private static readonly Guid CountryId = Guid.NewGuid();

        public GetStatesTests(FunctionalTestWebAppFactory factory)
            : base(factory) { }

        [Fact]
        public async Task GetStates_Should_ReturnBadRequest_WhenCountryNotFound()
        {
            // Act
            HttpResponseMessage response = await this.HttpClient.GetAsync(
                $"api/countries/{CountryId}/states"
            );

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
