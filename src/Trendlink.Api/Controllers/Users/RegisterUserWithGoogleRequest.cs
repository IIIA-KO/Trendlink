using System.Text.Json.Serialization;

namespace Trendlink.Api.Controllers.Users
{
    public sealed class RegisterUserWithGoogleRequest
    {
        public string AccessToken { get; set; }

        [JsonRequired]
        public DateOnly BirthDate { get; init; }

        public string PhoneNumber { get; init; }

        [JsonRequired]
        public Guid StateId { get; init; }
    }
}
