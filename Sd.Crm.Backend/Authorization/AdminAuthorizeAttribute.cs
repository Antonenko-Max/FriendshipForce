using Microsoft.AspNetCore.Authorization;

namespace Sd.Crm.Backend.Authorization
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        public AdminAuthorizeAttribute()
            : base(AuthorizationConstants.AdminRole)
        {
        }
    }
}
