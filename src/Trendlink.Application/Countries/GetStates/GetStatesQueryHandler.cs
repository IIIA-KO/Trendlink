﻿using System.Data;
using Dapper;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.Countries;

namespace Trendlink.Application.Countries.GetStates
{
    internal sealed class GetStatesQueryHandler
        : IQueryHandler<GetStatesQuery, IReadOnlyCollection<StateResponse>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly ICountryRepository _countryRepository;

        public GetStatesQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            ICountryRepository countryRepository
        )
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
            this._countryRepository = countryRepository;
        }

        public async Task<Result<IReadOnlyCollection<StateResponse>>> Handle(
            GetStatesQuery request,
            CancellationToken cancellationToken
        )
        {
            bool countryExists = await this._countryRepository.ExistsById(
                new CountryId(request.CountryId),
                cancellationToken
            );
            if (!countryExists)
            {
                return Result.Failure<IReadOnlyCollection<StateResponse>>(CountryErrors.NotFound);
            }

            using IDbConnection dbConnection = this._sqlConnectionFactory.CreateConnection();

            const string sql = """
                SELECT
                    id AS Id,
                    name AS Name
                FROM states
                WHERE country_id = @CountryId
                """;

            try
            {
                return (
                    await dbConnection.QueryAsync<StateResponse>(sql, new { request.CountryId })
                ).ToList();
            }
            catch (Exception)
            {
                return Result.Failure<IReadOnlyCollection<StateResponse>>(Error.Unexpected);
            }
        }
    }
}
