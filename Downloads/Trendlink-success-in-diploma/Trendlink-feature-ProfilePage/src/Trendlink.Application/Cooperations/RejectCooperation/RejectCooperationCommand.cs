using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Cooperations;

namespace Trendlink.Application.Cooperations.RejectCooperation
{
    public sealed record RejectCooperationCommand(CooperationId CooperationId) : ICommand;
}
