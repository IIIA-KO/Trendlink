using Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceGenderPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceReachPercentage;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Abstractions.Instagram
{
    public interface IInstagramAudienceService
    {
        Task<Result<AudienceGenderStatistics>> GetUserAudienceGenderPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        );

        Task<Result<AudienceReachStatistics>> GetUserAudienceReachPercentage(
            string accessToken,
            string instagramAccountId,
            DateOnly since,
            DateOnly until,
            CancellationToken cancellationToken = default
        );
    }
}
