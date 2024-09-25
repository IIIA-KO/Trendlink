using Trendlink.Application.Users.Instagarm.Audience.GetAudienceGenderPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceLocationPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceReachPercentage;
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

        Task<Result<AudienceReachStatistics>> GetAudienceReachPercentage(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        );

        Task<Result<string>> GetAudienceAgesPercentage(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        );
    }
}
