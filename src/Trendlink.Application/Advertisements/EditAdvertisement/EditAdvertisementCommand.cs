using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;
using Trendlink.Domain.Conditions.ValueObjects;

namespace Trendlink.Application.Advertisements.EditAdvertisement
{
    public sealed record EditAdvertisementCommand(
        AdvertisementId AdvertisementId,
        Name Name,
        Money Price,
        Description Description
    ) : ICommand;
}
