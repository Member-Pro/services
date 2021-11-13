using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Models.Members;
using MemberPro.Core.Services.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("members")]
    [Authorize]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<SimpleMemberModel>>> Get([FromQuery] SearchMembersModel model)
        {
            var members = await _memberService.SearchAsync(model);
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberModel>> GetById(int id)
        {
            var member = await _memberService.FindByIdAsync(id);
            return Ok(member);
        }
    }
}
