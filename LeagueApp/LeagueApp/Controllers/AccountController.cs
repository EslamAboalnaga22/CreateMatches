using LeagueApp.Models;
using LeagueApp.ViewModel.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Security.Claims;

namespace LeagueApp.Controllers
{
    // UserName: Eslam
    // Email: eslamnga220@gamil.com
    // Password: #Eslam1234
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerView)
        {
            if (ModelState.IsValid)
            {
                // create account
                ApplicationUser user = new();
                user.Email = registerView.Email;
                user.UserName = registerView.UserName;
                user.PasswordHash = registerView.Password;
                user.PhoneNumber = registerView.PhoneNumber;
                user.Address = registerView.Address;

                IdentityResult result = await userManager.CreateAsync(user, registerView.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdmin(RegisterViewModel registerView)
        {
            if (ModelState.IsValid)
            {
                // create account
                ApplicationUser user = new();
                user.Email = registerView.Email;
                user.UserName = registerView.UserName;
                user.PasswordHash = registerView.Password;
                user.PhoneNumber = registerView.PhoneNumber;
                user.Address = registerView.Address;

                IdentityResult result = await userManager.CreateAsync(user, registerView.Password);

                if (result.Succeeded)
                {
                    // assign to role
                    await userManager.AddToRoleAsync(user, "Admin");

                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginView)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByEmailAsync(loginView.UserName);

                if (user is not null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, loginView.Password);

                    if (found)
                    {
                        // make cookies
                        List<Claim> claims = new List<Claim>();

                        claims.Add(new Claim("Address", user.Address));

                        await signInManager.SignInWithClaimsAsync(user, loginView.RememberMe, claims);

                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Username or Passeword invalid");       
            }
            return View(loginView);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if(user is null)
                {
                    return RedirectToAction("Index", "Home");
                }

                var result = await userManager.ChangePasswordAsync(user, viewModel.OldPassword, viewModel.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                    return View();
                }

                await signInManager.RefreshSignInAsync(user);

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
