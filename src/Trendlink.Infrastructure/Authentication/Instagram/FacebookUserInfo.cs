using System.Text.Json.Serialization;

namespace Trendlink.Infrastructure.Authentication.Instagram
{
    internal sealed class FacebookUserInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
