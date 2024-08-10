namespace Trendlink.Application.Users
{
    public sealed class UserResponse
    {
        public UserResponse() { }

        public UserResponse(Guid id, string Email, string firstName, string lastName)
        {
            this.Id = id;
            this.Email = Email;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public Guid Id { get; init; }

        public string Email { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }
    }
}
