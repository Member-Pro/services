using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Models.Achievements;
using MemberPro.Core.Services.Achievements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("achievements")]
    [Authorize]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievementService _achievementService;

        public AchievementsController(IAchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AchievementModel>> GetById(int id)
        {
            var achievement = await _achievementService.FindByIdAsync(id);
            return Ok(achievement);
        }

        [HttpGet("")]
        public async Task<ActionResult<List<AchievementModel>>> GetAll()
        {
            var achievements = await _achievementService.GetAllAsync();
            return Ok(achievements);
        }

        [HttpPost("")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<AchievementModel>> Create(CreateAchievementModel model)
        {
            var result = await _achievementService.CreateAsync(model);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
    }
}
