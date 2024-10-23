using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Calendar.GetLoggedInUserCalendarForMonth
{
    public sealed record GetLoggedInUserCalendarForMonthQuery(int Month, int Year)
        : IQuery<IReadOnlyList<LoggedInDateResponse>>;
}
