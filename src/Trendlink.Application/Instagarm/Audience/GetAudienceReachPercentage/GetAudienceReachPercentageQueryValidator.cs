using FluentValidation;

namespace Trendlink.Application.Instagarm.Audience.GetAudienceReachPercentage
{
    internal sealed class GetAudienceReachPercentageQueryValidator
        : AbstractValidator<GetAudienceReachPercentageQuery>
    {
        public GetAudienceReachPercentageQueryValidator()
        {
            this.RuleFor(c => c.StatisticsPeriod).IsInEnum();
        }
    }
}
