using System.Text.Json.Serialization;

namespace Trendlink.Api.Controllers.Reviews
{
    public sealed class CreateReviewRequest
    {
        [JsonRequired]
        public int Rating { get; set; }

        [JsonRequired]
        public string Comment { get; set; }
    };
}
