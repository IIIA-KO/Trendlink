using System.Linq.Expressions;
using Trendlink.Application.Abstractions.Authentication;
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
        private readonly IUserContext _userContext;

        public GetUsersQueryHandler(IUserRepository userRepository, IUserContext userContext)
        {
            this._userRepository = userRepository;
            this._userContext = userContext;
        }

        public async Task<Result<PagedList<UserResponse>>> Handle(
            GetUsersQuery request,
            CancellationToken cancellationToken
        )
        {
            IQueryable<User> usersQuery = this._userRepository.DbSetAsQueryable();

            User user = await this._userRepository.GetByIdWithRolesAsync(
                this._userContext.UserId,
                cancellationToken
            );

            if (!user!.HasRole(Role.Administrator))
            {
                usersQuery = usersQuery.Where(user =>
                    !user.Roles.Any(r => r.Name == Role.Administrator.Name)
                );
            }

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                usersQuery = usersQuery.Where(user =>
                    ((string)user.FirstName).Contains(request.SearchTerm)
                    || ((string)user.LastName).Contains(request.SearchTerm)
                    || ((string)user.PhoneNumber).Contains(request.SearchTerm)
                );
            }

            usersQuery =
                request.SortOrder?.ToUpperInvariant() == "DESC"
                    ? usersQuery.OrderByDescending(GetSortProperty(request))
                    : usersQuery.OrderBy(GetSortProperty(request));

            IQueryable<UserResponse> userResponsesQuery = usersQuery.Select(
                user => new UserResponse(
                    user.Id.Value,
                    user.Email.Value,
                    user.FirstName.Value,
                    user.LastName.Value
                )
            );

            return await PagedList<UserResponse>.CreateAsync(
                userResponsesQuery,
                request.PageNumber,
                request.PageSize
            );
        }

        private static Expression<Func<User, object>> GetSortProperty(GetUsersQuery request)
        {
            return request.SortColumn?.ToUpperInvariant() switch
            {
                "FIRSTNAME" => user => user.FirstName,
                "LASTNAME" => user => user.LastName,
                "PHONENUMBER" => user => user.PhoneNumber,
                _ => user => user.Id,
            };
        }
    }
}
