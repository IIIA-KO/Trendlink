using MediatR;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}