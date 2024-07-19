using Microsoft.AspNetCore.Http;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Infrastructure.Authentication
{
    internal sealed class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public UserId UserId =>
            this._httpContextAccessor.HttpContext?.User.GetUserId()
            ?? throw new ApplicationException("User context is unavailable");

        public string IdentityId =>
            this._httpContextAccessor.HttpContext?.User.GetIdentityId()
            ?? throw new ApplicationException("User context is unavailable");

        public string? AccessToken =>
            this
                ._httpContextAccessor.HttpContext?.Request.Headers.Authorization.FirstOrDefault()
                ?.Split(' ')
                .LastOrDefault();
    }
}
