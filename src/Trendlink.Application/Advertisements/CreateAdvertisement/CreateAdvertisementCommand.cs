using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;
using Trendlink.Domain.Conditions.ValueObjects;

namespace Trendlink.Application.Advertisements.CreateAdvertisement
{
    public sealed record CreateAdvertisementCommand(Name Name, Money Price, Description Description)
        : ICommand<AdvertisementId>;
}
