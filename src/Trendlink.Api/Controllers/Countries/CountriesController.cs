using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Countries.GetAllCountries;
using Trendlink.Application.Countries.GetStates;
using Trendlink.Domain.Users.Countries;

namespace Trendlink.Api.Controllers.Countries
{
    [Route("/api/countries")]
    public class CountriesController : BaseApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllCountriesQuery();

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("states/{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCountryStates(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var query = new GetStatesQuery(id);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }
    }
}
