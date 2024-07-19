using Trendlink.Domain.Users.States;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class StateRepository : Repository<State, StateId>, IStateRepository
    {
        public StateRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<bool> ExistsByIdAsync(
            StateId stateId,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ExistsAsync(state => state.Id == stateId, cancellationToken);
        }
    }
}
