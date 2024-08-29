using Trendlink.Domain.Users;

namespace Trendlink.Application.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

        Task<User?> GetByIdWithRolesAsync(UserId id, CancellationToken cancellationToken = default);

        Task<User?> GetByIdWithInstagramAccountAsync(
            UserId id,
            CancellationToken cancellationToken = default
        );

        Task<User?> GetByIdWithInstagramAccountAndTokenAsync(
            UserId id,
            CancellationToken cancellationToken = default
        );

        Task<User?> GetByIdentityIdAsync(
            string identityId,
            CancellationToken cancellationToken = default
        );

        void Add(User user);

        Task<bool> ExistsByIdAsync(UserId id, CancellationToken cancellationToken = default);

        Task<bool> ExistByEmailAsync(Email email, CancellationToken cancellationToken = default);

        IQueryable<User> DbSetAsQueryable();
    }
}
