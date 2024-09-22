namespace Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceGenderPercentage
{
    public class AudienceGenderStatistics
    {
        public List<AudienceGenderPercentageResponse> GenderPercentages { get; }

        public AudienceGenderStatistics(Dictionary<string, int> genderCounts, int totalFollowers)
        {
            this.GenderPercentages = genderCounts
                .Select(g => new AudienceGenderPercentageResponse
                {
                    Gender = g.Key switch
                    {
                        "F" => "Female",
                        "M" => "Male",
                        _ => "Unknown"
                    },
                    Percentage = (double)g.Value / totalFollowers * 100
                })
                .ToList();
        }
    }
}
