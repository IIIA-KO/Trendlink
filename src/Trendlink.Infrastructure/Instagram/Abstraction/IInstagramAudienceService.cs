using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Instagarm.Audience.GetAudienceLocationPercentage;
using Trendlink.Application.Instagarm.Audience.GetAudienceLocationRatio;
using Trendlink.Application.Instagarm.Audience.GetAudienceReachPercentage;
using Trendlink.Domain.Abstraction;
using Trendlink.Infrastructure.Instagram.Models.Audience;

namespace Trendlink.Infrastructure.Instagram.Abstraction
{
    internal interface IInstagramAudienceService
    {
        Task<Result<GenderRatio>> GetAudienceGenderPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        );

        Task<Result<LocationRatio>> GetAudienceTopLocations(
            string accessToken,
            string instagramAccountId,
            LocationType locationType,
            CancellationToken cancellationToken = default
        );

        Task<Result<AgeRatio>> GetAudienceAgesPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        );

        Task<Result<ReachRatio>> GetAudienceReachPercentage(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        );
    }
}
