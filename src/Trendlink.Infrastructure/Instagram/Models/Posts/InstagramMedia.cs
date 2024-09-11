using Newtonsoft.Json;

namespace Trendlink.Infrastructure.Instagram.Models.Posts
{
    internal sealed class InstagramMedia
    {
        [JsonProperty("data")]
        public List<InstagramPostResponse> Data { get; init; }

        [JsonProperty("paging")]
        public InstagramPaging Paging { get; init; }
    }
}
