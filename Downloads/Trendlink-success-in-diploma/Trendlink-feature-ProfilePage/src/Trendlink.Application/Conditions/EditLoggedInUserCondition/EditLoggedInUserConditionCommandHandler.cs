using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;

namespace Trendlink.Application.Conditions.EditLoggedInUserCondition
{
    internal sealed class EditLoggedInUserConditionCommandHandler
        : ICommandHandler<EditLoggedInUserConditionCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IConditionRepository _conditionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditLoggedInUserConditionCommandHandler(
            IUserContext userContext,
            IConditionRepository conditionRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._userContext = userContext;
            this._conditionRepository = conditionRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            EditLoggedInUserConditionCommand request,
            CancellationToken cancellationToken
        )
        {
            Condition? condition = await this._conditionRepository.GetByUserIdAsync(
                this._userContext.UserId,
                cancellationToken
            );
            if (condition is null)
            {
                return Result.Failure(ConditionErrors.NotFound);
            }

            Result result = condition.Update(request.Description);
            if (result.IsFailure)
            {
                return result;
            }

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
