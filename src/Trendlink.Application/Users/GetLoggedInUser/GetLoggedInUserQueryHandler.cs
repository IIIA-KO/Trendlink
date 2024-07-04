using System.Data;
using Dapper;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Users.GetLoggedInUser
{
    internal sealed class GetLoggedInUserQueryHandler
        : IQueryHandler<GetLoggedInUserQuery, UserResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IUserContext _userContext;

        public GetLoggedInUserQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IUserContext userContext
        )
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
            this._userContext = userContext;
        }

        public async Task<Result<UserResponse>> Handle(
            GetLoggedInUserQuery request,
            CancellationToken cancellationToken
        )
        {
            using IDbConnection connection = this._sqlConnectionFactory.CreateConnection();

            const string sql = """
                SELECT
                    id AS Id,
                    first_name AS FirstName,
                    last_name AS LastName,
                    email AS Email
                FROM users
                WHERE identity_id = @IdentityId
                """;

            UserResponse user = await connection.QuerySingleAsync<UserResponse>(
                sql,
                new { this._userContext.IdentityId }
            );

            return user;
        }
    }
}
