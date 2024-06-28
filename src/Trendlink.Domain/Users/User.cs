using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.Cities;
using Trendlink.Domain.Users.DomainEvents;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Domain.Users
{
    public sealed class User : Entity<UserId>
    {
        private readonly List<Role> _roles = [];

        private User(
            UserId id,
            FirstName firstName,
            LastName lastName,
            DateOnly birthDate,
            Email email,
            PhoneNumber phoneNumber
        )
            : base(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }

        private User() { }

        public FirstName FirstName { get; private set; }

        public LastName LastName { get; private set; }

        public DateOnly BirthDate { get; private set; }

        public Email Email { get; private set; }

        public CityId CityId { get; private set; }

        public City City { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public Bio Bio { get; set; } = new Bio(string.Empty);

        public AccountType AccountType { get; set; } = AccountType.Personal;

        public AccountCategory AccountCategory { get; set; } = AccountCategory.None;

        public string IdentityId { get; private set; } = string.Empty;

        public IReadOnlyCollection<Role> Roles => this._roles.AsReadOnly();

        public static User Create(
            FirstName firstName,
            LastName lastName,
            DateOnly birthDate,
            Email email,
            PhoneNumber phoneNumber
        )
        {
            var user = new User(UserId.New(), firstName, lastName, birthDate, email, phoneNumber);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

            user._roles.Add(Role.Registered);

            return user;
        }

        public void SetCity(City city)
        {
            this.City = city 
                ?? throw new ArgumentNullException(nameof(city), "City cannot be null.");
            
            this.CityId = city.Id;
        }

        public void SetIdentityId(string identityId)
        {
            this.IdentityId = identityId
                ?? throw new ArgumentNullException(nameof(identityId), "IdentityId cannot be null.");
        }
    }
}
