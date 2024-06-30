using System.Text.Json.Serialization;

namespace Trendlink.Api.Controllers.Users
{
    public sealed record RegisterUserRequest
    {
        [JsonRequired]
        public string FirstName { get; init; }

        [JsonRequired]
        public string LastName { get; init; }

        [JsonRequired]
        public DateOnly BirthDate { get; init; }

        [JsonRequired]
        public string Email { get; init; }

        [JsonRequired]
        public string PhoneNumber { get; init; }

        [JsonRequired]
        public string Password { get; init; }

        [JsonRequired]
        public Guid CityId { get; init; }
    }
}
