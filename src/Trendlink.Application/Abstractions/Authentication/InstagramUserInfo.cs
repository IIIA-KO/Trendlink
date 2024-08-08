using System.Text.Json.Serialization;

namespace Trendlink.Application.Abstractions.Authentication
{
    public sealed class InstagramUserInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}
