using System.Text.Json.Serialization;

namespace Trendlink.Application.Abstractions.Authentication.Models
{
    public sealed class InstagramUserInfo
    {
        [JsonPropertyName("business_discovery")]
        public BusinessDiscovery BusinessDiscovery { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class BusinessDiscovery
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("biography")]
        public string Biography { get; set; }

        [JsonPropertyName("ig_id")]
        public long IgId { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("profile_picture_url")]
        public string ProfilePictureUrl { get; set; }

        [JsonPropertyName("followers_count")]
        public int FollowersCount { get; set; }

        [JsonPropertyName("media_count")]
        public int MediaCount { get; set; }
    }
}
