using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Conditions.Advertisements;

namespace Trendlink.Application.Advertisements.DeleteAdvertisement
{
    public sealed record DeleteAdvertisementCommand(AdvertisementId AdvertisementId) : ICommand;
}
