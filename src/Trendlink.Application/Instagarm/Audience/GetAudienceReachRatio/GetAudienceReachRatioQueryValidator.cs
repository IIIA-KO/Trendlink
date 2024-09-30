using FluentValidation;

namespace Trendlink.Application.Instagarm.Audience.GetAudienceReachPercentage
{
    internal sealed class GetAudienceReachRatioQueryValidator
        : AbstractValidator<GetAudienceReachRatioQuery>
    {
        public GetAudienceReachRatioQueryValidator()
        {
            this.RuleFor(c => c.StatisticsPeriod).IsInEnum();
        }
    }
}
