using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users.VerificationTokens
{
    public static class EmailVerificationTokenErrors
    {
        public static readonly Error EmailAlreadyVerified =
            new(
                "EmailVerificationToken.EmailAlreadyVerified ",
                "Email of the user is already verified"
            );

        public static readonly Error EmailNotVerified =
            new("EmailVerificationToken.EmailNotVerified", "Email of the user is not verified");

        public static readonly NotFoundError NotFound = new NotFoundError(
            "EmailVerificationToken.NotFound",
            "Email verification token with specified identifier was not found"
        );
    }
}
