using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Abstractions.Authentication
{
    public interface IUserContext
    {
        UserId UserId { get; }

        string IdentityId { get; }
    }
}
