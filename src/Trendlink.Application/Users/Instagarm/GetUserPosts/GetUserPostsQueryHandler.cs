using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Users.Instagarm.GetUserPosts
{
    internal sealed class GetUserPostsQueryHandler
        : IQueryHandler<GetUserPostsQuery, IReadOnlyList<PostResponse>>
    {
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly IInstagramService _instagramService;

        public GetUserPostsQueryHandler(
            IUserContext userContext,
            IUserRepository userRepository,
            IInstagramService instagramService
        )
        {
            this._userContext = userContext;
            this._userRepository = userRepository;
            this._instagramService = instagramService;
        }

        public async Task<Result<IReadOnlyList<PostResponse>>> Handle(
            GetUserPostsQuery request,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByIdWithInstagramAccountAsync(
                this._userContext.UserId,
                cancellationToken
            );
            if (user is null)
            {
                return Result.Failure<IReadOnlyList<PostResponse>>(UserErrors.NotFound);
            }

            string result = await this._instagramService.GetUserPosts(
                user.Token!.AccessToken,
                user.InstagramAccount!.Metadata.Id,
                6,
                string.Empty,
                string.Empty,
                cancellationToken
            );

            Console.WriteLine(result.Length);

            return Result.Success<IReadOnlyList<PostResponse>>([]);
        }
    }
}
