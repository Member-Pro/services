using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Models.Geography;
using MemberPro.Core.Services.Geography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("countries/{countryId}/states")]
    [Authorize]
    public class StateProvincesController : ControllerBase
    {
        private readonly IStateProvinceService _stateProvinceService;

        public StateProvincesController(IStateProvinceService stateProvinceService)
        {
            _stateProvinceService = stateProvinceService;
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<StateProvinceModel>>> Get(int countryId)
        {
            var states = await _stateProvinceService.GetByCountryIdAsync(countryId);

            return Ok(states);
        }
    }
}
