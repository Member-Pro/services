using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Models.Achievements;
using MemberPro.Core.Services.Achievements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("achievements/{achievementId}/requirements")]
    [Authorize]
    public class AchievementRequirementsController : ControllerBase
    {
        private readonly IAchievementRequirementService _requirementService;

        public AchievementRequirementsController(IAchievementRequirementService requirementService)
        {
            _requirementService = requirementService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AchievementRequirementModel>> GetById(int achievementId, int id)
        {
            var requirement = await _requirementService.FindByIdAsync(id);
            return Ok(requirement);
        }

        [HttpGet("")]
        public async Task<ActionResult<List<AchievementRequirementModel>>> GetByAchievement(int achievementId)
        {
            var requirements = await _requirementService.GetByAchievementIdAsync(achievementId);
            return Ok(requirements);
        }

        [HttpPost("")]
        public async Task<ActionResult<AchievementModel>> Create(int achievementId, CreateAchievementRequirementModel model)
        {
            model.AchievementId = achievementId;

            var result = await _requirementService.CreateAsync(model);

            return CreatedAtAction(nameof(GetById), new { achievementId, id = result.Id }, result);
        }
    }
}
