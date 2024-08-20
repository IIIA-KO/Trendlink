using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users
{
    public static class UserErrors
    {
        public static readonly NotFoundError NotFound =
            new("User.NotFound", "The user with the specified identifier was not found");

        public static readonly UnauthorizedError InvalidCredentials =
            new("User.InvalidCredentials", "The provided credentials were invalid");

        public static readonly Error Underage =
            new("User.Underage", "The user must be at least 18 years old");

        public static readonly Error DuplicateEmail =
            new("User.DuplicateEmail", "User with this email already exists");

        public static readonly UnauthorizedError NotAuthorized =
            new("User.NotAuthorized", "User is not authorized to perform this action");

        public static readonly Error RegistrationFailed =
            new(
                "User.RegistrationFailed ",
                "Failed to register user authentication service failure"
            );

        public static readonly Error InstagramAccountAlreadyLinked =
            new("User.InstagramAccountAlreadyLinked ", "User has already linked instagram account");

        public static readonly Error FailedToGetFacebookPage =
            new(
                "User.FailedToGetFacebookPage",
                "Failed to get access to user's Facebook Business Page"
            );

        public static readonly Error IncorrectFacebookPagesCount =
            new(
                "User.IncorrectFacebookPagesCount",
                "User has selected incorrect quantity of Facebook Pages. Required count is exactly one"
            );
    }
}
