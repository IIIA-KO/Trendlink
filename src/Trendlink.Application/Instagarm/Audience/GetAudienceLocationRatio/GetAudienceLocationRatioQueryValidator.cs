using FluentValidation;

namespace Trendlink.Application.Instagarm.Audience.GetAudienceLocationRatio
{
    internal sealed class GetAudienceLocationRatioQueryValidator
        : AbstractValidator<GetAudienceLocationRatioQuery>
    {
        public GetAudienceLocationRatioQueryValidator()
        {
            this.RuleFor(c => c.LocationType).IsInEnum();
        }
    }
}
