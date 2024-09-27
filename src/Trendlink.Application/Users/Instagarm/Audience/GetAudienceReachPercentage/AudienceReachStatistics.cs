namespace Trendlink.Application.Users.Instagarm.Audience.GetAudienceReachPercentage
{
    public class AudienceReachStatistics
    {
        public int TotalReach { get; }

        public List<AudienceReachPercentageResponse> ReachPercentages { get; }

        public AudienceReachStatistics(
            int totalReach,
            List<AudienceReachPercentageResponse> reachPercentages
        )
        {
            this.TotalReach = totalReach;
            this.ReachPercentages = reachPercentages;
        }
    }
}
