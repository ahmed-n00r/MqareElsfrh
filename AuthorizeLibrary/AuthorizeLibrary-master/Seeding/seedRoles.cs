using AuthorizeLibrary.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeLibrary.Seeding
{
    public static class seedRoles
    {
        
        public static async Task seedAsync(RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.Roles.Any())
                foreach (var item in RoleConstants.rolesList())
                    await roleManager.CreateAsync(new(item));
        }
    }
}
