using System.Data;
using Dapper;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Users.GetUser
{
    internal sealed class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<UserResponse>> Handle(
            GetUserQuery request,
            CancellationToken cancellationToken
        )
        {
            using IDbConnection connection = this._sqlConnectionFactory.CreateConnection();

            const string sql = """
                SELECT
                    u.id AS Id,
                    u.first_name AS FirstName,
                    u.last_name AS LastName,
                    u.email AS Email,
                    u.phone_number AS PhoneNumber,
                    u.bio AS Bio,
                    u.birth_date AS BirthDate,
                    s.name AS StateName,
                    c.name AS CountryName,
                    u.profile_picture AS ProfilePictureUrl
                FROM users AS u
                    INNER JOIN states AS s
                        ON u.state_id = s.id
                    INNER JOIN countries AS c
                        ON s.country_id = c.id
                WHERE u.id = @UserId
                """;

            try
            {
                UserResponse userResponse = await connection.QueryFirstOrDefaultAsync<UserResponse>(
                    sql,
                    new { UserId = request.UserId.Value }
                );

                return userResponse ?? Result.Failure<UserResponse>(UserErrors.NotFound);
            }
            catch (Exception)
            {
                return Result.Failure<UserResponse>(Error.Unexpected);
            }
        }
    }
}
