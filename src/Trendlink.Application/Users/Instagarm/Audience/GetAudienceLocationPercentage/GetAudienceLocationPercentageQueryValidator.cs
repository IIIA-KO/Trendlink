using FluentValidation;

namespace Trendlink.Application.Users.Instagarm.Audience.GetAudienceLocationPercentage
{
    internal sealed class GetAudienceLocationPercentageQueryValidator
        : AbstractValidator<GetAudienceLocationPercentageQuery>
    {
        public GetAudienceLocationPercentageQueryValidator()
        {
            this.RuleFor(c => c.LocationType).IsInEnum();
        }
    }
}
