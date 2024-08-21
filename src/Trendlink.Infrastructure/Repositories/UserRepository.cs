using Microsoft.EntityFrameworkCore;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;
using Trendlink.Infrastructure.Specifications.Users;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<User?> GetByIdWithRolesAsync(
            UserId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(new UserByIdWithRolesSpecification(id))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User?> GetByIdentityIdAsync(
            string identityId,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                .dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.IdentityId == identityId, cancellationToken);
        }

        public override void Add(User user)
        {
            foreach (Role role in user.Roles)
            {
                this.dbContext.Attach(role);
            }

            this.dbContext.Add(user);
        }

        public async Task<bool> ExistsByIdAsync(
            UserId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ExistsAsync(user => user.Id == id, cancellationToken);
        }

        public async Task<bool> ExistByEmailAsync(
            Email email,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ExistsAsync(user => user.Email == email, cancellationToken);
        }
    }
}
