using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Common;
using Trendlink.Domain.Conditions.Advertisements;

namespace Trendlink.Application.Advertisements.CreateAdvertisement
{
    public sealed record CreateAdvertisementCommand(Name Name, Money Price, Description Description)
        : ICommand<AdvertisementId>;
}
