using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Models.Achievements;
using MemberPro.Core.Services;
using MemberPro.Core.Services.Achievements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    // [Route("")]
    [Authorize]
    public class MemberRequirementsController : ControllerBase
    {
        private readonly IWorkContext _workContext;
        private readonly IMemberRequirementService _memberRequirementService;

        public MemberRequirementsController(IWorkContext workContext,
            IMemberRequirementService memberRequirementService)
        {
            _workContext = workContext;
            _memberRequirementService = memberRequirementService;
        }

        [HttpGet("/achievements/{achievementId}/state")]
        public async Task<ActionResult<IEnumerable<MemberRequirementStateModel>>> GetForAchievement(int achievementId)
        {
            var result = await _memberRequirementService.GetStatesForAchievementIdAsync(_workContext.GetCurrentUserId(),
                achievementId);

            return Ok(result);
        }

        [HttpGet("/requirements/{requirementId}/state")]
        public async Task<ActionResult<MemberRequirementStateModel>> GetStateForCurrentUser(int requirementId)
        {
            var model = await _memberRequirementService.GetStateForRequirementAsync(_workContext.GetCurrentUserId(), requirementId);
            return Ok(model);
        }

        [HttpPut("/requirements/{requirementId}/state")]
        public async Task<ActionResult> UpdateStateForCurrentUser(int requirementId, MemberRequirementStateModel model)
        {
            model.MemberId = _workContext.GetCurrentUserId();
            model.RequirementId = requirementId;

            await _memberRequirementService.UpdateAsync(model);

            return Ok(); // NoContent?
        }
    }

}
