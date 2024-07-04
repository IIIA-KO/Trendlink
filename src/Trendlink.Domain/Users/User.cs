using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.DomainEvents;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Domain.Users
{
    public sealed class User : Entity<UserId>
    {
        private readonly List<Role> _roles = [];

        private const int MinimumAge = 18;

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

        public StateId StateId { get; private set; }

        public State State { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public Bio Bio { get; set; } = new Bio(string.Empty);

        public AccountType AccountType { get; set; } = AccountType.Personal;

        public AccountCategory AccountCategory { get; set; } = AccountCategory.None;

        public string IdentityId { get; private set; } = string.Empty;

        public IReadOnlyCollection<Role> Roles => this._roles.AsReadOnly();

        public void Update(
            FirstName firstName,
            LastName lastName,
            DateOnly birthDate,
            State state,
            Bio bio,
            AccountType accountType,
            AccountCategory accountCategory
        )
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.SetState(state);
            this.Bio = bio;
            this.AccountType = accountType;
            this.AccountCategory = accountCategory;
        }

        public static Result<User> Create(
            FirstName firstName,
            LastName lastName,
            DateOnly birthDate,
            Email email,
            PhoneNumber phoneNumber
        )
        {
            Result validationResult = ValidateParameters(firstName, lastName, email, phoneNumber);
            if (validationResult.IsFailure)
            {
                return Result.Failure<User>(validationResult.Error);
            }

            if (!IsOfLegalAge(birthDate))
            {
                return Result.Failure<User>(UserErrors.Underage);
            }

            var user = new User(UserId.New(), firstName, lastName, birthDate, email, phoneNumber);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

            user._roles.Add(Role.Registered);

            return user;
        }

        private static Result ValidateParameters(
            FirstName firstName,
            LastName lastName,
            Email email,
            PhoneNumber phoneNumber
        )
        {
            if (firstName is null || lastName is null || email is null || phoneNumber is null)
            {
                return Result.Failure(UserErrors.InvalidCredentials);
            }

            return Result.Success();
        }

        private static bool IsOfLegalAge(DateOnly birthDate)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            var birthDateTimeOffset = new DateTimeOffset(
                birthDate.ToDateTime(TimeOnly.MinValue),
                TimeSpan.Zero
            );
            int age = now.Year - birthDateTimeOffset.Year;

            if (now < birthDateTimeOffset.AddYears(age))
            {
                age--;
            }

            return age >= MinimumAge;
        }

        public void SetState(State state)
        {
            this.State =
                state ?? throw new ArgumentNullException(nameof(state), "State cannot be null.");

            this.StateId = state.Id;
        }

        public void SetIdentityId(string identityId)
        {
            this.IdentityId =
                identityId
                ?? throw new ArgumentNullException(
                    nameof(identityId),
                    "IdentityId cannot be null."
                );
        }
    }
}
