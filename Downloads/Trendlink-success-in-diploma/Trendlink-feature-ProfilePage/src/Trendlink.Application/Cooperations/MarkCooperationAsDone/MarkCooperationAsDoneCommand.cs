using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Cooperations;

namespace Trendlink.Application.Cooperations.MarkCooperationAsDone
{
    public sealed record MarkCooperationAsDoneCommand(CooperationId CooperationId) : ICommand;
}
