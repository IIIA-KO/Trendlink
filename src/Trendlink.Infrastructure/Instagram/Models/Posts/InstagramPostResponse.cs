using Newtonsoft.Json;

namespace Trendlink.Infrastructure.Instagram.Models.Posts
{
    public sealed class InstagramPostResponse
    {
        [JsonProperty("id")]
        public string Id { get; init; }

        [JsonProperty("media_type")]
        public string MediaType { get; init; }

        [JsonProperty("media_url")]
        public string MediaUrl { get; init; }

        [JsonProperty("permalink")]
        public string Permalink { get; init; }

        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl { get; init; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; init; }

        public InstagramInsightsResponse? Insights { get; internal set; }
    }

    public sealed class InstagramInsightsResponse
    {
        [JsonProperty("data")]
        public List<InstagramInsightResponse> Data { get; init; }
    }

    public sealed class InstagramInsightResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("values")]
        public List<InsightValueResponse> Values { get; set; }
    }

    public class InsightValueResponse
    {
        [JsonProperty("value")]
        public int? Value { get; set; }
    }
}
