using Trendlink.Application.Instagarm.Audience.GetAudienceAgeRatio;

namespace Trendlink.Infrastructure.Instagram.Models.Audience
{
    internal sealed class AgeRatio
    {
        public List<AgePercentage> AgePercentages { get; }

        public AgeRatio(List<AgePercentage> agePercentages)
        {
            this.AgePercentages = agePercentages;
        }
    }
}
