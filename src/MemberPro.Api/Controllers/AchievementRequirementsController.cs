using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Exceptions;
using MemberPro.Core.Models.Achievements;
using MemberPro.Core.Services;
using MemberPro.Core.Services.Achievements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("requirements")]
    [Authorize]
    public class AchievementRequirementsController : ControllerBase
    {
        private readonly IWorkContext _workContext;
        private readonly IRequirementService _requirementService;

        public AchievementRequirementsController(IWorkContext workContext,
            IRequirementService requirementService)
        {
            _workContext = workContext;
            _requirementService = requirementService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequirementModel>> GetById(int componentId, int id)
        {
            var achievement = await _requirementService.FindByIdAsync(id);
            return Ok(achievement);
        }

        [HttpGet("/achievements/{achievementId}/requirements")]
        public async Task<ActionResult<List<RequirementModel>>> GetByAchievement(int achievementId)
        {
            var requirements = await _requirementService.GetByAchievementIdAsync(achievementId, _workContext.GetCurrentUserId());
            return Ok(requirements);
        }

        [HttpGet("/components/{componentId}/requirements")]
        public async Task<ActionResult<List<RequirementModel>>> GetByComponent(int componentId)
        {
            var achievements = await _requirementService.GetByComponentIdAsync(componentId);
            return Ok(achievements);
        }

        [HttpPost("/components/{componentId}/requirements")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<RequirementModel>> Create(int componentId, RequirementModel model)
        {
            // TODO: Make a Create model...
            var result = await _requirementService.CreateAsync(componentId, model);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult> Update(int id, RequirementModel model)
        {
            try
            {
                await _requirementService.UpdateAsync(model);
                return NoContent();
            }
            catch(ItemNotFoundException)
            {
                return NotFound();
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _requirementService.DeleteAsync(id);
                return NoContent();
            }
            catch(ItemNotFoundException)
            {
                return NotFound();
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
