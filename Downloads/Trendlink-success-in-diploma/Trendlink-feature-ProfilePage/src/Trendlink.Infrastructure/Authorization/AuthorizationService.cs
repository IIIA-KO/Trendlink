using Microsoft.EntityFrameworkCore;
using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Users;

namespace Trendlink.Infrastructure.Authorization
{
    internal sealed class AuthorizationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICacheService _cacheService;

        public AuthorizationService(ApplicationDbContext dbContext, ICacheService cacheService)
        {
            this._dbContext = dbContext;
            this._cacheService = cacheService;
        }

        public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
        {
            string cacheKey = $"auth:roles-{identityId}";

            UserRolesResponse? cachedRoles = await this._cacheService.GetAsync<UserRolesResponse>(
                cacheKey
            );

            if (cachedRoles is not null)
            {
                return cachedRoles;
            }

            UserRolesResponse roles = await this
                ._dbContext.Set<User>()
                .Where(user => user.IdentityId == identityId)
                .Select(user => new UserRolesResponse
                {
                    UserId = user.Id,
                    Roles = user.Roles.ToList()
                })
                .FirstAsync();

            await this._cacheService.SetAsync(cacheKey, roles);

            return roles;
        }
    }
}
