namespace Trendlink.Application.Instagarm.Audience.GetAudienceLocationPercentage
{
    public class AudienceLocationStatistics
    {
        public List<AudienceLocationPercentageResponse> LocationPercentages { get; }

        public AudienceLocationStatistics(
            List<AudienceLocationPercentageResponse> locationPercentages
        )
        {
            this.LocationPercentages = locationPercentages;
        }
    }
}
