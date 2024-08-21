using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;

namespace Trendlink.Application.Abstractions.Repositories
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
