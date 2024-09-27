using System.Text.Json.Serialization;

namespace Trendlink.Api.Controllers.Calendar
{
    public sealed class GetLoggedInUserCalendarForMonthRequest
    {
        [JsonRequired]
        public int Month { get; init; }

        [JsonRequired]
        public int Year { get; init; }
    }
}
