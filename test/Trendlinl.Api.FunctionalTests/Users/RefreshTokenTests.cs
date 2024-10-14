using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Trendlink.Api.FunctionalTests.Infrastructure;
using Trendlink.Application.Accounts.LogIn;

namespace Trendlink.Api.FunctionalTests.Users
{
    public class RefreshTokenTests : BaseFunctionalTest
    {
        public RefreshTokenTests(FunctionalTestWebAppFactory factory)
            : base(factory) { }

        [Fact]
        public async Task RefreshToken_Should_ReturnBadRequest_WhenTokenIsMissing()
        {
            // Act
            HttpResponseMessage response =
                await this.HttpClient.PostAsJsonAsync<AccessTokenResponse>(
                    "api/users/refresh",
                    null!
                );

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task RefreshToken_Should_ReturnNewToken_WhenTokenIsValid()
        {
            // Arrange
            AccessTokenResponse accessToken = await this.GetAccessToken();

            this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme,
                accessToken.AccessToken
            );

            // Act
            HttpResponseMessage response =
                await this.HttpClient.PostAsJsonAsync<AccessTokenResponse>(
                    "api/users/refresh",
                    accessToken
                );

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
