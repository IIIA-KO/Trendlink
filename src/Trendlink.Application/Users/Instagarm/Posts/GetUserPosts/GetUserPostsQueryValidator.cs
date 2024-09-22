using FluentValidation;

namespace Trendlink.Application.Users.Instagarm.Posts.GetUserPosts
{
    internal sealed class GetUserPostsQueryValidator : AbstractValidator<GetUserPostsQuery>
    {
        public GetUserPostsQueryValidator()
        {
            this.RuleFor(c => c.Limit)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Limit cannot be less than 1");
        }
    }
}
