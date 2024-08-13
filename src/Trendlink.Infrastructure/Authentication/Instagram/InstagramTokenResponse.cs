using System.Text.Json.Serialization;

namespace Trendlink.Infrastructure.Authentication.Instagram
{
    public class InstagramTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
    }
}
