using System.Text.Json.Serialization;

namespace Trendlink.Infrastructure.Authentication.Keycloak
{
    internal record FederatedIdentity
    {
        [JsonPropertyName("identityProvider")]
        public string IdentityProvider { get; init; }

        [JsonPropertyName("userId")]
        public string UserId { get; init; }

        [JsonPropertyName("userName")]
        public string UserName { get; init; }
    }
}
