using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Calendar.GetUserCalendarForMonth
{
    public sealed record GetUserCalendarForMonthQuery(UserId UserId, int Month, int Year)
        : IQuery<IReadOnlyList<DateResponse>>;
}
