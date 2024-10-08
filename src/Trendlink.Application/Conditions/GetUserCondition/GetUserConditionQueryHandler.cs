﻿using System.Data;
using Dapper;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;

namespace Trendlink.Application.Conditions.GetUserCondition
{
    internal sealed class GetUserConditionQueryHandler
        : IQueryHandler<GetUserConditionQuery, ConditionResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserConditionQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<ConditionResponse>> Handle(
            GetUserConditionQuery request,
            CancellationToken cancellationToken
        )
        {
            using IDbConnection dbConnection = this._sqlConnectionFactory.CreateConnection();

            Guid userId = request.UserId.Value;

            const string sql = """
                SELECT 
                    c.id AS Id,
                    c.user_id AS UserId,
                    c.description AS Description,
                    a.Id AS SplitProperty,
                    a.id AS Id,
                    a.name AS Name,
                    a.price_amount AS PriceAmount,
                    a.price_currency AS PriceCurrency,
                    a.description AS Description
                FROM 
                    conditions c
                LEFT JOIN 
                    advertisements a ON c.id = a.condition_id
                WHERE 
                    c.user_id = @UserId;
                """;

            var conditionDictionary = new Dictionary<Guid, ConditionResponse>();

            try
            {
                await dbConnection.QueryAsync<
                    ConditionResponse,
                    AdvertisementResponse,
                    ConditionResponse
                >(
                    sql,
                    (condition, advertisement) =>
                    {
                        if (
                            !conditionDictionary.TryGetValue(
                                condition.Id,
                                out ConditionResponse? conditionEntry
                            )
                        )
                        {
                            conditionEntry = new ConditionResponse
                            {
                                Id = condition.Id,
                                UserId = condition.UserId,
                                Description = condition.Description,
                                Advertisements = []
                            };

                            conditionDictionary.Add(condition.Id, conditionEntry);
                        }

                        if (advertisement != null)
                        {
                            var ad = new AdvertisementResponse
                            {
                                Id = advertisement.Id,
                                Name = advertisement.Name,
                                PriceAmount = advertisement.PriceAmount,
                                PriceCurrency = advertisement.PriceCurrency,
                                Description = advertisement.Description
                            };
                            conditionEntry.Advertisements.Add(ad);
                        }

                        return conditionEntry;
                    },
                    new { UserId = userId },
                    splitOn: "SplitProperty"
                );
            }
            catch (Exception)
            {
                return Result.Failure<ConditionResponse>(Error.Unexpected);
            }

            ConditionResponse? conditionResponse = conditionDictionary.Values.FirstOrDefault();

            if (conditionResponse == null)
            {
                return Result.Failure<ConditionResponse>(ConditionErrors.NotFound);
            }

            return Result.Success(conditionResponse);
        }
    }
}
