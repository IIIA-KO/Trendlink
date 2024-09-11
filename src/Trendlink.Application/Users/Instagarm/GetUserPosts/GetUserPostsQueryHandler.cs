using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Users.Instagarm.GetUserPosts
{
    internal sealed class GetUserPostsQueryHandler
        : IQueryHandler<GetUserPostsQuery, UserPostsResponse>
    {
        private const int Limit = 6;
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly IInstagramService _instagramService;
        private readonly IKeycloakService _keycloakService;

        public GetUserPostsQueryHandler(
            IUserContext userContext,
            IUserRepository userRepository,
            IInstagramService instagramService,
            IKeycloakService keycloakService
        )
        {
            this._userContext = userContext;
            this._userRepository = userRepository;
            this._instagramService = instagramService;
            this._keycloakService = keycloakService;
        }

        public async Task<Result<UserPostsResponse>> Handle(
            GetUserPostsQuery request,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByIdWithInstagramAccountAndTokenAsync(
                this._userContext.UserId,
                cancellationToken
            );
            if (user is null)
            {
                return Result.Failure<UserPostsResponse>(UserErrors.NotFound);
            }

            bool isInstagramLinked =
                await this._keycloakService.IsExternalIdentityProviderAccountLinkedAsync(
                    user.IdentityId,
                    "instagram",
                    cancellationToken
                );
            if (!isInstagramLinked)
            {
                return Result.Failure<UserPostsResponse>(
                    InstagramAccountErrors.InstagramAccountNotLinked
                );
            }

            return await this._instagramService.GetUserPostsWithInsights(
                user.Token!.AccessToken,
                user.InstagramAccount!.Metadata.Id,
                Limit,
                request.CursorType ?? string.Empty,
                request.Cursor ?? string.Empty,
                cancellationToken
            );
        }
    }
}
