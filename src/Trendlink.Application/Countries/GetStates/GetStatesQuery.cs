using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Countries.GetStates
{
    public sealed record GetStatesQuery(Guid CountryId)
        : IQuery<IReadOnlyCollection<StateResponse>>;
}
