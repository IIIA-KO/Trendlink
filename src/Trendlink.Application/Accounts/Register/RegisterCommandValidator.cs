﻿using FluentValidation;
using Trendlink.Application.Extensions;

namespace Trendlink.Application.Accounts.Register
{
    internal sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            this.RuleFor(c => c.FirstName).NotNullOrEmpty();

            this.RuleFor(c => c.FirstName.Value).NotNullOrEmpty();

            this.RuleFor(c => c.LastName).NotNullOrEmpty();

            this.RuleFor(c => c.LastName.Value).NotNullOrEmpty();

            this.RuleFor(c => c.BirthDate)
                .LessThan(DateOnly.FromDateTime(DateTime.UtcNow.Date.AddYears(-18)))
                .WithMessage("You must be at least 18 years old.");

            this.RuleFor(c => c.Email.Value)
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .NotEmpty()
                .WithMessage("Email is required.");

            this.RuleFor(p => p.PhoneNumber.Value)
                .NotEmpty()
                .NotNull()
                .WithMessage("Phone Number is required.")
                .MinimumLength(10)
                .WithMessage("PhoneNumber must not be less than 10 characters.")
                .MaximumLength(20)
                .WithMessage("PhoneNumber must not exceed 20 characters.")
                .Matches(@"^\d{10,20}$")
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
                .Matches(@"[\!\?\*\.\$]+")
                .WithMessage("Password must contain at least one (!? *.$).");

            this.RuleFor(c => c.StateId).NotEmpty().WithMessage("State is required.");
        }
    }
}
