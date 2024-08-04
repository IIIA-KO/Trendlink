namespace Trendlink.Infrastructure.Authentication.Keycloak
{
    public sealed class KeycloakOptions
    {
        public string BaseUrl { get; set; } = string.Empty;

        public string Realm { get; set; } = string.Empty;

        public string Authority { get; set; } = string.Empty;

        public string AdminUrl { get; set; } = string.Empty;

        public string TokenUrl { get; set; } = string.Empty;

        public string RedirectUri { get; set; } = string.Empty;

        public string AdminClientId { get; init; } = string.Empty;

        public string AdminClientSecret { get; init; } = string.Empty;

        public string AuthClientId { get; init; } = string.Empty;

        public string AuthClientSecret { get; init; } = string.Empty;
    }
}
