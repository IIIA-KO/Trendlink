namespace Trendlink.Application.Calendar
{
    public sealed class DateResponse
    {
        public DateOnly Date { get; init; }

        public bool IsBlocked { get; set; }

        public List<CooperationResponse> Cooperations { get; init; }
    }
}
