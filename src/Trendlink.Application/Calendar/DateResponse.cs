using Trendlink.Application.Calendar.GetLoggedInUserCooperations;

namespace Trendlink.Application.Calendar
{
    public sealed class DateResponse
    {
        public DateOnly Date { get; init; }

        public bool IsBlocked { get; init; }

        public List<CooperationResponse> Cooperations { get; init; }
    }
}
