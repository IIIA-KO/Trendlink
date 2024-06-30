namespace Trendlink.Domain.Users.Cities
{
    public interface ICityRepository
    {
        Task<City?> GetByIdAsync(CityId id, CancellationToken cancellationToken = default);
    }
}
