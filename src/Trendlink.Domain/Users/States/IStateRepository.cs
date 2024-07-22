namespace Trendlink.Domain.Users.States
{
    public interface IStateRepository
    {
        Task<State?> GetByIdAsync(StateId id, CancellationToken cancellationToken = default);

        Task<bool> ExistsByIdAsync(StateId stateId, CancellationToken cancellationToken = default);
    }
}
