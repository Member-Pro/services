using System.Threading.Tasks;
using MemberPro.Core.Services;
using MemberPro.Core.Services.Members;
using Microsoft.AspNetCore.Authorization;

namespace MemberPro.Api.Security
{
    public class AdminUserRequirement : IAuthorizationRequirement
    {

    }

    public class AdminUserAuthorizationHandler : AuthorizationHandler<AdminUserRequirement>
    {
        private readonly IMemberRoleService _memberRoleService;
        private readonly IWorkContext _workContext;

        public AdminUserAuthorizationHandler(IMemberRoleService memberRoleService,
            IWorkContext workContext)
        {
            _memberRoleService = memberRoleService;
            _workContext = workContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminUserRequirement requirement)
        {
            var userId = _workContext.GetCurrentUserId();
            if (await _memberRoleService.IsMemberInRoleAsync(userId, RoleIds.Admin))
            {
                context.Succeed(requirement);
            }
        }
    }
}
