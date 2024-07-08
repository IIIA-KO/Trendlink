using Trendlink.Domain.Users.States;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class StateRepository : Repository<State, StateId>, IStateRepository
    {
        public StateRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
