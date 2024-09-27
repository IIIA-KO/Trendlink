using Trendlink.Application.Instagarm.Audience.GetAudienceAgePercentage;
using Trendlink.Application.Instagarm.Audience.GetAudienceGenderPercentage;
using Trendlink.Application.Instagarm.Audience.GetAudienceLocationPercentage;
using Trendlink.Application.Instagarm.Audience.GetAudienceReachPercentage;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Abstractions.Instagram
{
    public interface IInstagramAudienceService
    {
        Task<Result<AudienceGenderStatistics>> GetAudienceGenderPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        );

        Task<Result<AudienceLocationStatistics>> GetAudienceTopLocations(
            string accessToken,
            string instagramAccountId,
            LocationType locationType,
            CancellationToken cancellationToken = default
        );

        Task<Result<AudienceAgeStatistics>> GetAudienceAgesPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        );

        Task<Result<AudienceReachStatistics>> GetAudienceReachPercentage(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        );
    }
}
