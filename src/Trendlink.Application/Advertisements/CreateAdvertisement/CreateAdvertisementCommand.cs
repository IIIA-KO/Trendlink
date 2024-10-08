﻿using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Shared;

namespace Trendlink.Application.Advertisements.CreateAdvertisement
{
    public sealed record CreateAdvertisementCommand(Name Name, Money Price, Description Description)
        : ICommand<AdvertisementId>;
}
