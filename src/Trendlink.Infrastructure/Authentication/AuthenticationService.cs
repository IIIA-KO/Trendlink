using System.Net.Http.Json;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Domain.Users;
using Trendlink.Infrastructure.Authentication.Models;

namespace Trendlink.Infrastructure.Authentication
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private const string PasswordCredentialType = "password";

        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<string> RegisterAsync(
            User user,
            string password,
            CancellationToken cancellationToken = default
        )
        {
            var userRepresentationModel = UserRepresentationModel.FromUser(user);

            userRepresentationModel.Credentials =
            [
                new()
                {
                    Value = password,
                    Temporary = false,
                    Type = PasswordCredentialType
                }
            ];

            HttpResponseMessage response = await this._httpClient.PostAsJsonAsync(
                "users",
                userRepresentationModel,
                cancellationToken
            );
            return ExtractIdentityIdFromLocationHeader(response);
        }

        private static string ExtractIdentityIdFromLocationHeader(
            HttpResponseMessage httpResponseMessage
        )
        {
            const string usersSegmentName = "users/";

            string locationHeader =
                httpResponseMessage.Headers.Location?.PathAndQuery
                ?? throw new InvalidOperationException("Location header can't be null.");

            int userSegmentValueIndex = locationHeader.IndexOf(
                usersSegmentName,
                StringComparison.InvariantCultureIgnoreCase
            );

            return locationHeader.Substring(userSegmentValueIndex + usersSegmentName.Length);
        }
    }
}
