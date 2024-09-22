using FluentValidation;

namespace Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceReachPercentage
{
    internal sealed class GetUserAudienceReachPercentageQueryValidator
        : AbstractValidator<GetUserAudienceReachPercentageQuery>
    {
        public GetUserAudienceReachPercentageQueryValidator()
        {
            this.RuleFor(c => c.StatisticsPeriod).IsInEnum();
        }
    }
}
