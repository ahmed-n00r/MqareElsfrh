using AuthorizeLibrary.Data;
using DBModels.IdentityModel;
using DBModels.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MqareElsfrh.web.Controllers
{
    public class UserController : Controller
    {

        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ApplicationDbContext context;

        public UserController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }



        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users
                .Select(user => new UserViewModel { Id = user.Id, UserName = user.UserName, Email = user.Email, Roles = userManager.GetRolesAsync(user).Result })
                .ToListAsync();

            return View(users);
        }

        public async Task<IActionResult> UserGroup(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var group = await context.Groups.ToListAsync();

            if (user == null)
                return NotFound();

            var viewModel = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.FullName,
                Roles = group.Select(role => new CheckBoxViewModel
                {
                    DisplayValue = role.Name,
                    IsSelected =  user.GroupId == role.Id //userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var roles = await roleManager.Roles.ToListAsync();

            var viewModel = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.FullName,
                Roles = roles.Select(role => new CheckBoxViewModel
                {
                    DisplayValue = role.Name,
                    IsSelected = userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRoles(UserRolesViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null)
                return NotFound();

            var userRoles = await userManager.GetRolesAsync(user);

            await userManager.RemoveFromRolesAsync(user, userRoles);
            await userManager.AddToRolesAsync(user, model.Roles.Where(r => r.IsSelected).Select(r => r.DisplayValue));

            //foreach (var role in model.Roles)
            //{
            //    if (userRoles.Any(r => r == role.RoleName) && !role.IsSelected)
            //        await userManager.RemoveFromRoleAsync(user, role.RoleName);

            //    if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
            //        await userManager.AddToRoleAsync(user, role.RoleName);
            //}

            return RedirectToAction(nameof(Index));
        }

    }
}
