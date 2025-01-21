using Microsoft.AspNetCore.Authorization;

namespace Sd.Crm.Backend.Authorization
{
    public class CrmAuthorizationHandler : AuthorizationHandler<CrmAuthorizationRequirements>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CrmAuthorizationRequirements requirement)
        {
            switch (requirement.PermissionName)
            {
                case "admin" : 
                    CheckAdmin(context, requirement);
                    break;
                case "mentor":
                    CheckMentor(context, requirement); 
                    break;
                default: 
                    CheckDefault(context, requirement); 
                    break;                
            }
            return Task.CompletedTask;
        }

        private void CheckDefault(AuthorizationHandlerContext context, CrmAuthorizationRequirements requirement)
        {
            var user = context.User.Claims.FirstOrDefault(m => m.Type == AuthorizationConstants.UserName)?.Value;

            if (user != null)
            {
                context.Succeed(requirement);
            }
        }

        private void CheckMentor(AuthorizationHandlerContext context, CrmAuthorizationRequirements requirement)
        {
            var role = context.User.Claims.FirstOrDefault(m => m.Type == AuthorizationConstants.Role && m.Value == AuthorizationConstants.MentorRole)?.Value;

            if (role != null)
            {
                context.Succeed(requirement);
            }
        }

        private void CheckAdmin(AuthorizationHandlerContext context, CrmAuthorizationRequirements requirement)
        {
            var role = context.User.Claims.FirstOrDefault(m => m.Type == AuthorizationConstants.Role && m.Value == AuthorizationConstants.AdminRole)?.Value;

            if (role != null)
            {
                context.Succeed(requirement);
            }
        }
    }
}
