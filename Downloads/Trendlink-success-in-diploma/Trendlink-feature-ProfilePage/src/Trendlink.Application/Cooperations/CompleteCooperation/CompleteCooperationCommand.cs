using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Cooperations;

namespace Trendlink.Application.Cooperations.CompleteCooperation
{
    public sealed record CompleteCooperationCommand(CooperationId CooperationId) : ICommand;
}
