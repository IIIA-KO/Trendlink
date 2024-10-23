using System.Net;
using FluentAssertions;
using Trendlink.Api.FunctionalTests.Infrastructure;

namespace Trendlink.Api.FunctionalTests.Countries
{
    public class GetAllCountriesTests : BaseFunctionalTest
    {
        public GetAllCountriesTests(FunctionalTestWebAppFactory factory)
            : base(factory) { }

        [Fact]
        public async Task GetAllCountries_Should_ReturnOk()
        {
            // Act
            HttpResponseMessage response = await this.HttpClient.GetAsync("api/countries");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
