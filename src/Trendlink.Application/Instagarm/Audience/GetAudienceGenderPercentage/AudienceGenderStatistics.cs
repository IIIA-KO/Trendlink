﻿namespace Trendlink.Application.Instagarm.Audience.GetAudienceGenderPercentage
{
    public class AudienceGenderStatistics
    {
        public List<AudienceGenderPercentageResponse> GenderPercentages { get; }

        public AudienceGenderStatistics(List<AudienceGenderPercentageResponse> genderPercentages)
        {
            this.GenderPercentages = genderPercentages;
        }
    }
}