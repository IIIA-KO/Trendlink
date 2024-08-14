using System.Text.Json.Serialization;

namespace Trendlink.Application.Abstractions.Authentication.Models
{
    public sealed class GoogleUserInfo
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("verified_email")]
        public bool VerifiedEmail { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("given_name")]
        public string GivenName { get; set; }

        [JsonPropertyName("picture")]
        public string Picture { get; set; }
    }
}
