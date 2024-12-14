using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Common;
using Trendlink.Domain.Conditions.Advertisements;

namespace Trendlink.Application.Advertisements.EditAdvertisement
{
    public sealed record EditAdvertisementCommand(
        AdvertisementId AdvertisementId,
        Name Name,
        Money Price,
        Description Description
    ) : ICommand;
}
