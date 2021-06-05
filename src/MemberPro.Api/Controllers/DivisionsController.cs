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
    [Route("regions/{regionId}/divisions")]
    [Authorize]
    public class DivisionsController : ControllerBase
    {
        private readonly IDivisionService _divisionService;

        public DivisionsController(IDivisionService divisionService)
        {
            _divisionService = divisionService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DivisionModel>> GetById(int regionId, int id)
        {
            var result = await _divisionService.FindById(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DivisionModel>>> Get(int regionId)
        {
            var divisions = await _divisionService.GetByRegionId(regionId);

            return Ok(divisions);
        }

        [HttpPost("")]
        public async Task<ActionResult<DivisionModel>> Create(int regionId, CreateDivisionModel model)
        {
            try
            {
                model.RegionId = regionId;

                var result = await _divisionService.Create(model);

                return CreatedAtAction(nameof(GetById), new { regionId, id = result.Id }, result);
            }
            catch(Exception)
            {
                return StatusCode(500, "Error creating divisions");
            }
        }
    }
}
