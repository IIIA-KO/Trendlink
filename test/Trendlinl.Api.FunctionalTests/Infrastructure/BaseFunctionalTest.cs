using System.Net.Http.Json;
using Trendlink.Api.Controllers.Users;
using Trendlink.Api.FunctionalTests.Users;
using Trendlink.Application.Accounts.LogInUser;

namespace Trendlink.Api.FunctionalTests.Infrastructure
{
    public abstract class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
    {
        protected readonly HttpClient HttpClient;

        protected BaseFunctionalTest(FunctionalTestWebAppFactory factory)
        {
            this.HttpClient = factory.CreateClient();
        }

        protected async Task<AccessTokenResponse> GetAccessToken()
        {
            HttpResponseMessage loginResponse = await this.HttpClient.PostAsJsonAsync(
                "api/users/login",
                new LogInUserRequest(
                    UserData.RegisterTestUserRequest.Email,
                    UserData.RegisterTestUserRequest.Password
                )
            );

            AccessTokenResponse? accessTokenResponse =
                await loginResponse.Content.ReadFromJsonAsync<AccessTokenResponse>();

            return accessTokenResponse!;
        }
    }
}
