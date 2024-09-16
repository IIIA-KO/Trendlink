using Trendlink.Application.Users.Instagarm.GetUserAudienceGenderPercentage;
using Trendlink.Infrastructure.Instagram.Models.Audience;

namespace Trendlink.Infrastructure.Instagram
{
    internal interface IInstagramAudienceService
    {
        Task<List<AudienceGenderPercentageResponse>> GetUserAudienceGenderPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        );
    }
}
