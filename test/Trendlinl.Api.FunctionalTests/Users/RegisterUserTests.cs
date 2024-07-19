using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Trendlink.Api.Controllers.Users;
using Trendlink.Api.FunctionalTests.Infrastructure;
using Trendlink.Application.Countries.GetAllCountries;
using Trendlink.Application.Countries.GetStates;
using Trendlink.Domain.Users.States;
using Trendlinl.Api.FunctionalTests.Infrastructure;

namespace Trendlink.Api.FunctionalTests.Users
{
    public class RegisterUserTests : BaseFunctionalTest
    {
        private const string testStateId = "00000000-0000-0000-0000-000000000000";

        public RegisterUserTests(FunctionalTestWebAppFactory factory)
            : base(factory) { }

        [Theory]
        [InlineData(
            "",
            "last",
            "2000-01-01",
            "test@test.com",
            "0123456789",
            "Pa$$w0rd",
            testStateId
        )]
        [InlineData(
            "first",
            "",
            "2000-01-01",
            "test@test.com",
            "0123456789",
            "Pa$$w0rd",
            testStateId
        )]
        [InlineData(
            "first",
            "last",
            "2020-01-01",
            "test@test.com",
            "0123456789",
            "Pa$$w0rd",
            testStateId
        )]
        [InlineData(
            "first",
            "last",
            "2000-01-01",
            "testtest.com",
            "0123456789",
            "Pa$$w0rd",
            testStateId
        )]
        [InlineData("first", "last", "2000-01-01", "test@test.com", "0123456789", "", testStateId)]
        [InlineData("first", "last", "2000-01-01", "test@test.com", "123", "Pa$$w0rd", testStateId)]
        public async Task RegisterUser_Should_ReturnBadRequest_WhenRequestIsInvalid(
            string firstName,
            string lastName,
            string birthDate,
            string email,
            string phoneNumber,
            string password,
            string stateId
        )
        {
            // Arrange
            var request = new RegisterUserRequest(
                firstName,
                lastName,
                DateOnly.Parse(birthDate, CultureInfo.InvariantCulture),
                email,
                phoneNumber,
                password,
                Guid.Parse(stateId)
            );

            // Act
            HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync(
                "api/users/register",
                request
            );

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
