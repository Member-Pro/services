using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Models.Organizations;
using MemberPro.Core.Services.Organizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("organizations")]
    [Authorize]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationsController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<OrganizationModel>> GetById(int id)
        {
            var result = await _organizationService.FindById(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<OrganizationModel>>> Get()
        {
            var organizations = await _organizationService.GetAll();

            return Ok(organizations);
        }

        [HttpPost("")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<OrganizationModel>> Create(CreateOrganizationModel model)
        {
            try
            {
                var result = await _organizationService.Create(model);

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch(Exception)
            {
                return StatusCode(500, "Error creating region");
            }
        }
    }
}
