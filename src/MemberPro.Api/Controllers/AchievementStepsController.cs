using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Models.Achievements;
using MemberPro.Core.Services.Achievements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("achievements/{achievementId}/steps")]
    [Authorize]
    public class AchievementStepsController : ControllerBase
    {
        private readonly IAchievementStepService _stepService;

        public AchievementStepsController(IAchievementStepService stepService)
        {
            _stepService = stepService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AchievementStepModel>> GetById(int achievementId, int id)
        {
            var step = await _stepService.FindByIdAsync(id);
            return Ok(step);
        }

        [HttpGet("")]
        public async Task<ActionResult<List<AchievementStepModel>>> GetByAchievement(int achievementId)
        {
            var steps = await _stepService.GetByAchievementIdAsync(achievementId);
            return Ok(steps);
        }

        [HttpPost("")]
        public async Task<ActionResult<AchievementModel>> Create(int achievementId, CreateAchievementStepModel model)
        {
            model.AchievementId = achievementId;

            var result = await _stepService.CreateAsync(model);

            return CreatedAtAction(nameof(GetById), new { achievementId, id = result.Id }, result);
        }
    }
}
