using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Calendar.GetUserCalendar
{
    public sealed record GetUserCalendarQuery(
        UserId UserId,
        string? SearchTerm,
        int? StartMonth,
        int? StartYear,
        int? EndMonth,
        int? EndYear,
        CooperationStatus? CooperationStatus
    ) : IQuery<IReadOnlyList<DateResponse>>;
}
