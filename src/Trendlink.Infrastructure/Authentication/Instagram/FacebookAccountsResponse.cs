using System.Text.Json.Serialization;

namespace Trendlink.Infrastructure.Authentication.Instagram
{
    internal sealed class FacebookAccountsResponse
    {
        [JsonPropertyName("data")]
        public FacebookPage[] Data { get; set; }
    }

    public class FacebookPage
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
