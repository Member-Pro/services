using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Models.Members;
using MemberPro.Core.Security;
using MemberPro.Core.Services;
using MemberPro.Core.Services.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("/members/{memberId}/achievements")]
    [Authorize]
    public class MemberAchievementsController : ControllerBase
    {
        private readonly IMemberAchievementService _memberAchievementService;
        private readonly ILogger<MemberAchievementsController> _logger;
        private readonly IWorkContext _workContext;

        public MemberAchievementsController(IMemberAchievementService memberAchievementService,
            ILogger<MemberAchievementsController> logger,
            IWorkContext workContext)
        {
            _memberAchievementService = memberAchievementService;
            _logger = logger;
            _workContext = workContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<MemberAchievementModel>>> GetByMember(int memberId)
        {
            var achievements = await _memberAchievementService.GetByMemberId(memberId);

            return Ok(achievements);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberAchievementModel>> GetById(int memberId, int id)
        {
            var achievement = await _memberAchievementService.FindById(id);
            if (achievement == null || achievement.MemberId != memberId)
            {
                return NotFound();
            }

            return Ok(achievement);
        }

        [HttpPost]
        public async Task<ActionResult<MemberAchievementModel>> Create(int memberId, CreateMemberAchievementModel model)
        {
            try
            {
                model.MemberId = memberId;
                model.CreatedByMemberId = _workContext.GetCurrentUserId();

                var result = await _memberAchievementService.Create(model);

                return CreatedAtAction(nameof(GetById), new { memberId, id = result.Id }, result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating member achievement");
                return StatusCode(500);
            }
        }
    }
}
