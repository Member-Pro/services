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
    [Route("roles")]
    [Authorize(Policy = Policies.Admin)]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService regionService)
        {
            _roleService = regionService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<RoleModel>>> Get()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleModel>> GetById(int id)
        {
            var result = await _roleService.FindByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<ActionResult<RoleModel>> Create(RoleModel model)
        {
            try
            {
                var result = await _roleService.CreateAsync(model);

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch(Exception)
            {
                return StatusCode(500, "Error creating role");
            }
        }
    }
}
