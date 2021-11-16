using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Models.Organizations;
using MemberPro.Core.Services;
using MemberPro.Core.Services.Organizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("officer-positions")]
    [Authorize]
    public class OfficerPositionsController : ControllerBase
    {
        private readonly IWorkContext _workContext;
        private readonly IOfficerService _officerService;
        private readonly ILogger<OfficerPositionsController> _logger;

        public OfficerPositionsController(IWorkContext workContext,
            IOfficerService officerService,
            ILogger<OfficerPositionsController> logger)
        {
            _workContext = workContext;
            _officerService = officerService;
            _logger = logger;
        }

        [HttpGet("/organizations/{orgId}/positions")]
        public async Task<ActionResult<IEnumerable<OfficerPositionModel>>> GetPositionsForOrg(int orgId)
        {
            var positions = await _officerService.GetPositionsForOrganizationAsync(orgId);

            return Ok(positions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfficerPositionModel>> GetById(int id)
        {
            var position = await _officerService.FindPositionByIdAsync(id);
            if (position is null)
            {
                return NotFound();
            }

            return Ok(position);
        }

        [HttpPost]
        public async Task<ActionResult<OfficerPositionModel>> Create(CreateOfficerPositionModel model)
        {
            try
            {
                var result = await _officerService.CreatePositionAsync(model);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating position");
                return StatusCode(500, "Error creating position");
            }
        }
    }
}
