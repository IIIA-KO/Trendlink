namespace Trendlink.Application.Users.Instagarm.GetUserPosts
{
    public sealed record PostResponse(
        string Id,
        string MediaType,
        Uri MediaUrl,
        Uri Permalink,
        DateTime TimeSpamp
    );
}
