using Trendlink.Domain.Users;

namespace Trendlink.Application.Abstractions.Authentication
{
    public interface IUserContext
    {
        UserId UserId { get; }

        string IdentityId { get; }

        string? AccessToken { get; }
    }
}
