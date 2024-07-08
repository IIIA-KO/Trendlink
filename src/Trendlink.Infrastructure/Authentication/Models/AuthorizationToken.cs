using System.Text.Json.Serialization;

namespace Trendlink.Infrastructure.Authentication.Models
{
    public sealed class AuthorizationToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; } = string.Empty;

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; init; } = string.Empty;
    }
}
