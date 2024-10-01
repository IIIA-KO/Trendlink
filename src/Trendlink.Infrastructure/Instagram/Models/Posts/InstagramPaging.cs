using System.Text.Json.Serialization;

namespace Trendlink.Infrastructure.Instagram.Models.Posts
{
    public class InstagramPaging
    {
        [JsonPropertyName("cursors")]
        public InstagramCursors Cursors { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("previous")]
        public string Previous { get; set; }
    }
}
