﻿using FluentValidation;

namespace Trendlink.Application.Instagarm.Statistics.GetEngagementStatistics
{
    internal sealed class GetEngagementStatisticsQueryValidator
        : AbstractValidator<GetEngagementStatisticsQuery>
    {
        public GetEngagementStatisticsQueryValidator()
        {
            this.RuleFor(c => c.StatisticsPeriod).IsInEnum();
        }
    }
}
