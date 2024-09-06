using System.Text.Json.Serialization;

namespace Trendlink.Api.Controllers.Me
{
    public sealed class GetLoggedInUserCalendarForMonthRequest
    {
        [JsonRequired]
        public int Month { get; init; }

        [JsonRequired]
        public int Year { get; init; }
    }
}
