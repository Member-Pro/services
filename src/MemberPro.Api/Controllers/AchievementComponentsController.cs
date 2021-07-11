using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Exceptions;
using MemberPro.Core.Models.Achievements;
using MemberPro.Core.Services.Achievements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("achievements/{achievementId}/components")]
    [Authorize]
    public class AchievementComponentsController : ControllerBase
    {
        private readonly IAchievementComponentService _componentService;

        public AchievementComponentsController(IAchievementComponentService componentService)
        {
            _componentService = componentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AchievementComponentModel>> GetById(int achievementId, int id)
        {
            var requirement = await _componentService.FindByIdAsync(id);
            return Ok(requirement);
        }

        [HttpGet("")]
        public async Task<ActionResult<List<AchievementComponentModel>>> GetByAchievement(int achievementId)
        {
            var requirements = await _componentService.GetByAchievementIdAsync(achievementId);
            return Ok(requirements);
        }

        [HttpPost("")]
        public async Task<ActionResult<AchievementModel>> Create(int achievementId, CreateAchievementComponentModel model)
        {
            var result = await _componentService.CreateAsync(achievementId, model);

            return CreatedAtAction(nameof(GetById), new { achievementId, id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int achievementId, int id, AchievementComponentModel model)
        {
            model.Id = id;

            try
            {
                await _componentService.UpdateAsync(model);

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
