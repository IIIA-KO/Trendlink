using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Shared;

namespace Trendlink.Application.Cooperations.PendCooperation
{
    public sealed record PendCooperationCommand(
        Name Name,
        Description Description,
        DateTimeOffset ScheduledOnUtc,
        Money Price,
        AdvertisementId AdvertisementId
    ) : ICommand<CooperationId>;
}
