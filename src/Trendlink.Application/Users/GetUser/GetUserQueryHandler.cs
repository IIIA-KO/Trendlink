using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.GetUser
{
    internal sealed class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<Result<UserResponse>> Handle(
            GetUserQuery request,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByIdWithStateAsync(
                request.UserId,
                cancellationToken
            );
            if (user is null)
            {
                return Result.Failure<UserResponse>(UserErrors.NotFound);
            }

            return new UserResponse(
                user.Id.Value,
                user.Email.Value,
                user.FirstName.Value,
                user.LastName.Value,
                user.ProfilePhoto?.Id,
                user.ProfilePhoto?.Uri.ToString(),
                user.BirthDate,
                user.State.Country!.Name.Value,
                user.State.Name.Value,
                user.PhoneNumber.Value,
                user.Bio.Value,
                user.AccountCategory
            );
        }
    }
}
