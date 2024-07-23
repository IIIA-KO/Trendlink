using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Cooperations.GetLoggedInUserCooperations
{
    public sealed record GetLoggedInUserCooperationsQuery
        : IQuery<IReadOnlyList<CooperationResponse>>;
}
