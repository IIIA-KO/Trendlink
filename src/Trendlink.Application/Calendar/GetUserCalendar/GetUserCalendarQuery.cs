using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Calendar.GetUserCalendar
{
    public sealed record GetUserCalendarQuery(UserId UserId) : IQuery<IReadOnlyList<DateResponse>>;
}
