using Microsoft.AspNetCore.Authorization;
using PrintingBI.Common;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PrintingBI.API.Security
{
    public class AdminAccessOnly : AuthorizationHandler<AdminAccessOnly>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminAccessOnly requirement)
        {
            if(context.User.Identity is ClaimsIdentity identity)
            {
                var isSuperAdmin = identity.FindFirst(ClaimTypes.Role).Value;
                if(isSuperAdmin == RoleModel.SuperAdmin)
                    context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
