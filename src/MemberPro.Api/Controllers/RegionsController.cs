using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Models.Geography;
using MemberPro.Core.Services.Geography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("regions")]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionsController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<RegionModel>> GetById(int id)
        {
            var result = await _regionService.FindById(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RegionModel>>> Get()
        {
            var regions = await _regionService.GetAll();

            return Ok(regions);
        }

        [HttpPost("")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<RegionModel>> Create(CreateRegionModel model)
        {
            try
            {
                var result = await _regionService.Create(model);

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch(Exception)
            {
                return StatusCode(500, "Error creating region");
            }
        }
    }
}
