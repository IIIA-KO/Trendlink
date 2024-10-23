using FluentValidation;

namespace Trendlink.Application.Extensions
{
    public static class AbstractValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> NotNullOrEmpty<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder
        )
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage($"{nameof(TProperty)} is required")
                .NotNull()
                .WithMessage($"{nameof(TProperty)} cannot be null.");
        }
    }
}
