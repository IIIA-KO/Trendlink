using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Common;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;

namespace Trendlink.Application.Cooperations.PendCooperation
{
    public sealed record PendCooperationCommand(
        Name Name,
        Description Description,
        DateTimeOffset ScheduledOnUtc,
        AdvertisementId AdvertisementId
    ) : ICommand<CooperationId>;
}
