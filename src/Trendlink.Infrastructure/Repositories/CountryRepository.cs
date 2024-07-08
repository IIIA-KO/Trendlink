﻿using Microsoft.EntityFrameworkCore;
using Trendlink.Domain.Users.Countries;
using Trendlink.Infrastructure.Specifications.Countries;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class CountryRepository : Repository<Country, CountryId>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<Country?> GetByIdWithStatesAsync(
            CountryId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(new CountryByIdWithStatesSpecification(id))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> CountryExists(CountryId id)
        {
            return await this
                .dbContext.Set<Country>()
                .AsNoTracking()
                .AnyAsync(country => country.Id == id);
        }
    }
}
