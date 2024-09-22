using Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceGenderPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceReachPercentage;

namespace Trendlink.Application.Abstractions.Instagram
{
    public interface IInstagramAudienceService
    {
        Task<AudienceGenderStatistics> GetUserAudienceGenderPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        );

        Task<AudienceReachStatistics> GetUserAudienceReachPercentage(
            string accessToken,
            string instagramAccountId,
            DateOnly since,
            DateOnly until,
            CancellationToken cancellationToken = default
        );
    }
}
