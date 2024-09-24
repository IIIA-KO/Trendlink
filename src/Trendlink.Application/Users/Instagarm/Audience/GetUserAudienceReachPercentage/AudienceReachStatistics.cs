namespace Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceReachPercentage
{
    public class AudienceReachStatistics
    {
        public List<AudienceReachPercentageResponse> ReachPercentages { get; }

        public AudienceReachStatistics(List<AudienceReachPercentageResponse> reachPercentages)
        {
            this.ReachPercentages = reachPercentages;
        }
    }
}
