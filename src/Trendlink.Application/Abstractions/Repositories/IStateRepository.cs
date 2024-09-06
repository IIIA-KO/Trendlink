using Trendlink.Domain.Users.States;

namespace Trendlink.Application.Abstractions.Repositories
{
    public interface IStateRepository
    {
        Task<State?> GetByIdAsync(StateId id, CancellationToken cancellationToken = default);

        Task<bool> ExistsByIdAsync(StateId stateId, CancellationToken cancellationToken = default);
    }
}
