namespace Trendlink.Application.Instagarm.Audience.GetAudienceAgePercentage
{
    public class AudienceAgeStatistics
    {
        public List<AudienceAgePercentageResponse> AgePercentages { get; }

        public AudienceAgeStatistics(List<AudienceAgePercentageResponse> agePercentages)
        {
            this.AgePercentages = agePercentages;
        }
    }
}
