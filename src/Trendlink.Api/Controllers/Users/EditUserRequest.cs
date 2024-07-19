using System.Text.Json.Serialization;
using Trendlink.Domain.Users;

namespace Trendlink.Api.Controllers.Users
{
    public sealed record EditUserRequest
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }

        [JsonRequired]
        public DateOnly BirthDate { get; init; }

        [JsonRequired]
        public Guid StateId { get; init; }

        public string Bio { get; init; }

        [JsonRequired]
        public AccountType AccountType { get; init; }

        [JsonRequired]
        public AccountCategory AccountCategory { get; init; }
    }
}
