using Microsoft.AspNetCore.Authorization;

namespace Sd.Crm.Backend.Authorization
{
    public class CrmAuthorizationRequirements : IAuthorizationRequirement
    {
        public string PermissionName { get; set; }

        public CrmAuthorizationRequirements(string permissionName)
        {
            PermissionName = permissionName;
        }
    }
}
