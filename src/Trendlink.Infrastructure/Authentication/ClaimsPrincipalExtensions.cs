using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Trendlink.Domain.Users;

namespace Trendlink.Infrastructure.Authentication
{
    internal static class ClaimsPrincipalExtensions
    {
        public static UserId GetUserId(this ClaimsPrincipal? principal)
        {
            string? userId = principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            return new UserId(
                Guid.TryParse(userId, out Guid parsedUserId)
                    ? parsedUserId
                    : throw new ApplicationException("User id is unavailable")
            );
        }

        public static string GetIdentityId(this ClaimsPrincipal? principal)
        {
            return principal?.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new ApplicationException("User identity is unavailable");
        }
    }
}
