using Microsoft.AspNetCore.Authorization;

namespace Sd.Crm.Backend.Authorization
{
    public class MentorAuthorizeAttribute : AuthorizeAttribute
    {
        public MentorAuthorizeAttribute()
            : base(AuthorizationConstants.MentorRole)
        {
        }
    }
}
