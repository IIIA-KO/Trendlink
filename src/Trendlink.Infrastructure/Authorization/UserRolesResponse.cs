using Trendlink.Domain.Users;

namespace Trendlink.Infrastructure.Authorization
{
    internal sealed class UserRolesResponse
    {
        public UserId UserId { get; init; }

        public List<Role> Roles { get; init; } = [];
    }
}
