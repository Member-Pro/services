using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Exceptions;
using MemberPro.Core.Models.Members;
using MemberPro.Core.Security;
using MemberPro.Core.Services.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("tracked-achievements")]
    [Authorize]
    public class TrackedAchievementsController : ControllerBase
    {
        private readonly ITrackedAchievementService _trackedAchievementService;

        public TrackedAchievementsController(ITrackedAchievementService trackedAchievementService)
        {
            _trackedAchievementService = trackedAchievementService;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<TrackedAchievementModel>>> GetByMemberId()
        {
            var achievements = await _trackedAchievementService.GetByMemberId(User.GetUserId());
            return Ok(achievements);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrackedAchievementModel>> GetById(int id)
        {
            var result = await _trackedAchievementService.FindById(id);
            if (result.MemberId != User.GetUserId())
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<ActionResult<List<TrackedAchievementModel>>> Create(CreateTrackedAchievementModel model)
        {
            model.MemberId = User.GetUserId();
            var result = await _trackedAchievementService.Create(model);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, TrackedAchievementModel model)
        {
            // TODO: verify user can update the entity (without passing in the memberId to Update())
            try
            {
                await _trackedAchievementService.Update(User.GetUserId(), model);
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
        public async Task<ActionResult> Delete(int id)
        {
            // TODO: verify user can update the entity
            try
            {
                await _trackedAchievementService.Delete(id);
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
