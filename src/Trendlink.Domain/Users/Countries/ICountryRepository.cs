namespace Trendlink.Domain.Users.Countries
{
    public interface ICountryRepository
    {
        Task<Country?> GetByIdAsync(CountryId id, CancellationToken cancellationToken = default);

        Task<Country?> GetByIdWithStatesAsync(
            CountryId id,
            CancellationToken cancellationToken = default
        );

        Task<bool> ExistsById(CountryId id, CancellationToken cancellationToken = default);
    }
}
