namespace Trendlink.Domain.Cooperations
{
    public interface ICooperationRepository
    {
        Task<Cooperation?> GetByIdAsync(
            CooperationId id,
            CancellationToken cancellationToken = default
        );

        void Add(Cooperation cooperation);
    }
}
