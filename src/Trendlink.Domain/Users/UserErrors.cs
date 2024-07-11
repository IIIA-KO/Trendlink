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
            new("User.NotAuthorized", "User is not authorized to perform action");

        public static readonly Error RegistrationFailed =
            new(
                "User.RegistrationFailed ",
                "Failed to register user authentication service failure"
            );
    }
}
