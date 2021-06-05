using System.Threading.Tasks;
using MemberPro.Core.Models.Members;
using MemberPro.Core.Security;
using MemberPro.Core.Services.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public UserController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet("current")]
        public async Task<ActionResult<MemberModel>> CurrentUser()
        {
            var userId = User.GetUserId();

            var member = await _memberService.FindByIdAsync(userId);

            return Ok(member);
        }

        [HttpPost("")]
        [AllowAnonymous]
        public async Task<ActionResult<MemberModel>> Register(RegisterUserModel model)
        {
            var member = await _memberService.RegisterAsync(model);

            return CreatedAtAction(nameof(CurrentUser), null, member);
        }

        [HttpPut("")]
        public async Task<ActionResult<MemberModel>> Update(MemberModel model)
        {
            model.Id = User.GetUserId(); // set the ID to the current user

            await _memberService.UpdateAsync(model);

            return NoContent();
        }
    }
}
