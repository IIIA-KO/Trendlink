using System.Text.Json.Serialization;

namespace Trendlink.Infrastructure.Instagram.Models.Posts
{
    public class InstagramCursors
    {
        [JsonPropertyName("before")]
        public string Before { get; set; }

        [JsonPropertyName("after")]
        public string After { get; set; }
    }
}
