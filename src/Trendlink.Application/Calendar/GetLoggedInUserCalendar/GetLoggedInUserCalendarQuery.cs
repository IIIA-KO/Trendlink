using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Cooperations;

namespace Trendlink.Application.Calendar.GetLoggedInUserCalendar
{
    public sealed record GetLoggedInUserCalendarQuery(
        string? SearchTerm,
        int? StartMonth,
        int? StartYear,
        int? EndMonth,
        int? EndYear,
        CooperationStatus? CooperationStatus
    ) : IQuery<IReadOnlyList<LoggedInDateResponse>>;
}
