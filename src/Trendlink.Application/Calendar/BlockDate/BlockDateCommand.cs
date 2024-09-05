using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Calendar.BlockDate
{
    public sealed record BlockDateCommand(DateOnly Date) : ICommand;
}
