using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Calendar.GetLoggedInUserCalendar
{
    public sealed record GetLoggedInUserCalendarQuery : IQuery<IReadOnlyList<LoggedInDateResponse>>;
}
