using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Pagination;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.GetUsers
{
    internal sealed class GetUsersQueryHandler
        : IQueryHandler<GetUsersQuery, PagedList<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<Result<PagedList<UserResponse>>> Handle(
            GetUsersQuery request,
            CancellationToken cancellationToken
        )
        {
            IQueryable<User> usersQuery = this._userRepository.SearchUsers(
                new UserSeachParameters(
                    request.SearchTerm,
                    request.SortColumn,
                    request.SortOrder,
                    request.Country,
                    request.AccountCategory,
                    request.MinFollowersCount,
                    request.MinMediaCount
                )
            );

            IQueryable<UserResponse> userResponsesQuery = usersQuery.Select(
                user => new UserResponse(
                    user.Id.Value,
                    user.Email.Value,
                    user.FirstName.Value,
                    user.LastName.Value,
                    user.BirthDate,
                    user.State.Country!.Name.Value,
                    user.State.Name.Value,
                    user.PhoneNumber.Value,
                    user.Bio.Value,
                    user.AccountCategory,
                    user.ProfilePicture == null ? null : user.ProfilePicture.Uri.ToString()
                )
            );

            return await PagedList<UserResponse>.CreateAsync(
                userResponsesQuery,
                request.PageNumber,
                request.PageSize
            );
        }
    }
}
