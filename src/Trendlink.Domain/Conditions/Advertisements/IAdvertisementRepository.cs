using Trendlink.Domain.Conditions.Advertisements.ValueObjects;

namespace Trendlink.Domain.Conditions.Advertisements
{
    public interface IAdvertisementRepository
    {
        Task<Advertisement?> GetByIdAsync(
            AdvertisementId id,
            CancellationToken cancellationToken = default
        );

        Task<Advertisement?> GetByIdWithUserAsync(
            AdvertisementId id,
            CancellationToken cancellationToken = default
        );

        void Add(Advertisement advertisement);

        void Remove(Advertisement advertisement);
    }
}
