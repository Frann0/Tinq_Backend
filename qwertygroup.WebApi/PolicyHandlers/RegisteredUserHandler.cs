using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using qwertygroup.Security;

namespace qwertygroup.WebApi.PolicyHandlers
{
    public class RegisteredUserHandler : AuthorizationHandler<RegisteredUserHandler>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RegisteredUserHandler requirementHandler)
        {
            var userId = context.User.Claims.FirstOrDefault(u => "Id".Equals(u.Type));
            int id;

            if (int.TryParse(userId?.Value, out id))
            {
                var defaultContext = context.Resource as DefaultHttpContext;
                var authService = defaultContext.HttpContext.RequestServices.GetRequiredService<IAuthService>();
                var permissions = authService.GetPermissions(id);
                if (permissions.Exists(p => p.Name.Equals("RegisteredUser")))
                {
                    context.Succeed(requirementHandler);
                }
                else
                {
                    context.Fail();
                }
            }
            return Task.CompletedTask;
        }
    }
}