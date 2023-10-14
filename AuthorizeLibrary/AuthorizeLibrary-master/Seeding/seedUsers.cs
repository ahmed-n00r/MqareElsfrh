using AuthorizeLibrary.Constants;
using AuthorizeLibrary.IdentityModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeLibrary.Seeding
{
    public static class seedUsers
    {
        public static async Task seedAsync(UserManager<AppUser> userManager
            ,RoleManager<IdentityRole> roleManager,
            AppUser user,
            List<string> role)
        {
            var x = await userManager.FindByEmailAsync(user.Email);
            if (userManager.FindByEmailAsync(user.Email).Result == null)
            {
                await userManager.CreateAsync(user,user.PasswordHash ?? AppConstants.defoletPassword);
                await userManager.AddToRolesAsync(user, role);
            }

            //foreach (var item in role)
            //    await roleManager.seedClaimsToRole(item,ModelConstants.Products);


        }

        public static async Task<AppUser> getUserByEmail(string email, UserManager<AppUser> userManager) => await userManager.FindByEmailAsync(email);

    }
}

