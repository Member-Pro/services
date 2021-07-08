using MemberPro.Core.Security;
using Microsoft.AspNetCore.Http;

namespace MemberPro.Core.Services
{
    public interface IWorkContext
    {
        int GetCurrentUserId();
    }

    public class ApiWorkContext : IWorkContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiWorkContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentUserId()
        {
            var user = _httpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                return 0;
            }

            var userIdClaim = user.FindFirst(x => x.Type == AppClaimTypes.UserId && x.Issuer == AppClaimTypes.AppClaimsIssuer);

            return int.TryParse(userIdClaim?.Value, out var userId) ? userId : 0;
        }
    }
}
