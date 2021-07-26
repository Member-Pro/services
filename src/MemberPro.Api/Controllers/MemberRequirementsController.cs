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
        private readonly IRequirementService _requirementService;

        public MemberRequirementsController(IWorkContext workContext,
            IRequirementService requirementService)
        {
            _workContext = workContext;
            _requirementService = requirementService;
        }

        [HttpGet("/achievements/{achievementId}/state")]
        public async Task<ActionResult<IEnumerable<MemberRequirementStateModel>>> GetForAchievement(int achievementId)
        {
            var result = await _requirementService.GetStatesForAchievementIdAsync(_workContext.GetCurrentUserId(),
                achievementId);

            return Ok(result);
        }

        [HttpGet("/requirements/{requirementId}/state")]
        public async Task<ActionResult<MemberRequirementStateModel>> GetStateForCurrentUser(int requirementId)
        {
            var model = await _requirementService.GetStateForRequirementAsync(_workContext.GetCurrentUserId(), requirementId);
            return Ok(model);
        }

        [HttpPut("/requirements/{requirementId}/state")]
        public async Task<ActionResult> UpdateStateForCurrentUser(int requirementId, UpdateMemberRequirementStateModel model)
        {
            model.MemberId = _workContext.GetCurrentUserId();
            model.RequirementId = requirementId;

            var result = await _requirementService.UpdateStateAsync(model);

            return Ok(result);
        }
    }

}
