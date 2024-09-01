using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Calendar.GetLoggedInUserCooperationsForMonth
{
    public sealed record GetLoggedInUserCooperationsForMonthQuery(int Month, int Year)
        : IQuery<IReadOnlyList<DateResponse>>;
}
