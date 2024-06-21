using LeagueApp.ViewModel.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LeagueApp.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult NewRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewRole(RoleViewModel roleView)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new();
                role.Name = roleView.RoleName;

                IdentityResult result = await roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return View(new RoleViewModel());
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(roleView);
        }
    }
}
