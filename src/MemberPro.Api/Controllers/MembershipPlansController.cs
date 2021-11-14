using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Models.Plans;
using MemberPro.Core.Services.Plans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("plans")]
    [Authorize]
    public class MembershipPlansController : ControllerBase
    {
        private readonly IMembershipPlanService _membershipPlanService;

        public MembershipPlansController(IMembershipPlanService membershipPlanService)
        {
            _membershipPlanService = membershipPlanService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MembershipPlanModel>> GetById(int id)
        {
            var plan = await _membershipPlanService.FindByIdAsync(id);
            if (plan == null)
            {
                return NotFound();
            }

            return Ok(plan);
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<MembershipPlanModel>>> List()
        {
            var plans = await _membershipPlanService.GetAllAsync();

            return Ok(plans);
        }
    }
}
