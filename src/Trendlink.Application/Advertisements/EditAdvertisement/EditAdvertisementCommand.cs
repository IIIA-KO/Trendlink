using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Shared;

namespace Trendlink.Application.Advertisements.EditAdvertisement
{
    public sealed record EditAdvertisementCommand(
        AdvertisementId AdvertisementId,
        Name Name,
        Money Price,
        Description Description
    ) : ICommand;
}
