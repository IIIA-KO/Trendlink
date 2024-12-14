using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Users;
using Trendlink.Infrastructure.Specifications.Users;
using User = Trendlink.Domain.Users.User;

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

        public async Task<User?> GetByIdWithStateAsync(
            UserId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(new UserByIdWithStateSpecification(id))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User?> GetByIdWithTokenAsync(
            UserId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(new UserByIdWithTokenSpecification(id))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User?> GetByIdWithInstagramAccountAsync(
            UserId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(new UserByIdWithInstagramAccountSpecification(id))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User?> GetByIdWithInstagramAccountAndTokenAsync(
            UserId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(
                    new UserByIdWithInstagramAccountSpecification(id)
                        & new UserByIdWithTokenSpecification(id)
                )
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User?> GetByIdWithStateAndInstagramAccountAsync(
            UserId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(
                    new UserByIdWithStateSpecification(id)
                        & new UserByIdWithInstagramAccountSpecification(id)
                )
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

        public IQueryable<User> SearchUsers(UserSeachParameters parameters)
        {
            IQueryable<User> query = this
                .dbContext.Set<User>()
                .Include(user => user.Roles)
                .Where(user => !user.Roles.Any(r => r.Name == Role.Administrator.Name))
                .Include(user => user.InstagramAccount)
                .Include(user => user.State)
                .ThenInclude(state => state.Country)
                .AsSplitQuery();

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.Where(user =>
                    ((string)user.FirstName).Contains(parameters.SearchTerm)
                    || ((string)user.LastName).Contains(parameters.SearchTerm)
                    || ((string)user.PhoneNumber).Contains(parameters.SearchTerm)
                );
            }

            if (parameters.AccountCategory is not null)
            {
                query = query.Where(user => user.AccountCategory == parameters.AccountCategory);
            }

            if (parameters.MinFollowersCount > 0)
            {
                query = query.Where(user =>
                    user.InstagramAccount != null
                    && user.InstagramAccount.Metadata.FollowersCount >= parameters.MinFollowersCount
                );
            }

            if (parameters.MinMediaCount > 0)
            {
                query = query.Where(user =>
                    user.InstagramAccount != null
                    && user.InstagramAccount.Metadata.MediaCount >= parameters.MinMediaCount
                );
            }

            return parameters.SortOrder?.ToUpperInvariant() == "DESC"
                ? query.OrderByDescending(GetSortProperty(parameters))
                : query.OrderBy(GetSortProperty(parameters));
        }

        private static Expression<Func<User, object>> GetSortProperty(
            UserSeachParameters parameters
        )
        {
            return parameters.SortColumn?.ToUpperInvariant() switch
            {
                "FIRSTNAME" => user => user.FirstName,
                "LASTNAME" => user => user.LastName,
                "PHONENUMBER" => user => user.PhoneNumber,
                "FOLLOWERSCOUNT" => user => user.InstagramAccount!.Metadata.FollowersCount,
                "MEDIACOUNT" => user => user.InstagramAccount!.Metadata.MediaCount,
                _ => user => user.Id,
            };
        }
    }
}
