using FluentValidation;

namespace Trendlink.Application.Users.Instagarm.Statistics.GetInteractionStatistics
{
    internal sealed class GetInteractionsStatisticsQueryValidator
        : AbstractValidator<GetInteractionStatisticsQuery>
    {
        public GetInteractionsStatisticsQueryValidator()
        {
            this.RuleFor(c => c.StatisticsPeriod).IsInEnum();
        }
    }
}
