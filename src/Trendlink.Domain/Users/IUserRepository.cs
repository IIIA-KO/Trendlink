using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

        Task<User?> GetByIdWithRolesAsync(UserId id, CancellationToken cancellationToken = default);

        void Add(User user);

        Task<bool> UserExists(Email email);
    }
}
