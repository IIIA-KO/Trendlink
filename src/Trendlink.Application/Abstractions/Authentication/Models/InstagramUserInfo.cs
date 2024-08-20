﻿using System.Text.Json.Serialization;

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
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("biography")]
        public string Biography { get; set; } = string.Empty;

        [JsonPropertyName("ig_id")]
        public long IgId { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("profile_picture_url")]
        public string ProfilePictureUrl { get; set; } = string.Empty;

        [JsonPropertyName("followers_count")]
        public int FollowersCount { get; set; }

        [JsonPropertyName("media_count")]
        public int MediaCount { get; set; }
    }
}
