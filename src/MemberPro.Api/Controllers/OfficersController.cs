using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Enums;
using MemberPro.Core.Models.Organizations;
using MemberPro.Core.Services;
using MemberPro.Core.Services.Common;
using MemberPro.Core.Services.Organizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("officers")]
    [Authorize]
    public class OfficersController : ControllerBase
    {
        private readonly IWorkContext _workContext;
        private readonly IDateTimeService _dateTimeService;
        private readonly IOfficerService _officerService;
        private readonly ILogger<OfficersController> _logger;

        public OfficersController(IWorkContext workContext,
            IDateTimeService dateTimeService,
            IOfficerService officerService,
            ILogger<OfficersController> logger)
        {
            _workContext = workContext;
            _dateTimeService = dateTimeService;
            _officerService = officerService;
            _logger = logger;
        }


        [HttpGet("/organizations/{orgId}/officers")]
        public async Task<ActionResult<IEnumerable<OfficerModel>>> GetOfficersForOrg(int orgId,
            DateOnly? asOf = null, OfficerPositionType? positionType = null)
        {
            if (!asOf.HasValue)
            {
                asOf = _dateTimeService.Today;
            }

            var officers = await _officerService.GetCurrentOfficersForOrganizationAsync(orgId, asOf.Value, positionType);

            return Ok(officers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfficerModel>> GetById(int id)
        {
            var officer = await _officerService.FindOfficerByIdAsync(id);
            if (officer is null)
            {
                return NotFound();
            }

            return Ok(officer);
        }

        [HttpPost]
        public async Task<ActionResult<OfficerModel>> Create(CreateOfficerModel model)
        {
            try
            {
                var result = await _officerService.CreateOfficerAsync(model);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating officer");
                return StatusCode(500, "Error creating officer");
            }
        }
    }
}
