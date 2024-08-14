using System.Text.Json.Serialization;

namespace Trendlink.Application.Abstractions.Authentication.Models
{
    public class InstagramTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
    }
}
