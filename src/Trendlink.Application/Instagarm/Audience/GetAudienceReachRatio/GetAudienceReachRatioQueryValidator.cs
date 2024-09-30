using FluentValidation;

namespace Trendlink.Application.Instagarm.Audience.GetAudienceReachRatio
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
