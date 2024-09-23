using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users.DomainEvents;
using Trendlink.Domain.Users.InstagramBusinessAccount;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.Token;

namespace Trendlink.Domain.Users
{
    public sealed class User : Entity<UserId>
    {
        private readonly List<Role> _roles = [];

        private readonly List<Notification> _notifications = [];

        private const int MinimumAge = 18;

        private User(
            UserId id,
            FirstName firstName,
            LastName lastName,
            DateOnly birthDate,
            StateId stateId,
            Email email,
            PhoneNumber phoneNumber
        )
            : base(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.StateId = stateId;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }

        public User() { }

        public FirstName FirstName { get; private set; }

        public LastName LastName { get; private set; }

        public Photo? ProfilePhoto { get; private set; }

        public DateOnly BirthDate { get; private set; }

        public Email Email { get; init; }

        public StateId StateId { get; private set; }

        public State State { get; init; }

        public PhoneNumber PhoneNumber { get; init; }

        public Bio Bio { get; set; } = new Bio(string.Empty);

        public AccountCategory AccountCategory { get; set; } = AccountCategory.None;

        public string IdentityId { get; private set; } = string.Empty;

        public Condition? Condition { get; init; }

        public UserToken? Token { get; set; }

        public InstagramAccount? InstagramAccount { get; set; }

        public IReadOnlyCollection<Role> Roles => this._roles.AsReadOnly();

        public IReadOnlyCollection<Notification> Notifications => this._notifications.AsReadOnly();

        public void AddRole(Role role)
        {
            if (!this._roles.Contains(role))
            {
                this._roles.Add(role);
            }
        }

        public bool HasRole(Role role)
        {
            return this._roles.Any(r => string.Equals(r.Name, role.Name, StringComparison.Ordinal));
        }

        public Result Update(
            FirstName firstName,
            LastName lastName,
            DateOnly birthDate,
            StateId stateId,
            Bio bio,
            AccountCategory accountCategory
        )
        {
            if (!IsOfLegalAge(birthDate))
            {
                return Result.Failure(UserErrors.Underage);
            }

            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.StateId = stateId;
            this.Bio = bio;
            this.AccountCategory = accountCategory;

            return Result.Success();
        }

        public static Result<User> Create(
            FirstName firstName,
            LastName lastName,
            DateOnly birthDate,
            StateId stateId,
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

            var user = new User(
                UserId.New(),
                firstName,
                lastName,
                birthDate,
                stateId,
                email,
                phoneNumber
            );

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
            if (
                firstName is null
                || string.IsNullOrEmpty(firstName.Value)
                || lastName is null
                || string.IsNullOrEmpty(lastName.Value)
                || email is null
                || string.IsNullOrEmpty(email.Value)
                || phoneNumber is null
                || string.IsNullOrEmpty(phoneNumber.Value)
            )
            {
                return Result.Failure(UserErrors.InvalidCredentials);
            }

            return Result.Success();
        }

        internal static bool IsOfLegalAge(DateOnly birthDate)
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

        public void SetIdentityId(string identityId)
        {
            this.IdentityId =
                identityId
                ?? throw new ArgumentNullException(
                    nameof(identityId),
                    "IdentityId cannot be null."
                );
        }

        public void SetProfilePhoto(Photo photo)
        {
            ArgumentNullException.ThrowIfNull(photo.Uri);

            this.ProfilePhoto = photo;
        }

        public void RemoveProfilePhoto()
        {
            this.ProfilePhoto = null;
        }

        public void LinkInstagramAccount(
            InstagramAccount instagramAccount,
            string facebookAccessToken,
            DateTimeOffset expiresAt
        )
        {
            ArgumentNullException.ThrowIfNull(instagramAccount);
            ArgumentException.ThrowIfNullOrEmpty(facebookAccessToken);

            this.InstagramAccount = instagramAccount;

            this.RaiseDomainEvent(
                new InstagramAccountLinkedDomainEvent(
                    this.Id,
                    instagramAccount,
                    facebookAccessToken,
                    expiresAt
                )
            );
        }
    }
}
