namespace Trendlink.Application.Instagarm.Audience.GetAudienceAgeRatio
{
    public sealed class AgeRatio
    {
        public List<AgePercentage> AgePercentages { get; }

        public AgeRatio(List<AgePercentage> agePercentages)
        {
            this.AgePercentages = agePercentages;
        }
    }
}
