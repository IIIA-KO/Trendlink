namespace Trendlink.Domain.Users.Countries
{
    public interface ICountryRepository
    {
        Task<Country?> GetByIdAsync(CountryId id, CancellationToken cancellationToken = default);

        Task<bool> CountryExists(CountryId id);
    }
}
