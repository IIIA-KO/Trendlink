using System.Text.Json.Serialization;

namespace Trendlink.Api.Controllers.Users
{
    public sealed record RegisterUserRequest
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }

        [JsonRequired]
        public DateOnly BirthDate { get; init; }

        public string Email { get; init; }

        public string PhoneNumber { get; init; }

        public string Password { get; init; }

        [JsonRequired]
        public Guid StateId { get; init; }
    }
}
