using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.Instagarm.GetUserAudienceGenderPercentage
{
    public sealed record GetUserAudienceGenderPercentageQuery(UserId UserId)
        : IQuery<List<AudienceGenderPercentageResponse>>;
}
