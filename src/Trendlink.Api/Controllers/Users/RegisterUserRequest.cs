using System.Text.Json.Serialization;

namespace Trendlink.Api.Controllers.Users
{
    public sealed record RegisterUserRequest
    {
        public RegisterUserRequest(
            string firstName,
            string lastName,
            DateOnly birthDate,
            string email,
            string phoneNumber,
            string password,
            Guid stateId
        )
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Password = password;
            this.StateId = stateId;
        }

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
