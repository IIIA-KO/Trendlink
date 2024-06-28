using Trendlink.Domain.Users.Cities;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class CityRepository : Repository<City, CityId>, ICityRepository
    {
        public CityRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
