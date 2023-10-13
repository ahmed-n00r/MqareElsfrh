using AuthorizeLibrary.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeLibrary.Seeding
{
    public static class seedClaims
    {
        public static async Task seedClaimsToRole(this RoleManager<IdentityRole> roleManager, string roleNmae, string modelName)
        {
            var role = await roleManager.FindByNameAsync(roleNmae);
            await roleManager.addPermissionClaims(modelName, role);
        }

        //this in method parameter call
        public static async Task addPermissionClaims(this RoleManager<IdentityRole> roleManager
            , string modelName,IdentityRole role)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = permissionConstants.PermissionsList(modelName);

            foreach(var permission in allPermissions)
            {
                if (!allClaims.Any(c => c.Type.Equals(permissionConstants.Permission) && c.Value.Equals(permission)))
                    await roleManager.AddClaimAsync(role, new(permissionConstants.Permission,permission));
            }
        }
    }
}
