using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Trendlink.Infrastructure.Authentication;

namespace Trendlink.Infrastructure.Authorization
{
    internal sealed class CustomClaimsTransformation : IClaimsTransformation
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomClaimsTransformation(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (
                principal.Identity is not { IsAuthenticated: true }
                || principal.HasClaim(claim => claim.Type == ClaimTypes.Role)
                    && principal.HasClaim(claim => claim.Type == JwtRegisteredClaimNames.Sub)
            )
            {
                return principal;
            }

            using IServiceScope scope = this._serviceProvider.CreateScope();

            AuthorizationService authorizationService =
                scope.ServiceProvider.GetRequiredService<AuthorizationService>();

            string identityId = principal.GetIdentityId();

            UserRolesResponse userRoles = await authorizationService.GetRolesForUserAsync(
                identityId
            );

            var claimsIdentity = new ClaimsIdentity();

            claimsIdentity.AddClaim(
                new Claim(JwtRegisteredClaimNames.Sub, userRoles.UserId.Value.ToString())
            );

            foreach (Domain.Users.Role role in userRoles.Roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
            }

            principal.AddIdentity(claimsIdentity);

            return principal;
        }
    }
}
