using FluentValidation;

namespace Trendlink.Application.Conditions.CreateCondition
{
    internal sealed class CreateConditionCommandValidator
        : AbstractValidator<CreateConditionCommand>
    {
        public CreateConditionCommandValidator()
        {
            this.RuleFor(c => c.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}
