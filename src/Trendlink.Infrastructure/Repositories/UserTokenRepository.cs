using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Users;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class UserTokenRepository : IUserTokenRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserTokenRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<UserToken?> GetByIdAsync(Guid id)
        {
            return await this._dbContext.Set<UserToken>().FindAsync(id);
        }

        public void Add(UserToken userToken)
        {
            this._dbContext.Set<UserToken>().Add(userToken);
        }

        public void Update(UserToken userToken)
        {
            this._dbContext.Set<UserToken>().Update(userToken);
        }
    }
}
