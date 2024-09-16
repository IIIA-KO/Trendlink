using Trendlink.Application.Users.Instagarm.GetUserAudienceGenderPercentage;

namespace Trendlink.Application.Abstractions.Instagram
{
    public interface IInstagramAudienceService
    {
        Task<List<AudienceGenderPercentageResponse>> GetUserAudienceGenderPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        );
    }
}
