using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Calendar.UblockDate
{
    public sealed record UnblockDateCommand(DateOnly Date) : ICommand;
}
