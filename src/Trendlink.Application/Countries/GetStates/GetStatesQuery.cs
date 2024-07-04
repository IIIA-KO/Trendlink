using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users.Countries;

namespace Trendlink.Application.Countries.GetStates
{
    public sealed record GetStatesQuery(Guid CountryId)
        : IQuery<IReadOnlyCollection<StateResponse>>;
}
