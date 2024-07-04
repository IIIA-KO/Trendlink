using Microsoft.EntityFrameworkCore;
using Trendlink.Domain.Users.Countries;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class CountryRepository : Repository<Country, CountryId>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext dbContext)
            : base(dbContext) {}

        public async Task<bool> CountryExists(CountryId id)
        {
            return await this.dbContext.Set<Country>()
                .AsNoTracking()
                .AnyAsync(country => country.Id == id);
        }
    }
}
