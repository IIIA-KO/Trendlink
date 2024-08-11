using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Trendlink.Api.FunctionalTests.Infrastructure;
using Trendlink.Application.Users;
using Trendlinl.Api.FunctionalTests.Infrastructure;

namespace Trendlink.Api.FunctionalTests.Users
{
    public class GetLoggedInUserTests : BaseFunctionalTest
    {
        public GetLoggedInUserTests(FunctionalTestWebAppFactory factory)
            : base(factory) { }

        [Fact]
        public async Task GetLoggedInUser_ShouldReturnUnauthorized_WhenAccessTokenIsMissing()
        {
            // Act
            HttpResponseMessage response = await this.HttpClient.GetAsync("api/users/me");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetLoggedInUser_ShouldReturnUser_WhenAccessTokenIsNotMissing()
        {
            // Arrange
            string accessToken = (await this.GetAccessToken()).AccessToken;

            this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme,
                accessToken
            );

            // Act
            UserResponse? user = await this.HttpClient.GetFromJsonAsync<UserResponse>(
                "api/users/me"
            );

            // Assert
            user.Should().NotBeNull();
        }
    }
}
