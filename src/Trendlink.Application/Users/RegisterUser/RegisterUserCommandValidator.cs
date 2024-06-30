using FluentValidation;

namespace Trendlink.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            this.RuleFor(c => c.FirstName).NotEmpty().WithMessage("First name is required.");

            this.RuleFor(c => c.LastName).NotEmpty().WithMessage("Last name is required.");

            this.RuleFor(c => c.BirthDate)
                .LessThan(DateOnly.FromDateTime(DateTime.UtcNow.Date.AddYears(-18)))
                .WithMessage("You must be at least 18 years old.");

            this.RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .NotEmpty()
                .WithMessage("Email is required.");

            this.RuleFor(p => p.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .WithMessage("Phone Number is required.")
                .MinimumLength(10)
                .WithMessage("PhoneNumber must not be less than 10 characters.")
                .MaximumLength(20)
                .WithMessage("PhoneNumber must not exceed 50 characters.")
                .Matches(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")
                .WithMessage("PhoneNumber not valid");

            this.RuleFor(c => c.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(8)
                .WithMessage("Password length must be at least 8.")
                .MaximumLength(16)
                .WithMessage("Password length must not exceed 16.")
                .Matches(@"[A-Z]+")
                .WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+")
                .WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+")
                .WithMessage("Password must contain at least one number.")
                .Matches(@"[\!\?\*\.]+")
                .WithMessage("Password must contain at least one (!? *.).");

            this.RuleFor(c => c.CityId).NotEmpty().WithMessage("City is required.");
        }
    }
}
