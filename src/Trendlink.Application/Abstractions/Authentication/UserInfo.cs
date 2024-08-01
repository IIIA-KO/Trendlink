using System.Text.Json.Serialization;

namespace Trendlink.Application.Abstractions.Authentication
{
    public class UserInfo
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        public string Code { get; set; }
    }
}
