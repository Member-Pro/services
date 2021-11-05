using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Exceptions;
using MemberPro.Core.Models.Members;
using MemberPro.Core.Security;
using MemberPro.Core.Services;
using MemberPro.Core.Services.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("achievements/favorites")]
    [Authorize]
    public class FavoriteAchievementsController : ControllerBase
    {
        private readonly IFavoriteAchievementService _favoriteAchievementService;
        private readonly IWorkContext _workContext;

        public FavoriteAchievementsController(IFavoriteAchievementService favoriteAchievementService,
            IWorkContext workContext)
        {
            _favoriteAchievementService = favoriteAchievementService;
            _workContext = workContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<FavoriteAchievementModel>>> GetByMemberId()
        {
            var achievements = await _favoriteAchievementService.GetByMemberId(_workContext.GetCurrentUserId());
            return Ok(achievements);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteAchievementModel>> GetById(int id)
        {
            var result = await _favoriteAchievementService.FindById(id);
            if (result.MemberId != _workContext.GetCurrentUserId())
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<ActionResult<List<FavoriteAchievementModel>>> Create(CreateFavoriteAchievementModel model)
        {
            model.MemberId = _workContext.GetCurrentUserId();
            var result = await _favoriteAchievementService.Create(model);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, FavoriteAchievementModel model)
        {
            // TODO: verify user can update the entity (without passing in the memberId to Update())
            try
            {
                await _favoriteAchievementService.Update(_workContext.GetCurrentUserId(), model);
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
                await _favoriteAchievementService.Delete(id);
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
