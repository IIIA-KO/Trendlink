using FluentValidation;

namespace Trendlink.Application.Users.Instagarm.Posts.GetPosts
{
    internal sealed class GetPostsQueryValidator : AbstractValidator<GetPostsQuery>
    {
        public GetPostsQueryValidator()
        {
            this.RuleFor(c => c.Limit)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Limit cannot be less than 1");
        }
    }
}
