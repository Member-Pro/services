using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Exceptions;
using MemberPro.Core.Models.Achievements;
using MemberPro.Core.Security;
using MemberPro.Core.Services.Achievements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("achievements/{achievementId}/activities")]
    [Authorize]
    public class AchievementActivitiesController : ControllerBase
    {
        private readonly IAchievementActivityService _activityService;

        public AchievementActivitiesController(IAchievementActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AchievementActivityModel>> GetById(int achievementId, int id)
        {
            var activity = await _activityService.FindByIdAsync(id);
            return Ok(activity);
        }

        [HttpGet("")]
        public async Task<ActionResult<List<AchievementActivityModel>>> GetByAchievement(int achievementId,
            int? requirementId = null)
        {
            var requirements = await _activityService.GetByMemberIdAsync(achievementId, User.GetUserId(), requirementId);
            return Ok(requirements);
        }

        [HttpPost("")]
        public async Task<ActionResult<AchievementModel>> Create(int achievementId, CreateAchievementActivityModel model)
        {
            model.AchievementId = achievementId;
            model.MemberId = User.GetUserId();

            var result = await _activityService.CreateAsync(model);

            return CreatedAtAction(nameof(GetById), new { achievementId, id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int achievementId, int id, AchievementActivityModel model)
        {
            // TODO: Verify user can update

            try
            {
                model.Id = id;
                model.MemberId = User.GetUserId();
                model.AchievementId = achievementId;

                await _activityService.UpdateAsync(model);

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
        public async Task<ActionResult> Delete(int achievementId, int id)
        {
            // TODO: Validate user can delete record

            try
            {
                await _activityService.DeleteAsync(id);

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
