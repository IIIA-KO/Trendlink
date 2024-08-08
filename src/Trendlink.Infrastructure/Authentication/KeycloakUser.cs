using Newtonsoft.Json;

namespace Trendlink.Infrastructure.Authentication
{
    public class KeycloakUser
    {
        [JsonProperty("attributes")]
        public Dictionary<string, List<string>> Attributes { get; set; }
    }
}
