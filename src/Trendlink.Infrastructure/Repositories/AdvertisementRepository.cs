﻿using Microsoft.EntityFrameworkCore;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Infrastructure.Specifications.Advertisements;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class AdvertisementRepository
        : Repository<Advertisement, AdvertisementId>,
            IAdvertisementRepository
    {
        public AdvertisementRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<Advertisement?> GetByIdWithUserAsync(
            AdvertisementId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(
                    new AdvertisementByIdWithConditionSpecification(id)
                )
                .FirstAsync(cancellationToken);
        }
    }
}
