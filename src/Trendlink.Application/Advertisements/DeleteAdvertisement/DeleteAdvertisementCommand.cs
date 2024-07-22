using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;

namespace Trendlink.Application.Advertisements.DeleteAdvertisement
{
    public sealed record DeleteAdvertisementCommand(AdvertisementId AdvertisementId) : ICommand;
}
