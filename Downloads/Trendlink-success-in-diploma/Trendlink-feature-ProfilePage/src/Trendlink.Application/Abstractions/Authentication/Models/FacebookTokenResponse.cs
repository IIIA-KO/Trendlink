using System.Text.Json.Serialization;

namespace Trendlink.Application.Abstractions.Authentication.Models
{
    public class FacebookTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        public DateTimeOffset ExpiresAtUtc { get; set; }
    }
}
