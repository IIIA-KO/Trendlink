using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Trendlink.Api.Controllers.Users;
using Trendlink.Api.FunctionalTests.Infrastructure;
using Trendlinl.Api.FunctionalTests.Infrastructure;

namespace Trendlink.Api.FunctionalTests.Users
{
    public class LogInUserTests : BaseFunctionalTest
    {
        private const string Email = "login@test.com";
        private const string Password = "Pa$$w0rd";

        public LogInUserTests(FunctionalTestWebAppFactory factory)
            : base(factory) { }

        [Fact]
        public async Task Login_Should_ReturnUnauthorized_WhenUserDoesNotExist()
        {
            // Arrange
            var request = new LogInUserRequest(Email, Password);

            // Act
            HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync(
                "api/users/login",
                request
            );

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
