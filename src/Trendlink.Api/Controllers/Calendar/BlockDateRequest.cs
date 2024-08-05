using System.Text.Json.Serialization;

namespace Trendlink.Api.Controllers.Calendar
{
    public sealed class BlockDateRequest
    {
        [JsonRequired]
        public DateOnly Date { get; init; }
    }
}
