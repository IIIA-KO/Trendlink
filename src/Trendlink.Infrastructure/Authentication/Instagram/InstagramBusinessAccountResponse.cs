using System.Text.Json.Serialization;

namespace Trendlink.Infrastructure.Authentication.Instagram
{
    internal class InstagramBusinessAccountResponse
    {
        [JsonPropertyName("instagram_business_account")]
        public InstagramAccount? InstagramAccount { get; set; }

        [JsonPropertyName("id")]
        public string FacebookBusinessPageId { get; set; }
    }

    internal class InstagramAccount
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("username")]
        public string UserName { get; set; }
    }
}
