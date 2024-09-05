using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Calendar.GetLoggedInUserCooperations
{
    public sealed record GetLoggedInUserCooperationsQuery : IQuery<IReadOnlyList<DateResponse>>;
}
