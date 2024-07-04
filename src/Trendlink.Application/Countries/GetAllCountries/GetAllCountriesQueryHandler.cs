using System.Data;
using Dapper;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Countries.GetAllCountries
{
    internal sealed class GetAllCountriesQueryHandler
        : IQueryHandler<GetAllCountriesQuery, IReadOnlyCollection<CountryResponse>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllCountriesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyCollection<CountryResponse>>> Handle(
            GetAllCountriesQuery request,
            CancellationToken cancellationToken
        )
        {
            using IDbConnection dbConnection = this._sqlConnectionFactory.CreateConnection();

            const string sql = """
                SELECT
                    id AS Id,
                    name AS Name
                FROM countries
                """;

            return (await dbConnection.QueryAsync<CountryResponse>(sql)).ToList();
        }
    }
}
