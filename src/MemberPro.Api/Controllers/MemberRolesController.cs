using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Models.Members;
using MemberPro.Core.Services.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("members/{memberId}/roles")]
    [Authorize(Policy = Policies.Admin)]
    public class MemberRolesController : ControllerBase
    {
        private readonly IMemberRoleService _roleService;

        public MemberRolesController(IMemberRoleService regionService)
        {
            _roleService = regionService;
        }

        [HttpGet("/members/roles/{id}")]
        public async Task<ActionResult<RoleModel>> GetById(int id)
        {
            var result = await _roleService.FindByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("")]
        public async Task<ActionResult<List<MemberRoleModel>>> GetByMember(int memberId)
        {
            var roles = await _roleService.GetByMemberIdAsync(memberId);
            return Ok(roles);
        }

        [HttpPost("")]
        public async Task<ActionResult<RoleModel>> Create(int memberId, CreateMemberRoleModel model)
        {
            try
            {
                model.MemberId = memberId;
                var result = await _roleService.CreateAsync(model);

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch(Exception)
            {
                return StatusCode(500, "Error adding member to role");
            }
        }
    }
}
