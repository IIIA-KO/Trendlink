using Trendlink.Domain.Cooperations;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class CooperationRepository
        : Repository<Cooperation, CooperationId>,
            ICooperationRepository
    {
        public CooperationRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
